using Admin.microservice.Data;
using Admin.microservice.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Notification.microservice.Models.DTOs;
using UserApi.microservice.Utils;

namespace Admin.microservice.Controllers
{
    [Route("api/v1/admin/report")]
    [ApiController]
    public class UserReportController : ControllerBase
    {
        private readonly DBcontext db;

        public UserReportController(DBcontext _db)
        {
            db = _db;
        }

        [HttpPost("predefined")]
        public async Task<ActionResult<ResponseDTO>> NewPredefinedUserReport(newPreDefinedReportDTO req)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                UserReportModel reportData = new UserReportModel()
                {
                    HelperId = req.HelperId,
                    ReportId = req.ReportId,
                    UserId = req.UserId,
                    Type = req.Type,
                };
                var report = await db.UserReports.AddAsync(reportData);

                if (report.Entity != null)
                {
                    response.Message = "Reported";
                    response.Success = true;
                    return Ok(response);
                }
                response.Message = "Failed to Report";
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

        [HttpPost("custom")]
        public async Task<ActionResult<ResponseDTO>> NewCustomUserReport(newCustomReportDTO req)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                CustomReportModel reportData = new CustomReportModel()
                {
                    HelperId = req.HelperId,
                    UserId = req.UserId,
                    Type = req.Type,
                };
                var report = await db.CustomReports.AddAsync(reportData);

                if (report.Entity != null)
                {
                    response.Message = "Reported";
                    response.Success = true;
                    return Ok(response);
                }
                response.Message = "Failed to Report";
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

        [HttpPost("bug")]
        public async Task<ActionResult<ResponseDTO>> NewBugReport(newBugReportDTO req)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {

                List<FilesModel> filesData = new List<FilesModel>();

                if (req.Files.Count > 0)
                {

                    ImageUpload Uploader = new ImageUpload();
                    foreach (var x in req.Files)
                    {
                        var img = await Uploader.Upload(x);
                        if (img == null)
                        {
                            response.Message = "File Upload failed.";
                            response.Success = false;
                            return Ok(response);

                        }
                        filesData.Add(new FilesModel()
                        {
                            filePublicId = img.PublicId,
                            fileURL = img.Url.ToString(),
                        });


                    }

                }
                BugReportModel reportData = new BugReportModel()
                {
                    UserId = req.UserId,
                    Text = req.Text,
                    Files = filesData
                };
                var report = await db.BugReports.AddAsync(reportData);

                if (report.Entity != null)
                {
                    response.Message = "Reported";
                    response.Success = true;
                    return Ok(response);
                }
                response.Message = "Failed to Report";
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
