using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Text;
using Thread.Data;
using Thread.Model;
using UserApi.microservice.Models.DTOs;
using Thread.microservice.Utils;
using Azure;

namespace Thread.microservice.Controller
{
    [Route("thread")]
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
        public async Task<ActionResult<ResponseDTO>> CreateThread([FromBody] CreateRequestDTO req)
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
                    Content = newContent,
                };

                var thread = await db.Threads.AddAsync(newThreadData);
                await db.SaveChangesAsync();

                var dataForAuthApi = JsonConvert.SerializeObject(
                    new
                    {
                        UserId = req.UserId,
                        message = "ADD_THREAD"
                    });

                var content = new StringContent(dataForAuthApi.ToString(), Encoding.UTF8, "application/json");
                var res = httpClient.PutAsync("https://localhost:7201/api/service/user", content).Result;


                if (thread != null)
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDTO>> DeleteThread(Guid id)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var thread = db.Threads.FirstOrDefault(t => t.ThreadId == id);

                if (thread != null)
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

        [HttpPost, Route("/file")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> FileUpload(IFormFile file)
        {
            return Ok();
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



        // FOR OTHER SERVICES
        [HttpGet("user/{id}")]
        public async Task<ActionResult<ResponseDTO>> getThreadsOfUser(string id)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var threads = db.Threads.Include(Thread => Thread.Content.Ratings).Include(Thread => Thread.Content.Options).Where(t => t.AuthorId == id).ToList();
                response.Success = true;
                response.Message = "Posts found";
                response.Data = threads;
                return Ok(response);

            }
            catch (Exception ex)
            {
                response.Message = "Internal server error.   " + ex.Message;
                response.Success = false;
                return BadRequest(response);
            }

        }

        [HttpGet("threadInfo/{id}")]
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

    }
}
