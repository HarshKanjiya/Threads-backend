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
        public required string UserName { get; set; }
        public required GenderType Gender { get; set; }
        public string Bio { get; set; } = string.Empty;
        public string? Status { get; set; }

        public RoleType Role { get; set; } = RoleType.USER;

        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        public int PostsCount { get; set; }
        public int RepliesCount { get; set; }

        public bool Verified { get; set; }

        public List<Device> Devices { get; set; } = new List<Device>();

    }
}
