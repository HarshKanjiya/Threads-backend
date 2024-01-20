using AuthAPI.microservice.Constants;
using Microsoft.OpenApi.Any;

namespace AuthAPI.microservice.Model.DTO
{
    public class SignUpRequest
    {
        public required string UserName { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Birthdate { get; set; }
        public required GenderType Gender { get; set; }
        public required AnyType Avatar { get; set; }
        public required string Password { get; set; }
    }
}
