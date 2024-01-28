using System.ComponentModel.DataAnnotations;

namespace Thread.microservice.Model
{
    public class PollResponseModel
    {
        [Key] public Guid PollResponseId { get; set; }
        public DateTime CreateAt = DateTime.Now;

        public required string ThreadId { get; set; }
        public required string UserId { get; set; }
        public required int Selection { get; set; }
    }
}
