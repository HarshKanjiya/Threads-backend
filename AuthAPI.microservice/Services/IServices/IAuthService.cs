using AuthAPI.microservice.Model.DTO;

namespace AuthAPI.microservice.Services.IServices
{
    public interface IAuthService
    {
        public Task<ResponseDTO> Register(SignUpRequest request);
        public Task<ResponseDTO> Login(LoginRequest request);
        public Task<ResponseDTO> CheckUserNameAvailability(UsernameAvailabilitiesRequest request);
    }
}
