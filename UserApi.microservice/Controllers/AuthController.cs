using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using UAParser;
using UserApi.microservice.Data;
using UserApi.microservice.Models;
using UserApi.microservice.Models.DTOs;
using UserApi.microservice.services;
using UserApi.microservice.Utils;
using UserAuthenticationManager;
using UserAuthenticationManager.Model;

namespace UserApi.microservice.Controllers
{
    [ApiController, Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly DbContextUsers db;
        private readonly HttpClient httpClient;
        private readonly ILogger<AuthController> logger;

        private readonly JwtTokenHandler JWT;
        private static readonly Random _random = new Random();

        public AuthController(DbContextUsers _db, HttpClient _httpClient, ILogger<AuthController> _logger, JwtTokenHandler _jwt)
        {
            db = _db;
            httpClient = _httpClient;
            logger = _logger;
            JWT = _jwt;
        }

        [HttpPost("register"), AllowAnonymous]
        public async Task<ActionResult<ResponseDTO>> SignUp(SignupRequestDTO req)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {

                var userNameExistance = db.Users.FirstOrDefault(u => string.Equals(u.UserName, req.UserName));

                if (userNameExistance != null)
                {
                    responseDTO.Message = "Username Already taken.";
                    responseDTO.Success = false;
                    return Ok(responseDTO);
                }

                var EmailExistance = db.Users.FirstOrDefault(u => string.Equals(u.Email, req.Email));

                if (EmailExistance != null)
                {
                    responseDTO.Message = "Email already in use";
                    responseDTO.Success = false;
                    return Ok(responseDTO);
                }

                var PhoneExistance = db.Users.FirstOrDefault(u => string.Equals(u.Email, req.Email));

                if (PhoneExistance != null)
                {
                    responseDTO.Message = "Phone Number already in use";
                    responseDTO.Success = false;
                    return Ok(responseDTO);
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
                    AvatarURL = null,
                    AvatarPublicID = null
                };

                if (!string.IsNullOrWhiteSpace(req.Avatar))
                {
                    ImageUpload Uploader = new ImageUpload();
                    var img = await Uploader.Upload(req.Avatar);

                    if (img == null)
                    {
                        responseDTO.Message = "Image Upload failed.";
                        responseDTO.Success = false;
                        return Ok(responseDTO);
                    }

                    newUser.AvatarURL = img.Url.ToString();
                    newUser.AvatarPublicID = img.PublicId;
                }

                var userAgent = HttpContext.Request.Headers["User-Agent"];
                var uaParser = Parser.GetDefault();
                ClientInfo c = uaParser.Parse(userAgent);

                var rToken = GenerateRandomString(30);

                List<DeviceModel> devices = new List<DeviceModel>()
                {
                    new DeviceModel()
                    {
                        DeviceType = c.OS.Family,
                        RefreshToken = rToken,
                    }
                };

                newUser.Devices = devices;


                var user = await db.Users.AddAsync(newUser);
                await db.SaveChangesAsync();

                if (user.Entity != null)
                {
                    GenerateTokenRequestDTO tokenData = new GenerateTokenRequestDTO()
                    {
                        UserName = user.Entity.UserName,
                        Role = user.Entity.Role
                    };

                    var TokenResp = JWT.GenerateJwtToken(tokenData);

                    AccessTokenData accessTokenData = new AccessTokenData()
                    {
                        ExpiresIn = TokenResp.ExpiresIn,
                        Token = TokenResp.Token
                    };

                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddDays(7);
                    CookieOptions optionsJWT = new CookieOptions();
                    optionsJWT.Expires = DateTime.Now.AddMinutes(20);
                    Response.Cookies.Append("RefreshToken", rToken, options);
                    Response.Cookies.Append("AccessToken", rToken, optionsJWT);
                    Response.Cookies.Append("UserName", user.Entity.UserName, options);

                    foreach (var d in user.Entity.Devices)
                    {
                        d.RefreshToken = null;
                    }

                    responseDTO.Message = "Account Created Successfully.";
                    responseDTO.Success = true;
                    user.Entity.Password = null;
                    user.Entity.PasswordSalt = null;
                    responseDTO.Data = user.Entity;
                    return Ok(responseDTO);
                }
                else
                {
                    responseDTO.Message = "Failed to create new Account.";
                    responseDTO.Success = false;
                    return Ok(responseDTO);

                }
            }
            catch (Exception e)
            {
                responseDTO.Message = "Something went wrong :" + e.Message;
                responseDTO.Success = false;
                return BadRequest(responseDTO);

            }
        }


