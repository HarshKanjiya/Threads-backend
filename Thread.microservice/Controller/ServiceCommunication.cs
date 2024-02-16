using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Threading;
using Thread.Data;
using UserApi.microservice.Models.DTOs;

namespace Thread.microservice.Controller
{
    [Route("api/v1/service/thread")]
    [ApiController,AllowAnonymous]
    public class ServiceCommunication : ControllerBase
    {
        private readonly DBcontext db;
        private readonly HttpClient httpClient;

        public ServiceCommunication(DBcontext _db, IHttpClientFactory httpClientFactory)
        {
            db = _db;
            httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet("user/{RequesterId}/{id}")]
        public async Task<ActionResult<ResponseDTO>> getThreadsOfUser(Guid RequesterId,Guid id)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var threads = db.Threads
                    .Include(Thread => Thread.Content)
                    .ThenInclude(t=>t.Ratings)
                    .Include(Thread => Thread.Content)
                    .ThenInclude(t=>t.Options)
                    .Where(t => string.Equals(t.AuthorId,id))
                    .ToList();

                List<ThreadResponseDTO> threadsRes = new List<ThreadResponseDTO>();
                foreach (var thread in threads)
                {
                    ThreadResponseDTO temp = new()
                    {
                        Content = thread.Content,
                        AuthorId = thread.AuthorId,
                        BanStatus = thread.BanStatus,
                        CreatedAt = thread.CreatedAt,
                        Likes = thread.Likes,
                        ReferenceId = thread.ReferenceId,
                        Replies = thread.Replies,
                        ReplyAccess = thread.ReplyAccess,
                        ThreadId = thread.ThreadId,
                        Type = thread.Type,
                    };
                    if (RequesterId != null)
                    {
                        ResponseDTO res = await httpClient.GetFromJsonAsync<ResponseDTO>("https://localhost:7203/api/v1/service/action/like/" + RequesterId + "/" + thread.ThreadId);

                        if (res.Success)
                        {
                            if (res.Message == "notliked")
                            {
                                temp.LikedByMe = false;
                            }
                            else
                            {
                                temp.LikedByMe = true;
                            }
                        }

                    }

                    threadsRes.Add(temp);
                }


                response.Success = true;
                response.Message = "Posts found";
                response.Data = threadsRes;
                return Ok(response);

            }
            catch (Exception ex)
            {
                response.Message = "Internal server error.   " + ex.Message;
                response.Success = false;
                return BadRequest(response);
            }

        }

        [HttpGet("thread/{id}")]
        public async Task<ActionResult<ResponseDTO>> getThreadInfo(Guid id)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                var thread = db.Threads.FirstOrDefault(t => t.ThreadId == id);
                if (thread != null)
                {
                    response.Success = true;
                    response.Message = "Thread found";
                    response.Data = thread;
                    return Ok(response);
                }
                response.Success = false;
                response.Message = "Thread not found";
                return Ok(response);

            }
            catch (Exception e)
            {
                response.Message = "Internal server error.   " + e.Message;
                response.Success = false;
                return BadRequest(response);
            }
        }

        [HttpGet("likecount/{type}/{id}")]
        public async Task<ActionResult<ResponseDTO>> manageLikeCount(string type,Guid id)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {

                var thread = db.Threads.FirstOrDefault(t => t.ThreadId == id);
                if (thread != null)
                {
                    if (type == "add")
                    {
                        thread.Likes += 1;
                        db.SaveChanges();
                    }

                    if (type == "less")
                    {
                        thread.Likes -= 1;
                        db.SaveChanges();
                    }

                    response.Success = true;
                    response.Message = "count managed";
                    return Ok(response);
                }
                
                response.Success = false;
                response.Message = "Thread not found";
                return Ok(response);

            }
            catch (Exception e)
            {
                response.Message = "Internal server error.   " + e.Message;
                response.Success = false;
                return BadRequest(response);
            }
        }

    }
}
