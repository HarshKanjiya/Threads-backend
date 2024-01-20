namespace AuthAPI.microservice.Model.DTO
{
    public class LoginRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
    public class EmailLoginRequest
    {
        public required string Email { get; set; }
    }

    public class UsernameAvailabilitiesRequest
    {
        public required string UserName { get; set; }
    }
}
