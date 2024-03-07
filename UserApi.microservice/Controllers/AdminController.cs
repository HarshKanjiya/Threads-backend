using Microsoft.AspNetCore.Mvc;
using UserApi.microservice.Data;
using UserApi.microservice.Models.DTOs;

namespace UserApi.microservice.Controllers
{
    [Route("api/v1/auth/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly DbContextUsers db;

        public AdminController(DbContextUsers _db)
        {
            db = _db;
        }


        [HttpGet("users/{type}")]
        public async Task<ActionResult<ResponseDTO>> getAll(string type)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {

                if (type == "all")
                {
                    var users = db.Users.OrderByDescending(u => u.CreatedAt).ToList();

                    if (users == null)
                    {
                        responseDTO.Message = "users does not exist.";
                        responseDTO.Success = false;

                        return Ok(responseDTO);

                    }

                    responseDTO.Success = true;
                    responseDTO.Data = users;
                    responseDTO.Message = "users foubnd.";
                    return Ok(responseDTO);
                }
                if(type == "unban")
                {
                    var users = db.Users.Where(u => u.BanStatus == "UNBAN").OrderByDescending(u => u.CreatedAt).ToList();

                    if (users == null)
                    {
                        responseDTO.Message = "users does not exist.";
                        responseDTO.Success = false;

                        return Ok(responseDTO);

                    }

                    responseDTO.Success = true;
                    responseDTO.Data = users;
                    responseDTO.Message = "users foubnd.";
                    return Ok(responseDTO);
                }
                if (type == "ban")
                {
                    var users = db.Users.Where(u => u.BanStatus == "BAN").OrderByDescending(u => u.CreatedAt).ToList();

                    if (users == null)
                    {
                        responseDTO.Message = "users does not exist.";
                        responseDTO.Success = false;
                        return Ok(responseDTO);
                    }
                    responseDTO.Success = true;
                    responseDTO.Data = users;
                    responseDTO.Message = "users foubnd.";
                    return Ok(responseDTO);
                }

                responseDTO.Success = false;
                responseDTO.Message = "Invalid choice";
                return Ok(responseDTO);

            }
            catch (Exception e)
            {
                responseDTO.Message = "Something went wrong";
                responseDTO.Success = false;
                return BadRequest(responseDTO);


            }
        }

        [HttpGet("user/{type}")]
        public async Task<ActionResult<ResponseDTO>> getsingle(Guid UserId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                var user = db.Users.FirstOrDefault(u => u.UserId == UserId);
             
                if (user == null)
                {
                    responseDTO.Message = "user does not exist.";
                    responseDTO.Success = false;
                    return Ok(responseDTO);
                }
                responseDTO.Success = true;
                responseDTO.Data = user;
                responseDTO.Message = "users found.";
                return Ok(responseDTO);
                        

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
