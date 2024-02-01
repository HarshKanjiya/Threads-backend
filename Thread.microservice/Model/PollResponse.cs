using System.ComponentModel.DataAnnotations;

namespace Thread.microservice.Model
{
    public class PollResponseModel
    {
        [Key] public Guid PollResponseId { get; set; }
        public DateTime CreateAt = DateTime.Now;

        public required Guid ThreadId { get; set; }
        public required Guid UserId { get; set; }
        public required Guid OptionId { get; set; }
    }
}
