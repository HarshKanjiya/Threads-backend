using Admin.microservice.Data;
using Admin.microservice.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notification.microservice.Models.DTOs;

namespace Admin.microservice.Controllers
{
    [Route("api/v1/admin/env")]
    [ApiController]
/*     [Authorize("admin")]*/
    public class EnvVarController : ControllerBase
    {
        private readonly DBcontext db;
        public EnvVarController(DBcontext _db)
        {
            db = _db;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> AddUpdateEnvVar(AddUpdateEnvVarRequestDTO req)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var existing = db.EnvironmentVariables.FirstOrDefault(var => var.Key == req.Key);
                if (existing != null)
                {
                    existing.Value = req.Value;
                    db.SaveChanges();

                    response.Message = "Key Updated Successfully";
                    response.Success = true;
                    response.Data = existing;

                }
                else
                {
                    EnvVarModel newVarData = new EnvVarModel() { Value = req.Value, Key = req.Key };
                    var newVar = await db.EnvironmentVariables.AddAsync(newVarData);
                    await db.SaveChangesAsync();

                    if (newVar.Entity != null)
                    {
                        response.Message = "Key Added Successfully";
                        response.Success = true;
                        response.Data = newVar.Entity;
                    }
                    else
                    {
                        response.Message = "Failed to create new Key!";
                        response.Success = false;
                    }
                }
                return (response);
            }
            catch (Exception e)
            {
                response.Message = "Internal server error : Admin";
                response.Success = false;
                return BadRequest(response);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAllEnvVar()
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var variables = db.EnvironmentVariables.ToList();
                response.Success = true;
                response.Message = "Variables Fetched";
                response.Data = variables;
                return Ok(response);
            }
            catch (Exception e)
            {
                response.Message = "Internal server error : Admin";
                response.Success = false;
                return BadRequest(response);
            }
        }

        [HttpGet("{Key}")]
        public async Task<ActionResult<ResponseDTO>> GetAllEnvVar(string Key)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var Variable = db.EnvironmentVariables.FirstOrDefault(x => x.Key == Key);
                if (Variable != null)
                {
                    response.Success = true;
                    response.Message = "Variable Fetched!";
                    response.Data = Variable;
                    return Ok(response);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Variable not found!";
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                response.Message = "Internal server error : Admin";
                response.Success = false;
                return BadRequest(response);
            }
        }


        [HttpDelete("{Key}")]
        public async Task<ActionResult<ResponseDTO>> RemoveEnvVar(string Key)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var Variable = db.EnvironmentVariables.FirstOrDefault(x => x.Key == Key);
                if (Variable != null)
                {
                    db.EnvironmentVariables.Remove(Variable);
                    db.SaveChanges();
                    response.Success = true;
                    response.Message = "Variable Removed";
                    return Ok(response);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Variable not found!";
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                response.Message = "Internal server error : Admin";
                response.Success = false;
                return BadRequest(response);
            }
        }
    }
}
