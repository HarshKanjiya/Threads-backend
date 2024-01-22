using System.ComponentModel.DataAnnotations;

namespace UserApi.microservice.Models
{
    public class PasswordTokenModel
    {
        [Key] public Guid TokenId { get; set; }
        public Guid UserId { get; set; }
        public int Token { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
