using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace UserApi.microservice.Models.DTOs
{
    public class CreateRequestDTO
    {

        public string UserId { get; set; }
        public string Type { get; set; } = "PARENT";
        public string? ReferenceId { get; set; }
        public string ReplyAccess { get; set; } = "ANY";
        public required ContentDTO Content { get; set; }

    }

    public class ContentDTO
    {
        public string ContentType { get; set; } = "TEXT";
        public string Text { get; set; }
        public List<IFormFile>? Files { get; set; } = new List<IFormFile>();
        public List<ContentOptionDTO>? Options { get; set; } = new List<ContentOptionDTO>();

    }

    public class ContentOptionDTO
    {
        public string Option { get; set; }
        public string Value { get; set; }
    }

    public class PollResponseDTO
    {
        public required string ThreadId { get; set; }
        public required string UserId { get; set; }
        public required int Selection { get; set; }
    }

}
