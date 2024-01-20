using AuthAPI.microservice.data;
using AuthAPI.microservice.Model;
using AuthAPI.microservice.Model.DTO;
using AuthAPI.microservice.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace AuthAPI.microservice.Services
{
    public class AuthService(DBcontextUser db, UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager) : IAuthService
    {
        private readonly DBcontextUser _db = db;
        private readonly UserManager<UserModel> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;


        public async Task<ResponseDTO> CheckUserNameAvailability(UsernameAvailabilitiesRequest request)
        {
            var user = _db.Users.First(u => u.UserName == request.UserName);

            Console.WriteLine(user?.UserName);


            if (user != null)
            {
                ResponseDTO resValidName = new ResponseDTO()
                {
                    Success = true,
                    Message = "UserName is not taken"
                };
                return resValidName;
            }
            ResponseDTO res = new ResponseDTO()
            {
                Success = false,
                Message = "UserName is Already taken"
            };
            return res;
        }

        public Task<UserDTO> Login(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDTO> Register(SignUpRequest request)
        {
            try
            {

                // upload image and store image url in here,
                var dummyImageUrl = "data:image/jpeg;base64,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";

                UserModel user = new()
                {
                    UserName = request.UserName,
                    Name = request.Name,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Gender = request.Gender,
                    Avatar = dummyImageUrl,
                    Birthdate = request.Birthdate,
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    var userToReturn = _db.Users.First(u => u.UserName == request.UserName);

                    UserDTO userDto = new()
                    {
                        UserId = userToReturn.UserId,
                        UserName = userToReturn.UserName,
                        Name = userToReturn.Name,
                        Email = userToReturn.Email,
                        Avatar = userToReturn.Avatar,
                        Role = userToReturn.Role
                    };
                    return new ResponseDTO()
                    {
                        Success = true,
                        Message = "Successfully logged in.",
                        Data = userDto
                    };
                }
                else
                {
                    return new ResponseDTO()
                    {
                        Success = false,
                        Message = result.Errors.FirstOrDefault().Description
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new ResponseDTO()
            {
                Success = false,
                Message = "Something went wrong!"
            };
        }
    }
}
