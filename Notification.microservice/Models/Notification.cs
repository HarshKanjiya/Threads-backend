using System.ComponentModel.DataAnnotations;
using static Notification.microservice.constants.Constants;

namespace Notification.microservice.Model
{
    public class NotificationModel
    {

        [Key] public Guid NotificationId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public required NotificationType Type { get; set; }

        public required string ReceiverId { get; set; }
        public required string CasterId { get; set; }
        public required string CasterUserName { get; set; }
        public required string CasterImage { get; set; }

        public bool Seen { get; set; } = false;

    }
}
