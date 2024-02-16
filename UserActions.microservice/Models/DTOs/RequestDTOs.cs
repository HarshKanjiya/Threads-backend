
namespace UserActions.microservice.Models.DTOs
{
    public class LikeDislikeRequestDTO
    {
        public Guid UserId { get; set; }
        public Guid ThreadId { get; set; }
        public string Status { get; set; }
    }

    public class FollowUserRequestDTO
    {
        public required Guid CasterId { get; set; }
        public required Guid ReceiverId { get; set; }
        public required string Type { get; set; }  // FOLLOW, MUTE, BLOCK
    }

    public class CkeckLikedDTO
    {
        public Guid UserId { get; set; }
        public Guid ThreadId { get; set; }
    }
}
