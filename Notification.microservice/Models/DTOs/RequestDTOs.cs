using System.Xml;

namespace Notification.microservice.Models.DTOs
{
    public class SendNotifDTO
    {
        public required string Type { get; set; } // FOLLOWER, REPLY, MENTIONED, REPOST, QUOTED
        public required Guid ReceiverId { get; set; }
        public required Guid CasterId { get; set; }
        public required string CasterUserName { get; set; }
        public required string CasterAvatarUrl { get; set; }
        public string? HelperId { get; set; }

    }
    public class RemoveNotifDTO
    {
        public required string Type { get; set; } // FOLLOWER, REPLY, MENTIONED, REPOST, QUOTED
        public required Guid ReceiverId { get; set; }
        public required Guid CasterId { get; set; }
        public string? HelperId { get; set; }
    }
}
