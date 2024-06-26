﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading;
using UserActions.microservice.Data;
using UserActions.microservice.Models;
using UserActions.microservice.Models.DTOs;

namespace UserActions.microservice.Controllers
{
    [Route("api/v1/action")]
    [ApiController]
    public class UserActionsController : ControllerBase
    {
        private readonly DBcontext db;
        private readonly HttpClient httpClient;

        public UserActionsController(DBcontext _db, IHttpClientFactory httpClientFactory)
        {
            db = _db;
            httpClient = httpClientFactory.CreateClient();
        }

        [HttpPost("like")]
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


                    /* if (LikeAction != null)
                     {
                         //getting thread owner and Caster Details
                         getThreadInfoResponseDTO thread = await httpClient.GetFromJsonAsync<getThreadInfoResponseDTO>("https://localhost:7202/api/v1/service/thread/thread/" + req.ThreadId);
                         getUserInfoResponseDTO caster = await httpClient.GetFromJsonAsync<getUserInfoResponseDTO>("https://localhost:7201/api/v1/service/auth/user/" + req.UserId);


                         if (thread.Success == true && caster.Success == true)
                         {
                             var notificationData = JsonConvert.SerializeObject(
                             new
                             {
                                 Type = "LIKE",
                                 ReceiverId = thread.Data.AuthorId,
                                 CasterId = caster.Data.UserId,
                                 CasterUserName = caster.Data.UserName,
                                 CasterAvatarUrl = caster.Data.AvatarURL,
                                 HelperId = thread.Data.ThreadId
                             });

                             var content = new StringContent(notificationData.ToString(), Encoding.UTF8, "application/json");
                             var res = httpClient.PostAsync("https://localhost:7204/api/v1/service/notification/sendnotif", content).Result;
                         }
                     }*/

                    ResponseDTO likemanage = await httpClient.GetFromJsonAsync<ResponseDTO>("https://localhost:7202/api/v1/service/thread/likecount/add/" + req.ThreadId);


                    response.Message = "Thread Liked";
                    response.Success = true;
                    response.Data = likemanage;
                    return Ok(response);
                }
                else
                {

                    //deleting like
                    var disLikeAction = db.Likes.FirstOrDefault(l => l.ThreadId == req.ThreadId && l.UserId == req.UserId);
                    if (disLikeAction != null)
                    {
                        db.Likes.Remove(disLikeAction);
                        db.SaveChanges();

                        /*  //getting thread owner and Caster Details
                          getThreadInfoResponseDTO thread = await httpClient.GetFromJsonAsync<getThreadInfoResponseDTO>("https://localhost:7202/api/v1/service/thread/thread/" + req.ThreadId);
                          getUserInfoResponseDTO caster = await httpClient.GetFromJsonAsync<getUserInfoResponseDTO>("https://localhost:7201/api/v1/service/auth/user/" + req.UserId);

                          if (thread.Success == true && caster.Success == true)
                          {
                              var notificationData = JsonConvert.SerializeObject(
                              new
                              {
                                  Type = "LIKE",
                                  ReceiverId = thread.Data.AuthorId,
                                  CasterId = caster.Data.UserId,
                                  HelperId = thread.Data.ThreadId
                              });

                              var content = new StringContent(notificationData.ToString(), Encoding.UTF8, "application/json");
                              var res = httpClient.PostAsync("https://localhost:7204/api/v1/service/notification/removenotif", content).Result;
                          }*/
                        ResponseDTO likemanage = await httpClient.GetFromJsonAsync<ResponseDTO>("https://localhost:7202/api/v1/service/thread/likecount/less/" + req.ThreadId);

                        response.Message = "Thread Disliked";
                        response.Success = true;
                        response.Data = likemanage;

                        return Ok(response);
                    }
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

        [HttpPost("follow")]
        public async Task<ActionResult<ResponseDTO>> addRemoveFollowUser(FollowUserRequestDTO req)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                if (req.Type == "FOLLOW")
                {
                    var existance = db.Relationships.FirstOrDefault(x => x.CasterId == req.CasterId && x.ReceiverId == req.ReceiverId && x.Type == "FOLLOW");

                    if (existance == null)
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
                            getUserInfoResponseDTO caster = await httpClient.GetFromJsonAsync<getUserInfoResponseDTO>("https://localhost:7201/api/v1/service/auth/user/" + req.CasterId);

                            if (caster != null)
                            {
                                var notificationData = JsonConvert.SerializeObject(
                                    new
                                    {
                                        Type = "FOLLOW",
                                        ReceiverId = req.ReceiverId,
                                        CasterId = req.CasterId,
                                        CasterUserName = caster.Data.UserName,
                                        CasterAvatarUrl = caster.Data.AvatarURL
                                    });

                                var content = new StringContent(notificationData.ToString(), Encoding.UTF8, "application/json");
                                var res = httpClient.PostAsync("https://localhost:7204/api/v1/service/notification/sendnotif", content).Result;

                            }

                            response.Message = "User Followed";
                            response.Success = true;
                            return Ok(response);
                        }
                    }
                    else
                    {
                        db.Relationships.Remove(existance);
                        db.SaveChanges();
                        response.Message = "User Unfollowed";
                        response.Success = true;
                        return Ok(response);
                    }
                    
                }
                else
                {
                    //deleting 
                    var relation = db.Relationships.FirstOrDefault(r => (r.CasterId == req.CasterId) && (r.ReceiverId == req.ReceiverId) && (r.Type == req.Type));

                    if (relation != null)
                    {

                        db.Relationships.Remove(relation);
                        db.SaveChanges();

                        var notificationData = JsonConvert.SerializeObject(
                            new
                            {
                                Type = "FOLLOW",
                                ReceiverId = req.ReceiverId,
                                CasterId = req.CasterId,
                            });

                        var content = new StringContent(notificationData.ToString(), Encoding.UTF8, "application/json");
                        var res = httpClient.PostAsync("https://localhost:7204/api/v1/service/notification/removenotif", content).Result;



                        response.Message = "Unfollowed";
                        response.Success = true;
                        return BadRequest(response);

                    }

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

        [HttpPost("mute")]
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
                    if (Mute != null)
                    {
                        db.Relationships.Remove(Mute);
                        db.SaveChanges();

                        response.Message = "User Unmuted";
                        response.Success = true;
                        return BadRequest(response);
                    }
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

        [HttpPost("block")]
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
                        response.Message = "User added to blacklist";
                        response.Success = true;
                        return BadRequest(response);
                    }
                }
                else
                {
                    var Block = db.Relationships.FirstOrDefault(f => string.Equals(f.CasterId, req.CasterId) && string.Equals(f.ReceiverId, req.ReceiverId));
                    if (Block != null)
                    {
                        db.Relationships.Remove(Block);
                        db.SaveChanges();

                        response.Message = "User removed from blacklist";
                        response.Success = true;
                        return BadRequest(response);
                    }
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