using System.ComponentModel.DataAnnotations;

namespace UserApi.microservice.Models
{
    public class OtpModel
    {
        [Key] public Guid OtpId { get; set; }
        public Guid UserId { get; set; }
        public string otp { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
