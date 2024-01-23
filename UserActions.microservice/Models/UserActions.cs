using System.ComponentModel.DataAnnotations;

namespace UserActions.microservice.Models
{
    public class LikeModel
    {
        [Key] public Guid LikeId { get; set; }
        public DateTime CreateAt = DateTime.Now;

        public required string ThreadId { get; set; }
        public required string UserId { get; set; }
    }

    /*    public class PollResponseModel
        {
            [Key] public Guid PollResponseId { get; set; }
            public DateTime CreateAt = DateTime.Now;

            public required string ThreadId { get; set; }
            public required string UserId { get; set; }
            public required string Selection { get; set; }
        }*/

    public class RelationshipModel
    {
        [Key] public string RelationId { get; set; }
        public required string CasterId { get; set; }
        public required string ReceiverId { get; set; }
        public required string Type { get; set; }
    }
}
