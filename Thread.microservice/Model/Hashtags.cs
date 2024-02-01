using System.ComponentModel.DataAnnotations;

namespace Thread.microservice.Model
{
    public class Hashtags
    {
        [Key] public int TagId { get; set; }
        public string TagName { get; set; }
        public List<Guid> Threads { get; set; } = new List<Guid>();
    }
}
