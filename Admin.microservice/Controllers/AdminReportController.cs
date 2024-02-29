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

        [HttpPut("report/category/{categoryId}")]
        public async Task<ActionResult<ResponseDTO>> UpdateCategory(Guid categoryId, newCategoryRequestDTO req)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var existance = db.ReportCategories.FirstOrDefault(c => c.ReportCategoryId == categoryId);

                if (existance == null)
                {
                    response.Message = "Category not found";
                    response.Success = false;
                    return Ok(response);
                }

                existance.CategoryName = !string.IsNullOrWhiteSpace(req.CategoryName) ? req.CategoryName : existance.CategoryName;
                db.SaveChanges();

                response.Message = "Category Updated";
                response.Success = true;
                response.Data = existance;
                return Ok(response);


            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Internal server error : Admin";
                return BadRequest(response);
            }
        }

        [HttpDelete("report/category/{categoryId}")]
        public async Task<ActionResult<ResponseDTO>> DeleteCategory(Guid categoryId)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var existance = db.ReportCategories.FirstOrDefault(c => c.ReportCategoryId == categoryId);

                if (existance == null)
                {
                    response.Message = "Category not found";
                    response.Success = false;
                    return Ok(response);
                }

                db.AvailableReports.RemoveRange(db.AvailableReports.Where(r=>r.ReportCategoryId == existance.ReportCategoryId));

                db.ReportCategories.Remove(existance);
                db.SaveChanges();

                response.Message = "Category deleted";
                response.Success = true;
                response.Data = existance;
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

        [HttpPut("report/{ReportId}")]
        public async Task<ActionResult<ResponseDTO>> UpdateReport(Guid ReportId, newReportRequestDTO req)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var existance = db.AvailableReports.FirstOrDefault(c => c.ReportId == ReportId);

                if (existance == null)
                {
                    response.Message = "Report not found";
                    response.Success = false;
                    return Ok(response);
                }

                existance.Text = !string.IsNullOrWhiteSpace(req.text) ? req.text : existance.Text;
                db.SaveChanges();

                response.Message = "report updated";
                response.Success = true;
                response.Data = existance;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Internal server error : Admin";
                return BadRequest(response);
            }
        }

        [HttpDelete("report/{ReportId}")]
        public async Task<ActionResult<ResponseDTO>> DeleteReport(Guid ReportId)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var existance = db.AvailableReports.FirstOrDefault(c => c.ReportId == ReportId);

                if (existance == null)
                {
                    response.Message = "Report not found";
                    response.Success = false;
                    return Ok(response);
                }

                db.AvailableReports.Remove(existance);
                db.SaveChanges();

                response.Message = "Report deleted";
                response.Success = true;
                response.Data = existance;
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

        [HttpGet("reports/users/{type}")]
        public async Task<ActionResult<ResponseDTO>> getAllUserReports(string type)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                if (type == "all")
                {
                    var reports = db.UserReports.ToList();
                    if (reports != null)
                    {
                        response.Message = "reports Found";
                        response.Success = true;
                        response.Data = reports;
                        return Ok(response);
                    }
                }
                if (type == "user")
                {
                    var reports = db.UserReports.Where(r => r.Type == "USER").ToList();
                    if (reports != null)
                    {
                        response.Message = "reports Found";
                        response.Success = true;
                        response.Data = reports;
                        return Ok(response);
                    }
                }
                if (type == "post")
                {
                    var reports = db.UserReports.Where(r => r.Type == "POST").ToList();
                    if (reports != null)
                    {
                        response.Message = "reports Found";
                        response.Success = true;
                        response.Data = reports;
                        return Ok(response);
                    }
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
