
namespace UserApi.microservice.Models.DTOs
{
    public class ResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Object? Data { get; set; }
        public AccessTokenData? AccessToken { get; set; }
        public RefreshTokenData? RefreshToken { get; set; }
    }

    public class AccessTokenData
    {
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
    }

    public class RefreshTokenData
    {
        public string Token { set; get; }
        public int ExpiresIn { get; set; }
    }
}
