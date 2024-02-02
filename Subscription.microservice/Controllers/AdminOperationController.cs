using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Subscription.microservice.data;
using Subscription.microservice.Models;
using Subscription.microservice.Models.DTOs;

namespace Subscription.microservice.Controllers
{
    [Route("api/v1/package/admin")]
    [ApiController, Authorize("ADMIN")]
    public class AdminOperationController : ControllerBase
    {
        private readonly DBcontext db;
        public AdminOperationController(DBcontext _db)
        {
            db = _db;
        }

        // FOR ADMIN
        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> getAllPackages()
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                var packages = db.Packages.ToList();

                if (packages != null)
                {
                    response.Message = "Packages Fetched";
                    response.Success = true;
                    response.Data = packages;
                    return Ok(response);
                }
                else
                {
                    response.Message = "Packages not found";
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
        public async Task<ActionResult<ResponseDTO>> getSinglePackage(Guid PackageId)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                var package = db.Packages.FirstOrDefault(p => p.PackageId == PackageId);

                if (package != null)
                {
                    response.Message = "Packages Fetched";
                    response.Success = true;
                    response.Data = package;
                    return Ok(response);
                }
                else
                {
                    response.Message = "Packages not found";
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

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> createNewPackage(CreateUpdatePackageRequestDTO req)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                PackagesModel data = new PackagesModel()
                {
                    Active = false,
                    Discount = req.Discount,
                    PackageName = req.PackageName,
                    PackagePrice = req.PackagePrice,
                    Perks = req.Perks,
                    AccentColor = req.AccentColor
                };

                var packages = await db.Packages.AddAsync(data);
                await db.SaveChangesAsync();

                if (packages.Entity != null)
                {
                    response.Message = "Packages Created";
                    response.Success = true;
                    response.Data = packages.Entity;
                    return Ok(response);
                }
                else
                {
                    response.Message = "Packages creation Failed";
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

        [HttpPut("{PackageId}")]
        public async Task<ActionResult<ResponseDTO>> updatePackage(Guid PackageId, CreateUpdatePackageRequestDTO req)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {

                var existance = db.Packages.FirstOrDefault(p => p.PackageId == PackageId);

                if (existance != null)
                {
                    existance.Perks = (req.Perks != null && req.Perks.Count > 0) ? req.Perks : existance.Perks;
                    existance.PackagePrice = req.PackagePrice != null ? req.PackagePrice : existance.PackagePrice;
                    existance.PackageName = req.PackageName != null ? req.PackageName : existance.PackageName;
                    existance.AccentColor = req.AccentColor != null ? req.AccentColor : existance.AccentColor;
                    existance.Active = req.Active != null ? req.Active : existance.Active;
                    existance.Discount = req.Discount != null ? req.Discount : existance.Discount;

                    db.SaveChanges();

                    response.Message = "Packages Updated";
                    response.Success = true;
                    response.Data = existance;
                    return Ok(response);
                }
                else
                {
                    response.Message = "Packages not found";
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

        [HttpDelete("{PackageId}")]
        public async Task<ActionResult<ResponseDTO>> deletePackage(Guid PackageId)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var existance = db.Packages.FirstOrDefault(p => p.PackageId == PackageId);
                if (existance != null)
                {
                    db.Packages.Remove(existance);
                    db.SaveChanges();

                    response.Success = true;
                    response.Message = "Package removed";
                    return Ok(response);
                }

                response.Message = "Package not Found";
                response.Success = false;
                return Ok(response);


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
