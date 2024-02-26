using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace UserApi.microservice.Models.DTOs
{
    public class CreateRequestDTO
    {

        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorUserName { get; set; }
        public string AuthorAvatarURL { get; set; }
        public string Type { get; set; } = "PARENT";
        public string? ReferenceId { get; set; }
        public string ReplyAccess { get; set; } = "ANY";
        public required ContentDTO Content { get; set; }
        public List<CreateRequestDTO>? Child { get; set; } = new List<CreateRequestDTO>();

    }

    public class ContentDTO
    {
        public string ContentType { get; set; } = "TEXT";
        public string Text { get; set; }
        public List<string>? Files { get; set; } = new List<string>();
        public List<ContentOptionDTO>? Options { get; set; } = new List<ContentOptionDTO>();

    }

    public class ContentOptionDTO
    {
        public string Option { get; set; }
        public string Value { get; set; }
    }

    public class PollResponseDTO
    {
        public required Guid ThreadId { get; set; }
        public required Guid UserId { get; set; }
        public required Guid OptionId { get; set; }
    }

    public class QueryParameters
    {
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
    }

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
        public Guid? HelperId { get; set; }
    }
}
