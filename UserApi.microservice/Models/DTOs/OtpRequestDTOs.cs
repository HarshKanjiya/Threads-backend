namespace UserApi.microservice.Models.DTOs
{
    public class OtpCreateDTO
    {
        public string Email { get; set; }
    }

    public class OtpVerifyDTO
    {
        public string Otp { get; set; }
        public string Email { get; set; }
    }
}
