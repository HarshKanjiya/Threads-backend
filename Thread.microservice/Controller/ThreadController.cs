﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Thread.Data;
using Thread.Model;
using UserApi.microservice.Models.DTOs;
using Thread.microservice.Utils;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Thread.microservice.Controller
{
    [Route("api/v1/thread")]
    [ApiController]
    public class ThreadController : ControllerBase
    {

        private readonly DBcontext db;
        private readonly HttpClient httpClient;
        private readonly ILogger<ThreadController> logger;

        public ThreadController(DBcontext _db, HttpClient _httpClient, ILogger<ThreadController> _logger)
        {
            db = _db;
            httpClient = _httpClient;
            logger = _logger;
        }


        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> CreateThread(CreateRequestDTO req)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                ThreadContent newContent = new ThreadContent()
                {
                    Text = req.Content.Text,
                    ContentType = req.Content.ContentType
                };

                if (req.Content.ContentType == "TEXT")
                {

                    if (req.Content.Files.Count > 0)
                    {

                        ImageUpload Uploader = new ImageUpload();
                        List<string> files = new List<string>();
                        List<string> filesPublicIDs = new List<string>();

                        foreach (var x in req.Content.Files)
                        {
                            var img = await Uploader.Upload(x);
                            if (img == null)
                            {
                                response.Message = "Image Upload failed.";
                                response.Success = false;
                                return BadRequest(response);

                            }
                            files.Add(img.Url.ToString());
                            filesPublicIDs.Add(img.PublicId);

                        }
                        newContent.Files = files;
                        newContent.FilePublicIDs = filesPublicIDs;
                    }
                }
                else
                {
                    newContent.Options = req.Content?.Options?.Select(opt => new ThreadContentOptions()
                    {
                        Option = opt.Option,
                        Value = opt.Value
                    }).ToList();


                    if (req.Content?.Options?.Count != 0)
                    {
                        var _temp = new List<int>();
                        foreach (var item in req.Content.Options)
                        {
                            _temp.Add(0);
                        }
                        newContent.Ratings = new ThreadContentRatings()
                        {
                            TotalResponse = 0,
                            Responses = _temp
                        };
                    }
                }

                ThreadModel newThreadData = new()
                {
                    AuthorId = req.UserId,
                    ReplyAccess = req.ReplyAccess,
                    Type = req.Type,
                    Content = newContent
                };

                if (req.Type != "PARENT")
                {
                    newThreadData.ReferenceId = req.ReferenceId;
                }

                var thread = await db.Threads.AddAsync(newThreadData);
                await db.SaveChangesAsync();

                if (req.Type == "REPLY")
                {
                    var replyUpdate = db.Threads.FirstOrDefault(t => t.ThreadId.ToString() == req.ReferenceId);
                    if (replyUpdate != null)
                    {
                        replyUpdate.Replies += 1;
                        db.SaveChanges();
                    }
                }

                var dataForAuthApi = JsonConvert.SerializeObject(
                    new
                    {
                        UserId = req.UserId,
                        message = "ADD_THREAD"
                    });

                var content = new StringContent(dataForAuthApi.ToString(), Encoding.UTF8, "application/json");
                var res = httpClient.PutAsync("https://localhost:7201/api/v1/service/auth/usercounts", content).Result;


                if (thread.Entity != null)
                {
                    response.Message = "Thread Created";
                    response.Success = true;
                    response.Data = thread.Entity;
                    return Ok(response);
                }
                else
                {
                    response.Message = "Thread Creatio failed!";
                    response.Success = false;
                    return BadRequest(response);
                }

            }
            catch (Exception ex)
            {
                response.Message = "Internal server error.   " + ex.Message;
                response.Success = false;
                return BadRequest(response);
            }
        }

        [HttpGet("{ThreadId}")]
        public async Task<ActionResult<ResponseDTO>> GetThread(Guid ThreadId)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var thread = db.Threads
                    .Include(t => t.Content).ThenInclude(t => t.Ratings)
                    .Include(t => t.Content).ThenInclude(t => t.Options)
                    .FirstOrDefault(t => t.ThreadId == ThreadId);

                if (thread != null)
                {
                    response.Success = true;
                    response.Message = "Thread found";
                    response.Data = thread;
                    return Ok(response);
                }
                response.Message = "Please try again.";
                response.Success = false;
                return BadRequest(response);


            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Internal Server error :" + e.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("replies/{ThreadId}")]
        public async Task<ActionResult<ResponseDTO>> GetThreadReplies(Guid ThreadId)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                string REPLY = "REPLY";
                var threads = db.Threads
                    .Include(t => t.Content).ThenInclude(t => t.Ratings)
                    .Include(t => t.Content).ThenInclude(t => t.Options)
                    .Where(t => (t.ReferenceId == ThreadId.ToString()) && (t.Type == REPLY));

                if (threads != null)
                {
                    response.Success = true;
                    response.Message = "Replies found";
                    response.Data = threads;
                    return Ok(response);
                }
                response.Message = "Please try again.";
                response.Success = false;
                return BadRequest(response);


            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Internal Server error :" + e.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("feed/{UserId}")]
        public async Task<ActionResult<ResponseDTO>> GetFeed(Guid UserId)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                string REPLY = "REPLY";

                var threads = db.Threads
                    .Include(t => t.Content).ThenInclude(t => t.Ratings)
                    .Include(t => t.Content).ThenInclude(t => t.Options)
                    .Where(t => (t.Type != REPLY));

                if (threads != null)
                {
                    response.Success = true;
                    response.Message = "feed found";
                    response.Data = threads;
                    return Ok(response);
                }
                response.Message = "Please try again.";
                response.Success = false;
                return BadRequest(response);


            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Internal Server error :" + e.Message;
                return BadRequest(response);
            }
        }


        [HttpDelete("{UserId}/{ThreadId}")]
        public async Task<ActionResult<ResponseDTO>> DeleteThread(Guid UserId, Guid ThreadId)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var thread = db.Threads.FirstOrDefault(t => t.ThreadId == ThreadId);

                if (thread != null)
                {

                    if (thread.AuthorId == UserId)
                    {
                        db.Threads.Remove(thread);
                        db.SaveChanges();

                        var dataForAuthApi = JsonConvert.SerializeObject(
                            new
                            {
                                UserId = thread.AuthorId,
                                message = "REMOVE_THREAD"
                            });

                        var content = new StringContent(dataForAuthApi.ToString(), Encoding.UTF8, "application/json");
                        var res = httpClient.PutAsync("https://localhost:7201/Auth/user", content).Result;

                        response.Message = "Thread Removed.";
                        response.Success = true;
                        return Ok(response);
                    }
                    else
                    {
                        response.Message = "You don't have right to perform this action.";
                        response.Success = true;
                        return Ok(response);
                    }


                }
                response.Message = "Please try again.";
                response.Success = false;
                return BadRequest(response);


            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Internal Server error :" + e.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("user/{UserId}")]
        public async Task<ActionResult<ResponseDTO>> GetUsersPosts(Guid UserId)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                string postType = Request.Query["type"];
                int pageSize = int.Parse(Request.Query["pageSize"]);
                int pageNumber = int.Parse(Request.Query["pageNumber"]);


                var threads = db.Threads
                    .OrderByDescending(t => t.CreatedAt)
                       .Include(t => t.Content).ThenInclude(t => t.Ratings)
                       .Include(t => t.Content).ThenInclude(t => t.Options)
                    .Where(t => string.Equals(t.AuthorId, UserId) && string.Equals(t.Type, postType)).ToList();


                if (threads == null)
                {
                    response.Message = "Threads couldn't be Found.";
                    response.Success = false;
                    return Ok(response);
                }
                else
                {
                    response.Success = true;
                    response.Message = "Threads found";
                    response.Data = threads;
                    return Ok(response);
                }

            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Internal Server error :" + e.Message;
                return BadRequest(response);
            }
        }


        [HttpDelete("deleteall")]
        public async Task<ActionResult> DeleteAll()
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                foreach (var thread in db.Threads)
                {
                    db.Threads.Remove(thread);
                }
                foreach (var content in db.Contents)
                {
                    db.Contents.Remove(content);
                }
                foreach (var rating in db.Ratings)
                {
                    db.Ratings.Remove(rating);
                }
                foreach (var option in db.Options)
                {
                    db.Options.Remove(option);
                }
                db.SaveChanges();

                response.Message = "All posts deleted";
                response.Success = true;
                return Ok(response);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Internal Server error :" + e.Message;
                return BadRequest(response);
            }
        }

    }
}
