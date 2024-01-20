using System.ComponentModel.DataAnnotations;

namespace AuthAPI.microservice.Model
{
    public class Otp
    {
        [Key] public string Id { get; set; }
        public required string UserId { get; set; }
        public required string OTP { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
