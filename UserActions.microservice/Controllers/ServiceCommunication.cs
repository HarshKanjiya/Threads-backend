using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using UserActions.microservice.Data;
using UserActions.microservice.Models.DTOs;

namespace UserActions.microservice.Controllers
{
    [Route("api/v1/service/action"), AllowAnonymous]
    [ApiController]
    public class ServiceCommunication : ControllerBase
    {
        private readonly DBcontext db;
        private readonly HttpClient httpClient;

        public ServiceCommunication(DBcontext _db, HttpClient _httpClient)
        {
            db = _db;
            httpClient = _httpClient;
        }

        [HttpGet("like/{UserId}/{ThreadId}")]
        public async Task<ActionResult<ResponseDTO>> LikeDislikeAction(Guid UserId, Guid ThreadId)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                var existance = db.Likes.FirstOrDefault(x => x.UserId == UserId && x.ThreadId == ThreadId);

                if (existance != null)
                {
                    response.Message = "liked";
                    response.Success = true;
                    return Ok(response);
                }
                else
                {
                    response.Message = "notliked";
                    response.Success = true;
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = "Internal server error.";
                response.Success = false;
                return BadRequest(response);
            }
        }

        [HttpPost("follow")]
        public async Task<ActionResult<ResponseDTO>> addRemoveFollowUser(FollowUserRequestDTO req)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                var existance = db.Relationships.FirstOrDefault(x => x.CasterId == req.CasterId && x.ReceiverId == req.ReceiverId && x.Type == req.Type);

                if (existance != null)
                {
                    response.Message = "followed";
                    response.Success = true;
                    return Ok(response);
                }
                else
                {
                    response.Message = "notfollowed";
                    response.Success = true;
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = "Internal server error.";
                response.Success = false;
                return BadRequest(response);
            }
        }

        [HttpPost("mute")]
        public async Task<ActionResult<ResponseDTO>> MutingUsers(FollowUserRequestDTO req)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {

                var existance = db.Relationships.FirstOrDefault(x => x.CasterId == req.CasterId && x.ReceiverId == req.ReceiverId && x.Type == req.Type);

                if (existance != null)
                {
                    response.Message = "muted";
                    response.Success = true;
                    return Ok(response);
                }
                else
                {
                    response.Message = "notmuted";
                    response.Success = true;
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = "Internal server error.";
                response.Success = false;
                return BadRequest(response);
            }
        }

        [HttpPost("block")]
        public async Task<ActionResult<ResponseDTO>> BlockingUsers(FollowUserRequestDTO req)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {

                var existance = db.Relationships.FirstOrDefault(x => x.CasterId == req.CasterId && x.ReceiverId == req.ReceiverId && x.Type == req.Type);

                if (existance != null)
                {
                    response.Message = "blocked";
                    response.Success = true;
                    return Ok(response);
                }
                else
                {
                    response.Message = "notblocked";
                    response.Success = true;
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = "Internal server error.";
                response.Success = false;
                return BadRequest(response);
            }
        }

    }
}
