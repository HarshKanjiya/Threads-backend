using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using UserApi.microservice.Data;
using UserApi.microservice.Models;
using UserApi.microservice.Models.DTOs;
using UserApi.microservice.Utils;
using Microsoft.AspNetCore.Http.HttpResults;

namespace UserApi.microservice.Controllers
{

    [ApiController]
    public class OtpController : ControllerBase
    {
        private readonly DbContextUsers db;

        public OtpController(DbContextUsers _db)
        {
            db = _db;
        }

        [HttpPost]
        [Route("otp/create")]
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
                    response.Message = "Otp couldn't be sent1.";
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

        [HttpPost, Route("otp/verify")]
        public async Task<ActionResult<ResponseDTO>> verifyOtp(OtpVerifyDTO req)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                var User = db.Users.FirstOrDefault(u => string.Equals(u.Email, req.Email));
                if (User == null)
                {
                    response.Message = "User doesnot exist.";
                    response.Success = false;
                    return BadRequest(response);
                }



                var OtpRecord = db.Otps.FirstOrDefault(otp => string.Equals(otp.UserId, User.UserId));

                if (OtpRecord == null)
                {
                    response.Message = "OTP doesn't exist, Please try again.";
                    response.Success = false;
                    return BadRequest(response);
                }

                int timeDifference = (int)DateTime.Now.Subtract(OtpRecord.CreatedAt).TotalSeconds;
                if (timeDifference > 60)
                {
                    response.Message = "OTP has Expired!" + timeDifference;
                    response.Success = false;
                    return BadRequest(response);
                }

                if (string.Equals(OtpRecord.otp, req.Otp))
                {
                    User.Password = "";
                    User.PasswordSalt = "";
                    response.Message = "Login Successful." + timeDifference;
                    response.Success = true;
                    response.Data = User;
                    return Ok(response);
                }
                else
                {
                    response.Message = "OTP doesn't match!" + timeDifference + "," + OtpRecord.otp + "," + req.Otp;
                    response.Success = false;
                    return BadRequest(response);
                }


            }
            catch (Exception e)
            {
                response.Message = "Something went wrong";
                response.Success = false;
                return BadRequest(e.ToString());

            }
        }
    }
}
