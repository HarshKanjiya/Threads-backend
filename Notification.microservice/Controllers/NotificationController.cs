using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notification.microservice.data;
using Notification.microservice.Model;
using Notification.microservice.Models.DTOs;

namespace Notification.microservice.Controllers
{
    [Route("api/v1/notification")]
    [ApiController, Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly DBcontext db;

        public NotificationController(DBcontext _db)
        {
            db = _db;
        }


        // FOR END USER
        [HttpGet("getall/{UserId}")]
        public async Task<ActionResult<ResponseDTO>> GetMyNotifications(Guid UserId)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var notifications = db.Notifications.Where(n => n.ReceiverId == UserId).ToList();

                response.Success = true;
                response.Message = "Notifications Sent";
                response.Data = notifications;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Internal server error";
                return BadRequest(response);

            }
        }

        [HttpPost("markseen")]
        public async Task<ActionResult<ResponseDTO>> MarkAsSeen(List<Guid> notifications)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                foreach (var x in notifications)
                {
                    var notif = db.Notifications.FirstOrDefault(n => n.NotificationId == x);
                    if (notif != null) notif.Seen = true;
                }
                db.SaveChanges();

                response.Success = true;
                response.Message = "Notifications Sent";
                response.Data = notifications;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Internal server error";
                return BadRequest(response);

            }
        }




    }
}
