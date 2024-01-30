using System.ComponentModel.DataAnnotations;

namespace Notification.microservice.Model
{
    public class NotificationModel
    {

        [Key] public Guid NotificationId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public required string Type { get; set; }
        public required Guid ReceiverId { get; set; }
        public required Guid CasterId { get; set; }
        public required string CasterUserName { get; set; }
        public required string CasterAvatarUrl { get; set; }
        public string? HelperId { get; set; }

        public bool Seen { get; set; } = false;

    }
}
