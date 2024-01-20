using AuthAPI.microservice.data;
using AuthAPI.microservice.Model.DTO;
using AuthAPI.microservice.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.microservice.Controllers
{

    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly DBcontextUser _db;
        private readonly IAuthService _authService;

        public AuthController(DBcontextUser db, IAuthService authService)
        {
            _db = db;
            ResponseDTO _responseDto = new ResponseDTO();
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(SignUpRequest req)
        {

            var res = await _authService.Register(req);
            return Ok(res);

        }

        /* [HttpPost, Route("/username")]
        public IActionResult CheckUserNameExistance(UsernameExistance req)
        {


        }*/

    }
}
