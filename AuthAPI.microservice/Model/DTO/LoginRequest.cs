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
}
