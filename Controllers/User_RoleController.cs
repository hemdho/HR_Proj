using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR.CommonUtility;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ActionFilters.Log]
    [ActionFilters.TokenVerify]
    public class User_RoleController : ControllerBase
    {
        public ICommonRepository<User_Role> user_RoleRepository { get; set; }
        private ResponseHelper objHelper = new ResponseHelper();
        public User_RoleController(ICommonRepository<User_Role> commonRepository)
        {
            this.user_RoleRepository = commonRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int RecordLimit)
        {
            try
            {
                var vList = await user_RoleRepository.GetAll(RecordLimit);
                if (vList == null)
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Get Empty Data";
                }
                else
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Get Successfully";
                    objHelper.Data = vList;
                }
                return Ok(objHelper);
            }
            catch
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = "Get Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }

        // GET: api/User_Role/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var vList = await user_RoleRepository.Get(id);
                if (vList == null)
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Get Empty Data";
                }
                else
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Get Successfully";
                    objHelper.Data = vList;
                }
                return Ok(objHelper);
            }
            catch
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = "Get Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }

        // GET: api/User_Role/5
        [HttpGet("{pageIndex},{pageSize}")]
        public async Task<IActionResult> Get(int pageIndex, int pageSize,string SearchValue)
        {
            try
            {
                var vList = await user_RoleRepository.FindPaginated(pageIndex, pageSize, SearchValue);
                if (vList == null)
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Get Empty Data";
                }
                else
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Get Successfully";
                    objHelper.Data = vList;
                }
                return Ok(objHelper);
            }
            catch
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = "Get Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }

        // POST: api/User_Role
        [ActionFilters.AuditLog]
        [HttpPost]
        public async Task<IActionResult> Post(User_Role user_Role)
        {
            if (!ModelState.IsValid)
            {
                objHelper.Status = StatusCodes.Status424FailedDependency;
                objHelper.Message = "Invalid Model State";
                return BadRequest(objHelper);
            }

            try
            {
                //if (user_RoleRepository.IsDuplicate(user_Role))
                //{
                //    objHelper.Status = StatusCodes.Status200OK;
                //    objHelper.Message = "Data already available";
                //    return Ok(objHelper);
                //}

                await user_RoleRepository.Insert(user_Role);
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Saved Successfully";
                objHelper.Data = user_Role;
                return Ok(objHelper);
            }
            catch
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = "Get Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }        

        [ActionFilters.AuditLog]
        [HttpPut("{id},{isActive}")]
        public async Task<IActionResult> Put(int id, short isActive)
        {
            if (!ModelState.IsValid)
            {
                objHelper.Status = StatusCodes.Status424FailedDependency;
                objHelper.Message = "Invalid Model State";
                return BadRequest(objHelper);
            }
            try
            {
                await user_RoleRepository.ToogleStatus(id, isActive);
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Saved Successfully";
                return Ok(objHelper);
            }
            catch
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = "Get Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }

        // DELETE: api/User_Role/5
        [ActionFilters.AuditLog]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await user_RoleRepository.Delete(id);
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Saved Successfully";
                return Ok(objHelper);
            }
            catch
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = "Get Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }
    }
}