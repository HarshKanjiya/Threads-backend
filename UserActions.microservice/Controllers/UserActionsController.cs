using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using UserActions.microservice.Data;
using UserActions.microservice.Models;
using UserActions.microservice.Models.DTOs;

namespace UserActions.microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserActionsController : ControllerBase
    {
        private readonly DBcontext db;
        public UserActionsController(DBcontext _db) { db = _db; }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> LikeDislikeAction(LikeDislikeRequestDTO req)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                if (req.Status == "LIKE")
                {
                    // create like
                    LikeModel newLike = new()
                    {
                        ThreadId = req.ThreadId,
                        UserId = req.UserId,
                    };
                    var LikeAction = await db.Likes.AddAsync(newLike);
                    await db.SaveChangesAsync();

                    if (LikeAction != null)
                    {
                        response.Message = "Thread Liked";
                        response.Success = true;
                        return Ok(response);
                    }
                }
                else
                {
                    var LikeAction = db.Likes.FirstOrDefault(like => string.Equals(like.UserId, req.UserId) && string.Equals(like.ThreadId, req.ThreadId));
                    db.Likes.Remove(LikeAction);
                    db.SaveChanges();

                    response.Message = "Thread Disliked";
                    response.Success = true;
                    return Ok(response);
                }

                response.Message = "Something went wrong";
                response.Success = false;
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.Message = "Internal server error.";
                response.Success = false;
                return BadRequest(response);
            }
        }

        public async Task<ActionResult<ResponseDTO>> addRemoveFollowUser(FollowUserRequestDTO req)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                if (req.Type == "FOLLOW")
                {
                    RelationshipModel relationship = new()
                    {
                        CasterId = req.CasterId,
                        ReceiverId = req.ReceiverId,
                        Type = "FOLLOW",
                    };
                    var Follow = await db.Relationships.AddAsync(relationship);
                    await db.SaveChangesAsync();

                    if (Follow != null)
                    {
                        response.Message = "Followed";
                        response.Success = true;
                        return BadRequest(response);
                    }
                }
                else
                {
                    var follow = db.Relationships.FirstOrDefault(f => string.Equals(f.CasterId, req.CasterId) && string.Equals(f.ReceiverId, req.ReceiverId));
                    db.Relationships.Remove(follow);
                    db.SaveChanges();

                    response.Message = "Unfollowed";
                    response.Success = true;
                    return BadRequest(response);
                }

                response.Message = "Something went wrong";
                response.Success = false;
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.Message = "Internal server error.";
                response.Success = false;
                return BadRequest(response);
            }
        }

        public async Task<ActionResult<ResponseDTO>> MutingUsers(FollowUserRequestDTO req)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                if (req.Type == "MUTE")
                {
                    RelationshipModel relationship = new()
                    {
                        CasterId = req.CasterId,
                        ReceiverId = req.ReceiverId,
                        Type = "MUTE",
                    };
                    var Mute = await db.Relationships.AddAsync(relationship);
                    await db.SaveChangesAsync();

                    if (Mute != null)
                    {
                        response.Message = "Muted";
                        response.Success = true;
                        return BadRequest(response);
                    }
                }
                else
                {
                    var Mute = db.Relationships.FirstOrDefault(f => string.Equals(f.CasterId, req.CasterId) && string.Equals(f.ReceiverId, req.ReceiverId));
                    db.Relationships.Remove(Mute);
                    db.SaveChanges();

                    response.Message = "Unmuted";
                    response.Success = true;
                    return BadRequest(response);
                }

                response.Message = "Something went wrong";
                response.Success = false;
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.Message = "Internal server error.";
                response.Success = false;
                return BadRequest(response);
            }
        }

        public async Task<ActionResult<ResponseDTO>> BlockingUsers(FollowUserRequestDTO req)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                if (req.Type == "BLOCK")
                {
                    RelationshipModel relationship = new()
                    {
                        CasterId = req.CasterId,
                        ReceiverId = req.ReceiverId,
                        Type = "BLOCK",
                    };
                    var Block = await db.Relationships.AddAsync(relationship);
                    await db.SaveChangesAsync();

                    if (Block != null)
                    {
                        response.Message = "Blocked";
                        response.Success = true;
                        return BadRequest(response);
                    }
                }
                else
                {
                    var Block = db.Relationships.FirstOrDefault(f => string.Equals(f.CasterId, req.CasterId) && string.Equals(f.ReceiverId, req.ReceiverId));
                    db.Relationships.Remove(Block);
                    db.SaveChanges();

                    response.Message = "Unblocked";
                    response.Success = true;
                    return BadRequest(response);
                }

                response.Message = "Something went wrong";
                response.Success = false;
                return BadRequest(response);
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