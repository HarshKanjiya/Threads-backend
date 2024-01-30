using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using UserApi.microservice.Data;
using UserApi.microservice.Models;
using UserApi.microservice.Models.DTOs;
using UserApi.microservice.services;
using UserApi.microservice.Utils;

namespace UserApi.microservice.Controllers
{
    [ApiController, Route("Auth")]
    public class AuthController : ControllerBase
    {
        private readonly DbContextUsers db;
        private readonly HttpClient httpClient;
        private readonly ILogger<AuthController> logger;

        public AuthController(DbContextUsers _db, IMessageProducer _messageProducer, HttpClient _httpClient, ILogger<AuthController> _logger)
        {
            db = _db;
            httpClient = _httpClient;
            logger = _logger;
        }
        [HttpPost("register")]
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

                ImageUpload Uploader = new ImageUpload();

                var img = await Uploader.Upload(req.Avatar);

                if (img == null)
                {
                    responseDTO.Message = "Image Upload failed.";
                    responseDTO.Success = false;
                    return BadRequest(responseDTO);
                }

                var salt = PasswordUtils.GeneratePasswordSalt(10);
                var encPassword = PasswordUtils.EncodePassword(req.Password, salt);


                UserModel newUser = new UserModel()
                {
                    UserName = req.UserName,
                    PhoneNumber = req.PhoneNumber,
                    Password = encPassword,
                    PasswordSalt = salt,
                    Name = req.Name,
                    Gender = req.Gender,
                    Email = req.Email,
                    Birthdate = req.BirthDate,
                    AvatarURL = img.Url.ToString(),
                    AvatarPublicID = img.PublicId
                };

                var user = await db.Users.AddAsync(newUser);
                await db.SaveChangesAsync();

                if (user != null)
                {
                    responseDTO.Message = "Account Created Successfully.";
                    responseDTO.Success = true;
                    user.Entity.Password = "";
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
                responseDTO.Message = "Something went wrong :" + e.Message;
                responseDTO.Success = false;
                return BadRequest(responseDTO);

            }
        }


        [HttpPost("login")]
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
                        var encodingPasswordString = PasswordUtils.EncodePassword(req.Password, User.PasswordSalt);

                        if (string.Equals(User.Password, encodingPasswordString))
                        {
                            responseDTO.Message = "Logged in Successfully.";
                            responseDTO.Success = true;
                            User.Password = "";
                            User.PasswordSalt = "";
                            responseDTO.Data = User;

                            return Ok(responseDTO);
                        }
                    }

                    responseDTO.Message = "Invalid credentials.";
                    responseDTO.Success = false;

                    return Ok(responseDTO);


                }
                else if (Regex.IsMatch(req.UniqueId, Phonenumber_REGEX))
                {
                    // Phone number
                    var User = db.Users.FirstOrDefault(item => string.Equals(item.PhoneNumber, req.UniqueId));

                    if (User != null)
                    {
                        var encodingPasswordString = PasswordUtils.EncodePassword(req.Password, User.PasswordSalt);

                        if (string.Equals(User.Password, encodingPasswordString))
                        {
                            responseDTO.Message = "Logged in Successfully.";
                            responseDTO.Success = true;
                            User.Password = "";
                            User.PasswordSalt = "";
                            responseDTO.Data = User;

                            return Ok(responseDTO);
                        }
                    }

                    responseDTO.Message = "Invalid credentials.";
                    responseDTO.Success = false;

                    return Ok(responseDTO);
                }
                else if (Regex.IsMatch(req.UniqueId, Email_REGEX))
                {
                    // email
                    var User = db.Users.FirstOrDefault(item => string.Equals(item.Email, req.UniqueId));

                    if (User != null)
                    {
                        var encodingPasswordString = PasswordUtils.EncodePassword(req.Password, User.PasswordSalt);

                        if (string.Equals(User.Password, encodingPasswordString))
                        {
                            responseDTO.Message = "Logged in Successfully.";
                            responseDTO.Success = true;
                            User.Password = "";
                            User.PasswordSalt = "";
                            responseDTO.Data = User;

                            return Ok(responseDTO);
                        }
                    }

                    responseDTO.Message = "Invalid credentials.";
                    responseDTO.Success = false;

                    return Ok(responseDTO);
                }
                else
                {
                    responseDTO.Message = "Invalid credentials.";
                    responseDTO.Success = false;

                    return Ok(responseDTO);
                }
            }
            catch (Exception e)
            {
                responseDTO.Message = "Something went wrong";
                responseDTO.Success = false;
                return BadRequest(responseDTO);

            }
        }


        [HttpPost("username")]
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


        [HttpGet("user/{username}")]
        public async Task<ActionResult<ResponseDTO>> GetSingleUserProfileData(string username)
        {
            // messageProducer.SendingMessage("harsh");
            ResponseDTO responseDTO = new ResponseDTO();

            try
            {
                var dbUser = db.Users.FirstOrDefault(u => u.UserName == username);
                if (dbUser != null)
                {

                    ResponseDTO postsResponse = await httpClient.GetFromJsonAsync<ResponseDTO>("https://localhost:7202/thread/user/" + dbUser.UserId);

                    UserProfileResponseDTO res = new UserProfileResponseDTO() { user = dbUser };

                    if (postsResponse.Success == true)
                    {
                        res.posts = postsResponse.Data;
                    }


                    responseDTO.Success = true;
                    responseDTO.Message = "User data found";
                    responseDTO.Data = res;

                    return Ok(responseDTO);
                }
                responseDTO.Success = false;
                responseDTO.Message = "User data not found";
                return BadRequest(responseDTO);

            }
            catch (Exception e)
            {
                responseDTO.Message = "Something went wrong :" + e.Message;
                responseDTO.Success = false;
                return BadRequest(responseDTO);


            }



        }
        public class UserProfileResponseDTO
        {
            public UserModel user { get; set; }
            public Object posts { get; set; }
        }

    }
}
