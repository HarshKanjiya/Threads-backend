using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Thread.Data;
using UserApi.microservice.Models.DTOs;

namespace Thread.microservice.Controller
{
    [Route("api/v1/service/thread")]
    [ApiController]
    public class ServiceCommunication : ControllerBase
    {
        private readonly DBcontext db;
        public ServiceCommunication(DBcontext _db)
        {
            db = _db;
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<ResponseDTO>> getThreadsOfUser(Guid id)
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
    }
}
