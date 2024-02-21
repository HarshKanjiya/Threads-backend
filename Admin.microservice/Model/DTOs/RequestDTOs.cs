using System.Xml;

namespace Notification.microservice.Models.DTOs
{
    public class newCategoryRequestDTO
    {
        public string CategoryName { get; set; }
    }
    public class newReportRequestDTO
    {
        public string text { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
    }
    public class changeReportCategoryRequestDTO
    {
        public Guid ReportId { get; set; }
        public Guid NewCategoryId { get; set; }
    }

    public class newPreDefinedReportDTO
    {
        public Guid UserId { get; set; }
        public Guid ReportId { get; set; }
        public string Type { get; set; }
        public Guid HelperId { get; set; }

    }

    public class newCustomReportDTO
    {
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public Guid HelperId { get; set; }
        public string Type { get; set; }

    }

    public class newBugReportDTO
    {
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public List<string>? Files { get; set; }
    }


    public class AddEnvVarRequestDTO
    {
        public required string Key { get; set; }
        public required string Value { get; set; }
        public bool SecretKey { get; set; }
    }

    public class UpdateEnvVarRequestDTO
    {
        public required Guid VarId { get; set; }
        public required string Key { get; set; }
        public required string Value { get; set; }
        public bool SecretKey { get; set; }
    }

}
