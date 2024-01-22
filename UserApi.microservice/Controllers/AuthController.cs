using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using UserApi.microservice.Data;
using UserApi.microservice.Models;
using UserApi.microservice.Models.DTOs;

namespace UserApi.microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DbContextUsers db;

        public AuthController(DbContextUsers _db)
        {
            db = _db;
        }

        [HttpPost, Route("/register")]
        public async Task<ActionResult<ResponseDTO>> SignUp(SignupRequestDTO req)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                var existance = db.Users.FirstOrDefault(u => string.Equals(u.UserName, req.UserName));

                if (existance != null)
                {
                    responseDTO.Message = "Username Already taken.";
                    responseDTO.Success = false;
                    return BadRequest(responseDTO);
                }

                //create user
                var img = "data:image/jpeg;base64,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";
                UserModel newUser = new UserModel()
                {
                    UserName = req.UserName,
                    PhoneNumber = req.PhoneNumber,
                    Password = req.Password,
                    Name = req.Name,
                    Gender = req.Gender,
                    Email = req.Email,
                    Birthdate = req.BirthDate,
                    Avatar = img
                };

                var user = await db.Users.AddAsync(newUser);
                await db.SaveChangesAsync();

                if (user != null)
                {
                    responseDTO.Message = "Account Created Successfully.";
                    responseDTO.Success = true;
                    responseDTO.Data = user.Entity;
                    return Ok(responseDTO);
                }
                else
                {
                    responseDTO.Message = "Account creating failed.";
                    responseDTO.Success = false;
                    return BadRequest(responseDTO);

                }
            }
            catch (Exception e)
            {
                responseDTO.Message = "Something went wrong";
                responseDTO.Success = false;
                return BadRequest(responseDTO);

            }
        }

        [HttpPost, Route("/login")]
        public async Task<ActionResult<ResponseDTO>> Login(LoginRequestDTO req)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                string Username_REGEX = @"^[a-zA-Z0-9_]+$";
                string Phonenumber_REGEX = @"^\d{10}$";
                string Email_REGEX = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

                if (Regex.IsMatch(req.UniqueId, Username_REGEX))
                {
                    // username
                    var User = db.Users.FirstOrDefault(item => string.Equals(item.UserName, req.UniqueId));

                    if (User != null)
                    {
                        if (string.Equals(User.Password, req.Password))
                        {
                            responseDTO.Message = "Logged in Successfully.";
                            responseDTO.Success = true;
                            responseDTO.Data = User;

                            return Ok(responseDTO);
                        }
                    }

                    responseDTO.Message = "Invalid credentials.";
                    responseDTO.Success = false;

                    return BadRequest(responseDTO);


                }
                else if (Regex.IsMatch(req.UniqueId, Phonenumber_REGEX))
                {
                    // Phone number
                    var User = db.Users.FirstOrDefault(item => string.Equals(item.PhoneNumber, req.UniqueId));

                    if (User != null)
                    {
                        if (string.Equals(User.Password, req.Password))
                        {
                            responseDTO.Message = "Logged in Successfully.";
                            responseDTO.Success = true;
                            responseDTO.Data = User;

                            return Ok(responseDTO);

                        }
                    }

                    responseDTO.Message = "Invalid credentials.";
                    responseDTO.Success = false;

                    return BadRequest(responseDTO);
                }
                else if (Regex.IsMatch(req.UniqueId, Email_REGEX))
                {
                    // email
                    var User = db.Users.FirstOrDefault(item => string.Equals(item.Email, req.UniqueId));

                    if (User != null)
                    {
                        if (string.Equals(User.Password, req.Password))
                        {
                            responseDTO.Message = "Logged in Successfully.";
                            responseDTO.Success = true;
                            responseDTO.Data = User;

                            return Ok(responseDTO);

                        }
                    }

                    responseDTO.Message = "Invalid credentials.";
                    responseDTO.Success = false;

                    return BadRequest(responseDTO);
                }
                else
                {
                    responseDTO.Message = "Invalid credentials.";
                    responseDTO.Success = false;

                    return BadRequest(responseDTO);
                }
            }
            catch (Exception e)
            {
                responseDTO.Message = "Something went wrong";
                responseDTO.Success = false;
                return BadRequest(responseDTO);

            }
        }

        [HttpPatch, Route("/username")]
        public async Task<ActionResult<ResponseDTO>> CheckUserNameAvaibility(CheckUsernameAvaibilityDTO req)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                var user = db.Users.FirstOrDefault(user => user.UserName == req.UserName);
                if (user == null)
                {
                    responseDTO.Message = "Username is Available.";
                    responseDTO.Success = true;

                    return Ok(responseDTO);

                }
                else
                {
                    responseDTO.Message = "Username is not Available.";
                    responseDTO.Success = false;

                    return BadRequest(responseDTO);
                }
            }
            catch (Exception e)
            {
                responseDTO.Message = "Something went wrong";
                responseDTO.Success = false;
                return BadRequest(responseDTO);


            }
        }


    }
}
