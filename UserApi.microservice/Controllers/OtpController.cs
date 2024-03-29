using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using UAParser;
using UserApi.microservice.Data;
using UserApi.microservice.Models;
using UserApi.microservice.Models.DTOs;
using UserApi.microservice.Utils;
using UserAuthenticationManager.Model;
using UserAuthenticationManager;
using Microsoft.EntityFrameworkCore;

namespace UserApi.microservice.Controllers
{

    [ApiController, Route("api/v1/otp")]
    public class OtpController : ControllerBase
    {
        private readonly DbContextUsers db;
        private readonly JwtTokenHandler JWT;
        private static readonly Random _random = new Random();

        public OtpController(DbContextUsers _db, JwtTokenHandler _jwt)
        {
            db = _db;
            JWT = _jwt;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ResponseDTO>> sendOtp(OtpCreateDTO req)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {

                var User = db.Users.FirstOrDefault(u => string.Equals(u.Email, req.Email));
                if (User == null)
                {
                    response.Message = "User doesnot exist.";
                    response.Success = false;
                    return response;
                }

                EmailService emailService = new EmailService("smtp.gmail.com", 587, "harshkanjiyaotp@gmail.com", "ppxs yjmq xgal vmzu\r\n");


                Random rnd = new Random();
                var otp = (rnd.Next(1000, 9999)).ToString();

                bool isEmailSent = await emailService.SendOtpEmailAsync(req.Email, otp);

                if (!isEmailSent)
                {
                    response.Message = "Otp couldn't be sent.";
                    response.Success = false;

                    return BadRequest(response);
                }

                OtpModel OTP = new OtpModel()
                {
                    otp = otp,
                    UserId = User.UserId
                };

                //check if otp record already exist, if yes then udate else create
                var oldOtpExistance = db.Otps.FirstOrDefault(o => o.UserId == User.UserId);

                if (oldOtpExistance == null)
                {
                    var res = await db.Otps.AddAsync(OTP);
                    await db.SaveChangesAsync();
                    if (res != null)
                    {
                        response.Message = "Otp sent Successfully.";
                        response.Success = true;

                        return Ok(response);
                    }
                }
                else
                {
                    oldOtpExistance.otp = otp;
                    db.SaveChanges();
                    response.Message = "Otp sent Successfully.";
                    response.Success = true;

                    return Ok(response);
                }

                response.Message = "Otp couldn't be sent.";
                response.Success = false;

                return BadRequest(response);
            }
            catch (Exception e)
            {
                response.Message = "Something went wrong";
                response.Success = false;
                return BadRequest(e.ToString());

            }
        }

        [HttpPost("verify")]
        public async Task<ActionResult<ResponseDTO>> verifyOtp(OtpVerifyDTO req)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                var User = db.Users.Include(u => u.Devices).FirstOrDefault(u => string.Equals(u.Email, req.Email));
                if (User == null)
                {
                    response.Message = "User doesnot exist.";
                    response.Success = false;
                    return Ok(response);
                }



                var OtpRecord = db.Otps.FirstOrDefault(otp => string.Equals(otp.UserId, User.UserId));

                if (OtpRecord == null)
                {
                    response.Message = "OTP doesn't exist, Please try again.";
                    response.Success = false;
                    return Ok(response);
                }

                int timeDifference = (int)DateTime.Now.Subtract(OtpRecord.CreatedAt).TotalSeconds;
                if (timeDifference > 60)
                {
                    response.Message = "OTP has Expired!" + timeDifference;
                    response.Success = false;
                    return Ok(response);
                }

                if (string.Equals(OtpRecord.otp, req.Otp))
                {

                    GenerateTokenRequestDTO tokenData = new GenerateTokenRequestDTO()
                    {
                        UserName = User.UserName,
                        Role = User.Role
                    };

                    var TokenResp = JWT.GenerateJwtToken(tokenData);

                    AccessTokenData accessTokenData = new AccessTokenData()
                    {
                        ExpiresIn = TokenResp.ExpiresIn,
                        Token = TokenResp.Token
                    };
                    var userAgent = HttpContext.Request.Headers["User-Agent"];
                    var uaParser = Parser.GetDefault();
                    ClientInfo c = uaParser.Parse(userAgent);

                    var rToken = GenerateRandomString(30);

                    DeviceModel device = new DeviceModel()
                    {
                        DeviceType = c.OS.Family,
                        RefreshToken = rToken,
                    };

                    User.Devices.Add(device);
                    db.Otps.Remove(OtpRecord);
                    db.SaveChanges();

                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddDays(7);
                    options.SameSite = SameSiteMode.None;
                    options.Secure = true;

                    CookieOptions optionsJWT = new CookieOptions();
                    optionsJWT.Expires = DateTime.Now.AddMinutes(20);
                    optionsJWT.SameSite = SameSiteMode.None;
                    optionsJWT.Secure = true;

                    Response.Cookies.Append("RefreshToken", rToken, options);
                    Response.Cookies.Append("AccessToken", TokenResp.Token, optionsJWT);
                    Response.Cookies.Append("UserName", User.UserName, options);

                    foreach (var d in User.Devices)
                    {
                        d.RefreshToken = null;
                    }

                    response.Message = "Logged in Successfully.";
                    response.Success = true;
                    User.Password = null;
                    User.PasswordSalt = null;
                    response.Data = User;

                    return Ok(response);
                }
                else
                {
                    response.Message = "OTP doesn't match!";
                    response.Success = false;
                    return Ok(response);
                }


            }
            catch (Exception e)
            {
                response.Message = "Something went wrong " + e.Message;
                response.Success = false;
                return BadRequest(e.ToString());

            }
        }


        // :: FOR RANDOM STRING ::
        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[_random.Next(chars.Length)]);
            }

            return stringBuilder.ToString();
        }

    }
}
