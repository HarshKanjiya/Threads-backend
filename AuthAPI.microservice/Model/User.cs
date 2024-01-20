using AuthAPI.microservice.Constants;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static AuthAPI.microservice.Constants.Constants;

namespace AuthAPI.microservice.Model
{
    public class UserModel : IdentityUser
    {

        [Key] public Guid UserId { get; set; }

        public required string Name { get; set; }
        public override required string UserName { get; set; }
        public required GenderType Gender { get; set; }
        public string? Bio { get; set; } = string.Empty;
        public override required string PhoneNumber { get; set; }
        public string? Status { get; set; }
        public required string Avatar { get; set; }
        public required string Birthdate { get; set; }

        public RoleType Role { get; set; } = RoleType.USER;

        public int FollowersCount { get; set; } = 0;
        public int FollowingCount { get; set; } = 0;
        public int PostsCount { get; set; } = 0;
        public int RepliesCount { get; set; } = 0;

        public bool Verified { get; set; } = false;

        public List<Device> Devices { get; set; } = new List<Device>();


    }

    public class Device
    {

        [Key] public Guid DeviceId { get; set; }
        public required DeviceType Type { get; set; }
        public required string RefreshToken { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
