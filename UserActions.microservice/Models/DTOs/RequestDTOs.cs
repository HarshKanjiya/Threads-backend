
namespace UserActions.microservice.Models.DTOs
{
    public class LikeDislikeRequestDTO
    {
        public string UserId { get; set; }
        public string ThreadId { get; set; }
        public string Status { get; set; }
    }

    public class FollowUserRequestDTO
    {
        public required string CasterId { get; set; }
        public required string ReceiverId { get; set; }
        public required string Type { get; set; }  // FOLLOW, MUTE, BLOCK
    }
}
