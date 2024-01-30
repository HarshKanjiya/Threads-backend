using System.ComponentModel.DataAnnotations;

namespace Admin.microservice.Model
{
    public class ReportCategoryModel
    {
        [Key] public Guid ReportCategoryId { get; set; }
        public List<ReportModel> Reports { get; set; }
        public string CategoryName { get; set; } = string.Empty;

    }
    public class ReportModel
    {
        [Key] public Guid ReportId { get; set; }
        public Guid CategoryId { get; set; }
        public string Text { get; set; }
    }
    public class CustomReportModel
    {
        [Key] public Guid ReportId { get; set; }
        public string Text { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public Guid HelperId { get; set; }
        public string Type { get; set; } // ACCOUNT_REPORT, POST_REPORT

    }

    public class BugReportModel
    {
        public Guid UserId { get; set; }
        [Key] public Guid ReportId { get; set; }
        public string? Text { get; set; } = string.Empty;
        public List<FilesModel>? Files { get; set; }
    }
    public class FilesModel
    {
        [Key] public string FileId { get; set; }
        public string filePublicId { get; set; }
        public string fileURL { get; set; }
    }

    public class UserReportModel
    {
        [Key] public Guid UserReportId { get; set; }
        public Guid UserId { get; set; }
        public Guid ReportId { get; set; }
        public Guid HelperId { get; set; }
        public string Type { get; set; } // ACCOUNT_REPORT, POST_REPORT
    }
}
