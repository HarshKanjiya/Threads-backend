﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Subscription.microservice.data;
using Subscription.microservice.Models;
using Subscription.microservice.Models.DTOs;

namespace Subscription.microservice.Controllers
{
    [Route("api/v1/packages/admin")]
    [ApiController]
    /*    [Authorize("ADMIN")]*/
    public class AdminOperationController : ControllerBase
    {
        private readonly DBcontext db;
        public AdminOperationController(DBcontext _db)
        {
            db = _db;
        }

        // FOR ADMIN
        [HttpGet("all/{type}")]
        public async Task<ActionResult<ResponseDTO>> getAllPackages(string type)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                if (type == "active")
                {
                    var packages = db.Packages.Where(t => t.Published == true).ToList();
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
                if (type == "inactive")
                {
                    var packages = db.Packages.Where(t => t.Active == false).ToList();
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
                if (type == "drafts")
                {
                    var packages = db.Packages.Where(t => t.Active == true && t.Published == false).ToList();
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
                if (type == "all")
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


                response.Message = "Invalid type";
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
                    Active = true,
                    Published = false,
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
                    existance.Published = req.Published != null ? req.Published : existance.Published;

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
