using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using UserApi.microservice.Data;
using UserApi.microservice.Models;
using UserApi.microservice.Models.DTOs;
using UserApi.microservice.Utils;

namespace UserApi.microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        private readonly DbContextUsers db;

        public OtpController(DbContextUsers _db)
        {
            db = _db;
        }

        [HttpPost]
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

                EmailService emailService = new EmailService("smtp.gmail.com", 587, "harshkanjiyaotp@gmail.com", "1Trillion$");


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

                var res = await db.Otps.AddAsync(OTP);
                await db.SaveChangesAsync();

                if (res != null)
                {
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
    }
}
