using Admin.microservice.Data;
using Admin.microservice.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notification.microservice.Models.DTOs;

namespace Admin.microservice.Controllers
{
    [Route("api/v1/admin")]
    [ApiController]
    /*    [Authorize("ADMIN")]*/
    public class AdminReportController : ControllerBase
    {
        private readonly DBcontext db;

        public AdminReportController(DBcontext _db)
        {
            db = _db;
        }

        [HttpPost("report/category")]
        public async Task<ActionResult<ResponseDTO>> CreateNewCategory(newCategoryRequestDTO req)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                ReportCategoryModel reportCategory = new ReportCategoryModel()
                {
                    CategoryName = req.CategoryName
                };
                var category = await db.ReportCategories.AddAsync(reportCategory);
                db.SaveChangesAsync();

                if (category.Entity != null)
                {
                    response.Message = "Category Created";
                    response.Success = true;
                    response.Data = category.Entity;
                    return Ok(response);
                }
                response.Message = "Failed to create new Category";
                response.Success = false;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Internal server error : Admin";
                return BadRequest(response);
            }
        }

        [HttpPost("report")]
        public async Task<ActionResult<ResponseDTO>> CreateNewReport(newReportRequestDTO req)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                ReportModel report = new ReportModel()
                {
                    Text = req.text,
                    ReportCategoryId = req.CategoryId
                };
                var rep = await db.AvailableReports.AddAsync(report);
                db.SaveChangesAsync();

                if (rep.Entity != null)
                {
                    response.Message = "Category Created";
                    response.Success = true;
                    response.Data = rep.Entity;
                    return Ok(response);
                }
                response.Message = "Failed to create new Category";
                response.Success = false;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Internal server error : Admin";
                return BadRequest(response);
            }
        }

        [HttpGet("reports")]
        public async Task<ActionResult<ResponseDTO>> getAllReports()
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var categories = db.AvailableReports.ToList();

                if (categories != null)
                {
                    response.Message = "reports Found";
                    response.Success = true;
                    response.Data = categories;
                    return Ok(response);
                }
                response.Message = "Failed to get reports";
                response.Success = false;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Internal server error : Admin";
                return BadRequest(response);
            }
        }

        [HttpGet("reports/category")]
        public async Task<ActionResult<ResponseDTO>> getAllCategories()
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var categories = db.ReportCategories.ToList();

                if (categories != null)
                {
                    response.Message = "categories Found";
                    response.Success = true;
                    response.Data = categories;
                    return Ok(response);
                }
                response.Message = "Failed to get categories";
                response.Success = false;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Internal server error : Admin";
                return BadRequest(response);
            }
        }

        [HttpGet("reports/users")]
        public async Task<ActionResult<ResponseDTO>> getAllUserReports()
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var categories = db.UserReports.ToList();

                if (categories != null)
                {
                    response.Message = "reports Found";
                    response.Success = true;
                    response.Data = categories;
                    return Ok(response);
                }
                response.Message = "Failed to get reports";
                response.Success = false;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Internal server error : Admin";
                return BadRequest(response);
            }
        }

        [HttpGet("reports/bugs")]
        public async Task<ActionResult<ResponseDTO>> getAllBugReports()
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var categories = db.BugReports.ToList();

                if (categories != null)
                {
                    response.Message = "reports Found";
                    response.Success = true;
                    response.Data = categories;
                    return Ok(response);
                }
                response.Message = "Failed to get reports";
                response.Success = false;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Internal server error : Admin";
                return BadRequest(response);
            }
        }

    }
}
