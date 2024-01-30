using Admin.microservice.Data;
using Admin.microservice.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notification.microservice.Models.DTOs;

namespace Admin.microservice.Controllers
{
    [Route("api/admin")]
    [ApiController]
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
                    CategoryId = req.CategoryId
                };
                var category = await db.AvailableReports.AddAsync(report);
                db.SaveChangesAsync();

                if (category.Entity != null)
                {
                    response.Message = "Category Created";
                    response.Success = true;
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

    }
}
