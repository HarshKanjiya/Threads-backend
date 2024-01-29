using System.ComponentModel.DataAnnotations;

namespace Thread.microservice.Model
{
    public class Hashtags
    {
        [Key] public int TagId { get; set; }
        public List<Guid> Tags { get; set; } = new List<Guid>();
    }
}