        [HttpPost("login"), AllowAnonymous]
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
                            db.SaveChanges();

                            CookieOptions options = new CookieOptions();
                            options.Expires = DateTime.Now.AddDays(7);
                            CookieOptions optionsJWT = new CookieOptions();
                            optionsJWT.Expires = DateTime.Now.AddMinutes(20);
                            Response.Cookies.Append("RefreshToken", rToken, options);
                            Response.Cookies.Append("AccessToken", TokenResp.Token, optionsJWT);
                            Response.Cookies.Append("UserName", User.UserName, options);

                            foreach (var d in User.Devices)
                            {
                                d.RefreshToken = null;
                            }

                            responseDTO.Message = "Logged in Successfully.";
                            responseDTO.Success = true;
                            User.Password = null;
                            User.PasswordSalt = null;
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
                            GenerateTokenRequestDTO tokenData = new GenerateTokenRequestDTO()
                            {
                                UserName = User.UserName,
                                Role = User.Role
                            };

                            var TokenResp = JWT.GenerateJwtToken(tokenData);


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
                            db.SaveChanges();

                            CookieOptions options = new CookieOptions();
                            options.Expires = DateTime.Now.AddDays(7);
                            CookieOptions optionsJWT = new CookieOptions();
                            optionsJWT.Expires = DateTime.Now.AddMinutes(20);
                            Response.Cookies.Append("RefreshToken", rToken, options);
                            Response.Cookies.Append("AccessToken", TokenResp.Token, optionsJWT);
                            Response.Cookies.Append("UserName", User.UserName, options);

                            foreach (var d in User.Devices)
                            {
                                d.RefreshToken = null;
                            }

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
                            GenerateTokenRequestDTO tokenData = new GenerateTokenRequestDTO()
                            {
                                UserName = User.UserName,
                                Role = User.Role
                            };

                            var TokenResp = JWT.GenerateJwtToken(tokenData);


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
                            db.SaveChanges();

                            CookieOptions options = new CookieOptions();
                            options.Expires = DateTime.Now.AddDays(7);
                            CookieOptions optionsJWT = new CookieOptions();
                            optionsJWT.Expires = DateTime.Now.AddMinutes(20);
                            Response.Cookies.Append("RefreshToken", rToken, options);
                            Response.Cookies.Append("AccessToken", TokenResp.Token, optionsJWT);
                            Response.Cookies.Append("UserName", User.UserName, options);

                            foreach (var d in User.Devices)
                            {
                                d.RefreshToken = null;
                            }

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


        [HttpPost("check/username"), AllowAnonymous]
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

