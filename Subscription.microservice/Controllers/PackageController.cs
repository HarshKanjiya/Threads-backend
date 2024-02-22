using Microsoft.AspNetCore.Mvc;
using Subscription.microservice.data;
using Subscription.microservice.Models.DTOs;

namespace Subscription.microservice.Controllers
{
    [Route("api/v1/packages")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly DBcontext db;
        public PackageController(DBcontext _db)
        {
            db = _db;
        }


        // FOR END USER
        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> getAvailablePackages()
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                var packages = db.Packages.Where(p => p.Published == true).ToList();

                if (packages != null)
                {
                    response.Message = "Packages Fetched";
                    response.Success = true;
                    response.Data = packages;
                    return Ok(response);
                }
                else
                {
                    response.Message = "Packages not Fetched";
                    response.Success = false;
                    return Ok(response);
                }

            }
            catch (Exception e)
            {
                response.Message = "Internal server Error";
                response.Success = false;
                return BadRequest(response);
            }
        }

        [HttpGet("{PackageId}")]
        public async Task<ActionResult<ResponseDTO>> getAvailablePackages(Guid PackageId)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                var packages = db.Packages.FirstOrDefault(p => p.PackageId == PackageId && p.Active == true);

                if (packages != null)
                {
                    response.Message = "Packages Fetched";
                    response.Success = true;
                    response.Data = packages;
                    return Ok(response);
                }
                else
                {
                    response.Message = "Packages not Found";
                    response.Success = false;
                    return Ok(response);
                }

            }
            catch (Exception e)
            {
                response.Message = "Internal server Error";
                response.Success = false;
                return BadRequest(response);
            }
        }
    }
}
