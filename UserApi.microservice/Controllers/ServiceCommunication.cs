using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserApi.microservice.Data;
using UserApi.microservice.Models.DTOs;

namespace UserApi.microservice.Controllers
{
    [Route("api/service")]
    [ApiController]
    public class ServiceCommunication : ControllerBase
    {
        private readonly DbContextUsers db;
        private readonly HttpClient httpClient;
        private readonly ILogger<AuthController> logger;
        public ServiceCommunication(DbContextUsers _db, HttpClient _httpClient, ILogger<AuthController> _logger)
        {
            db = _db;
            httpClient = _httpClient;
            logger = _logger;
        }



        [HttpPut("user")]
        public async Task<ActionResult<ResponseDTO>> UpdateUserData(UserDataFieldUpdateDTO data)
        {
            ResponseDTO responseDTO = new ResponseDTO();

            try
            {
                var user = db.Users.FirstOrDefault(u => u.UserId == data.UserId);

                if (user != null)
                {
                    switch (data.message)
                    {
                        case "ADD_THREAD":
                            user.PostsCount += 1;
                            db.SaveChanges();
                            break;
                        case "REMOVE_THREAD":
                            user.PostsCount -= 1;
                            db.SaveChanges();
                            break;

                        case "ADD_FOLLOWER":
                            user.FollowersCount += 1;
                            break;
                        case "REMOVE_FOLLOWER":
                            user.FollowersCount -= 1;
                            break;

                        case "ADD_FOLLOWING":
                            user.FollowingCount += 1;
                            break;
                        case "REMOVE_FOLLOWING":
                            user.FollowingCount -= 1;
                            break;
                    }



                    responseDTO.Message = "User Updated";
                    responseDTO.Success = true;
                    return Ok(responseDTO);
                }
                responseDTO.Message = "User not found";
                responseDTO.Success = false;
                return Ok(responseDTO);

            }
            catch (Exception e)
            {
                responseDTO.Message = "Internal Server Error : Auth Service " + e.Message;
                responseDTO.Success = false;
                return BadRequest(responseDTO);
            }
        }

        [HttpGet("Userdata/{UserId}")]
        public async Task<ActionResult<ResponseDTO>> getUserInformation(Guid UserId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                var user = db.Users.FirstOrDefault(u => u.UserId == UserId);
                if (user != null)
                {
                    responseDTO.Message = "User Found";
                    responseDTO.Success = true;
                    responseDTO.Data = user;
                    return Ok(responseDTO);
                }
                responseDTO.Message = "User not found";
                responseDTO.Success = false;
                return Ok(responseDTO);
            }
            catch (Exception e)
            {
                responseDTO.Message = "Internal Server Error : Auth Service " + e.Message;
                responseDTO.Success = false;
                return BadRequest(responseDTO);
            }
        }


    }
}
