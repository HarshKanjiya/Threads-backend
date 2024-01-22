using System.Xml;

namespace UserApi.microservice.Models.DTOs
{
    public class LoginRequestDTO
    {
        public required string UniqueId { get; set; }
        public required string Password { get; set; }
    }
    public class SignupRequestDTO
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
        public string Password { get; set; }
    }
    public class EmailLoginRequestDTO
    {
        public string Email { get; set; }
    }
    public class ForgotPasswordDTO
    {
        public string Email { get; set; }
    }
    public class SignOutRequestDTO { }

    public class CheckUsernameAvaibilityDTO
    {
        public string UserName { get; set; }
    }
}
