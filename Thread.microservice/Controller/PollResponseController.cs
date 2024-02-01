using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Thread.Data;
using Thread.microservice.Model;
using UserApi.microservice.Models.DTOs;

namespace Thread.microservice.Controller
{
    [Route("api/v1/poll")]
    [ApiController, Authorize]
    public class PollResponseController : ControllerBase
    {

        private readonly DBcontext db;

        public PollResponseController(DBcontext _db)
        {
            db = _db;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> responseToPoll(PollResponseDTO req)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                if (req.OptionId != null)
                {
                    PollResponseModel pollRes = new()
                    {
                        OptionId = req.OptionId,
                        ThreadId = req.ThreadId,
                        UserId = req.UserId
                    };

                    var poll = await db.PollResponses.AddAsync(pollRes);
                    await db.SaveChangesAsync();

                    if (poll != null)
                    {
                        response.Message = "Poll responded.";
                        response.Success = true;
                        return Ok(response);
                    }
                    response.Message = "Poll responding Failed.";
                    response.Success = false;
                    return BadRequest(response);
                }
                else
                {
                    var poll = db.PollResponses.FirstOrDefault(poll => string.Equals(poll.UserId, req.UserId) && string.Equals(poll.ThreadId, req.ThreadId));
                    db.PollResponses.Remove(poll);
                    db.SaveChanges();

                    response.Message = "Poll response deleted.";
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
