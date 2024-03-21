using System.ComponentModel.DataAnnotations;
using Thread.Model;

namespace UserApi.microservice.Models.DTOs
{
    public class ResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Object? Data { get; set; }

    }

    public class ThreadResponseDTO
    {
        public Guid ThreadId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid AuthorId { get; set; }
        public string? Type { get; set; }
        public string? ReferenceId { get; set; } = string.Empty;
        public string? ReplyAccess { get; set; }
        public required ThreadContent Content { get; set; }
        public int Replies { get; set; } = 0;
        public int Likes { get; set; } = 0;
        public Boolean LikedByMe { get; set; } = false;
        public string BanStatus { get; set; } = "UNBAN";
        public string AuthorName { get; set; }
        public string AuthorUserName { get; set; }
        public string AuthorAvatarURL { get; set; }
    }
}
