using System.ComponentModel.DataAnnotations;

namespace Subscription.microservice.Models
{
    public class ReportModel
    {
        [Key] public int ReportId { get; set; }

        public required string CasterId { get; set; }
        public required string ReceiverId { get; set; }

        public string? ReceiverPostId { get; set; }

        public required ReportMessages Report { get; set; }
    }

    public class ReportMessages
    {
        [Key] public int MessageId { get; set; }
        public required string Message { get; set; }

        public required ReportCategory Category { get; set; }
    }

    public class ReportCategory
    {
        [Key] public int CategoryId { get; set; }
        public required string CategoryName { get; set; }
    }
}
