using AuthAPI.microservice.Constants;

namespace AuthAPI.microservice.Model.DTO
{
    public class UserDTO
    {

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public RoleType Role { get; set; }
    }
}
