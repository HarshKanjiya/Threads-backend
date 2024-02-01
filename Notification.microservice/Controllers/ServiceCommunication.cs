using Microsoft.AspNetCore.Mvc;
using Notification.microservice.data;
using Notification.microservice.Model;
using Notification.microservice.Models.DTOs;

namespace Notification.microservice.Controllers
{

    [ApiController]
    [Route("api/v1/service/notification")]
    public class ServiceCommunication : ControllerBase
    {
        private readonly DBcontext db;

        public ServiceCommunication(DBcontext _db)
        {
            db = _db;
        }

        [HttpPost("sendNotif")]
        public async Task<ActionResult<ResponseDTO>> sendNotification(SendNotifDTO req)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                NotificationModel newNotifData = new NotificationModel()
                {
                    CasterId = req.CasterId,
                    CasterAvatarUrl = req.CasterAvatarUrl,
                    CasterUserName = req.CasterUserName,
                    ReceiverId = req.ReceiverId,
                    Type = req.Type,
                };

                var newNotification = db.Notifications.Add(newNotifData);
                db.SaveChanges();

                if (newNotifData != null)
                {
                    response.Success = true;
                    response.Message = "Notification sent.";
                    return Ok(response);
                }
                response.Success = false;
                response.Message = "could not sent notification!";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Internal server error";
                return BadRequest(response);
            }
        }

        [HttpPost("removenotif")]
        public async Task<ActionResult<ResponseDTO>> removeNotif(RemoveNotifDTO req)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {

                var notif = db.Notifications.FirstOrDefault(n => n.Type == req.Type && n.CasterId == req.CasterId && n.ReceiverId == req.ReceiverId && n.HelperId == req.HelperId);


                if (notif != null)
                {
                    db.Notifications.Remove(notif);
                    db.SaveChanges();

                    response.Success = true;
                    response.Message = "Notif removed";
                    return Ok(response);
                }
                response.Success = false;
                response.Message = "could not sent notification!";
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
