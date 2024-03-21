using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Threading;
using Thread.Data;
using Thread.Model;
using UserApi.microservice.Models.DTOs;

namespace Thread.microservice.Controller
{
    [Route("api/v1/thread/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly DBcontext db;
        private readonly HttpClient httpClient;

        public AdminController(DBcontext _db, HttpClient _httpClient)
        {
            db = _db;
            httpClient = _httpClient;
        }

        [HttpGet("user/{UserId}/{ThreadCount}")]
        public async Task<ActionResult<ResponseDTO>> GetThread(Guid UserId, int ThreadCount)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var threads = db.Threads
                    .Include(t => t.Content).ThenInclude(t => t.Ratings)
                    .Include(t => t.Content).ThenInclude(t => t.Options)
                    .OrderByDescending(x => x.CreatedAt)
                    .Where(t => t.AuthorId == UserId && t.Content.ContentType == "TEXT")
                    .Take(ThreadCount)
                    .ToList();

                var threadCount = db.Threads.Where(t => t.AuthorId == UserId).ToList().Count;


                PostResponseDto postResponse = new PostResponseDto();
                postResponse.TotalCount = threadCount;
                postResponse.Posts = threads;


                response.Message = "Threads Found.";
                response.Success = true;
                response.Data = postResponse;
                return Ok(response);


            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Internal Server error :" + e.Message;
                return BadRequest(response);
            }
        }
        public class PostResponseDto
        {
            public List<ThreadModel> Posts { get; set; }
            public int TotalCount { get; set; }
        }
    }
}
