using System.ComponentModel.DataAnnotations;

namespace UserApi.microservice.Models
{
    public class UserModel
    {
        [Key] public Guid UserId { get; set; }

        public required string Name { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;
        public required string PasswordSalt { get; set; } = string.Empty;
        public required string Gender { get; set; }
        public string? Bio { get; set; } = string.Empty;
        public required string PhoneNumber { get; set; }
        public string? Status { get; set; }
        public required string AvatarURL { get; set; }
        public string AvatarPublicID { get; set; }
        public required string Birthdate { get; set; }
        public string? Role { get; set; } = "USER";
        public int FollowersCount { get; set; } = 0;
        public int FollowingCount { get; set; } = 0;
        public int PostsCount { get; set; } = 0;
        public int RepliesCount { get; set; } = 0;

        public bool Verified { get; set; } = false;

        public List<Device> Devices { get; set; } = new List<Device>();

        public string BanStatus { get; set; } = "UNBAN";
        public DateTime? BanStartTime { get; set; }
        public string? BanDuration { get; set; }
    }

    public class Device
    {
        [Key] public Guid DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string DeviceLocation { get; set; }
        public string RefreshToken { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
