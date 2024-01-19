using AuthAPI.microservice.data;
using AuthAPI.microservice.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.microservice.Controllers
{

    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly DBcontextUser _db;

        public AuthController(DBcontextUser db)
        {
            _db = db;
            ResponseDTO _responseDto = new ResponseDTO();
        }


        [HttpPatch]
        public async Task<IActionResult> Login()
        {


        }
    }
}
