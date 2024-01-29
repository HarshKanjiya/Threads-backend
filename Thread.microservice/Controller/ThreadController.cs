using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Threading;
using Thread.Data;
using Thread.Model;
using UserApi.microservice.Models.DTOs;

namespace Thread.microservice.Controller
{
    [Route("thread")]
    [ApiController]
    public class ThreadController : ControllerBase
    {

        private readonly DBcontext db;
        private readonly HttpClient httpClient;

        public ThreadController(DBcontext _db, HttpClient _httpClient)
        {
            db = _db;
            httpClient = _httpClient;
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
                    newContent.Files = new List<string> {
                        "https://images.unsplash.com/photo-1682686581312-506a8b53190e?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDF8MHxlZGl0b3JpYWwtZmVlZHw2fHx8ZW58MHx8fHx8", "https://images.unsplash.com/photo-1682686581312-506a8b53190e?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDF8MHxlZGl0b3JpYWwtZmVlZHw2fHx8ZW58MHx8fHx8",
                    };
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

                await httpClient.PutAsJsonAsync("https://localhost:7201/auth/user/", dataForAuthApi);

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


        [HttpPost, Route("/file")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> FileUpload(IFormFile file)
        {
            return Ok();
        }


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


        [HttpDelete("deleteall")]
        public async Task<ActionResult> DeleteAll()
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                foreach(var thread in db.Threads)
                {
                    db.Threads.Remove(thread);
                }
                db.SaveChanges();

                response.Message = "All posts deleted";
                response.Success = true;
                return Ok(response);
            }
            catch(Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
                return BadRequest(response);
            }
        }
    }
}