        [HttpPut("user/{UserId}"), Authorize]
        public async Task<ActionResult<ResponseDTO>> UpdateProfile(Guid UserId, UpdateProfileRequestDTO req)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var user = db.Users.FirstOrDefault(u => u.UserId == UserId);

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found";
                    return Ok(response);
                }

                if (!string.IsNullOrWhiteSpace(req.Email))
                {
                    var EmailExistance = db.Users.FirstOrDefault(u => u.Email == req.Email);
                    if (EmailExistance != null)
                    {
                        response.Success = false;
                        response.Message = "Email already in use";
                        return Ok(response);
                    }
                    user.Email = req.Email;
                }

                if (!string.IsNullOrWhiteSpace(req.PhoneNumber))
                {
                    var PhoneExistance = db.Users.FirstOrDefault(u => u.PhoneNumber == req.PhoneNumber);
                    if (PhoneExistance != null)
                    {
                        response.Success = false;
                        response.Message = "Email already in use";
                        return Ok(response);
                    }
                    user.PhoneNumber = req.PhoneNumber;
                }

                if (!string.IsNullOrWhiteSpace(req.UserName))
                {
                    var UserNameExistance = db.Users.FirstOrDefault(u => u.UserName == req.UserName);
                    if (UserNameExistance != null)
                    {
                        response.Success = false;
                        response.Message = "Email already in use";
                        return Ok(response);
                    }
                    user.UserName = req.UserName;
                }
                if (!string.IsNullOrWhiteSpace(req.Avatar))
                {
                    ImageUpload Uploader = new ImageUpload();

                    var img = await Uploader.Upload(req.Avatar);

                    if (img == null)
                    {
                        response.Message = "Image Upload failed.";
                        response.Success = false;
                        return BadRequest(response);
                    }
                    user.AvatarURL = img.Url.ToString();
                    user.AvatarPublicID = img.PublicId;
                }

                user.Name = !string.IsNullOrWhiteSpace(req.Name) ? req.Name : user.Name;
                user.UserName = !string.IsNullOrWhiteSpace(req.UserName) ? req.UserName : user.UserName;
                user.Bio = !string.IsNullOrWhiteSpace(req.Bio) ? req.Bio : user.Bio;
                user.Gender = !string.IsNullOrWhiteSpace(req.Gender) ? req.Gender : user.Gender;
                user.Birthdate = !string.IsNullOrWhiteSpace(req.Birthdate) ? req.Birthdate : user.Birthdate;
                user.Status = !string.IsNullOrWhiteSpace(req.Status) ? req.Status : user.Status;

                db.SaveChanges();

                response.Message = "User Updated";
                response.Success = true;
                response.Data = user;
                return Ok(response);

            }
            catch (Exception e)
            {
                response.Message = "Internal server error";
                response.Success = false;
                return BadRequest(response);
            }

        }

        [HttpPut("user/pass/{UserId}"), Authorize]
        public async Task<ActionResult<ResponseDTO>> UpdatePassword(Guid UserId, UpdatePasswordRequestDTO req)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                if (string.IsNullOrWhiteSpace(req.CurrentPassword) || string.IsNullOrWhiteSpace(req.NewPassword) || string.IsNullOrWhiteSpace(req.ConfirmPassword))
                {
                    response.Success = false;
                    response.Message = "Please fill all the Data";
                    return Ok(response);
                }

                if (string.Equals(req.NewPassword, req.ConfirmPassword))
                {
                    response.Success = false;
                    response.Message = "both Passwords doesn't match";
                    return Ok(response);
                }

                var user = db.Users.FirstOrDefault(u => u.UserId == UserId);

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found";
                    return Ok(response);
                }

                var salt = PasswordUtils.GeneratePasswordSalt(10);
                var encPassword = PasswordUtils.EncodePassword(req.NewPassword, salt);

                user.PasswordSalt = salt;
                user.Password = encPassword;

                db.SaveChanges();

                response.Message = "Password Updated";
                response.Success = true;
                return Ok(response);

            }
            catch (Exception e)
            {
                response.Message = "Internal server error";
                response.Success = false;
                return BadRequest(response);
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

                    ResponseDTO postsResponse = await httpClient.GetFromJsonAsync<ResponseDTO>("https://localhost:7202/api/v1/thread/user/" + dbUser.UserId);

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

        [HttpGet("token")]
        public async Task<ActionResult<ResponseDTO>> getNewAccessToken()
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var UserName = Request.Cookies["UserName"];
                var RefreshToken = Request.Cookies["RefreshToken"];
                var user = db.Users.FirstOrDefault(u => string.Equals(u.UserName, UserName));

                var tokenExistance = user.Devices.FirstOrDefault(t => string.Equals(t.RefreshToken, RefreshToken));
                if (user != null)
                {
                    GenerateTokenRequestDTO tokenData = new GenerateTokenRequestDTO()
                    {
                        UserName = user.UserName,
                        Role = user.Role
                    };

                    var TokenResp = JWT.GenerateJwtToken(tokenData);



                    CookieOptions optionsJWT = new CookieOptions();
                    optionsJWT.Expires = DateTime.Now.AddMinutes(20);

                    Response.Cookies.Append("AccessToken", TokenResp.Token, optionsJWT);

                    response.Success = true;
                    response.Message = "Token Sent";
                    return Ok(response);

                }
                response.Success = false;
                response.Message = "Token Expired";
                return Ok(response);
            }
            catch (Exception e)
            {
                response.Message = "Something went wrong :" + e.Message;
                response.Success = false;
                return BadRequest(response);
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
