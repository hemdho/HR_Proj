using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR.WebApi.Repositories;
using HR.WebApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HR.CommonUtility;

namespace HR.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ActionFilters.Log]
    [ActionFilters.TokenVerify]
    public class EmployeeDetailController : ControllerBase
    {
        public EmployeeDetailService employeeDetailService { get; set; }
        public EmployeeDetailController(EmployeeDetailService commonRepository)
        {
            employeeDetailService = commonRepository;
        }

        [HttpGet()]
        [HttpGet("{recordLimit}")]
        public async Task<IActionResult> GetAll(int recordLimit)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                var vList = await employeeDetailService.GetAll(recordLimit);
                if (vList == null)
                {
                    objHelper.Status = StatusCodes.Status200OK;;
                    objHelper.Message = "Get Empty Data";
                }
                else
                {
                    objHelper.Status = StatusCodes.Status200OK;;
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
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                var vList = await employeeDetailService.GetEmployee(id);
                if (vList == null)
                {
                    objHelper.Status = StatusCodes.Status200OK;;
                    objHelper.Message = "Get Empty Data";
                }
                else
                {
                    objHelper.Status = StatusCodes.Status200OK;;
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

        [ActionFilters.AuditLog]
        [HttpPost]
        public async Task<IActionResult> Add(Employee employee)
        {
            ResponseHelper objHelper = new ResponseHelper();
            if (!ModelState.IsValid)
            {
                objHelper.Status = StatusCodes.Status410Gone;
                objHelper.Message = "Invalid Model State";
                return BadRequest(objHelper);
            }

            try
            {
                //if (_EmpDetails.IsDuplicate(employee))
                //{
                //    objHelper.Status = StatusCodes.Status200OK;;
                //    objHelper.Message = "Data already available";
                //    return Ok(objHelper);
                //}
                await employeeDetailService.Insert(employee);
                objHelper.Status = StatusCodes.Status200OK;;
                objHelper.Message = "Saved Successfully";
                objHelper.Data = employee;
                return Ok(objHelper);
            }
            catch
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = "Get Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }

        // PUT: api/Employee/5
        [ActionFilters.AuditLog]
        [HttpPut]
        public async Task<IActionResult> Edit(Employee employee)
        {
            ResponseHelper objHelper = new ResponseHelper();
            if (!ModelState.IsValid)
            {
                objHelper.Status = StatusCodes.Status410Gone;
                objHelper.Message = "Invalid Model State";
                return BadRequest(objHelper);
            }

            try
            {
                //if (_EmpDetails.IsDuplicate(employee))
                //{
                //    objHelper.Status = StatusCodes.Status200OK;;
                //    objHelper.Message = "Data already available";
                //    return Ok(objHelper);
                //}

               await employeeDetailService.Update(employee);
                objHelper.Status = StatusCodes.Status200OK;;
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

        [ActionFilters.AuditLog]
        [HttpPut("{id},{isActive}")]
        public async Task<IActionResult> UpdateStatus(int id, short isActive)
        {
            ResponseHelper objHelper = new ResponseHelper();
            if (!ModelState.IsValid)
            {
                objHelper.Status = StatusCodes.Status410Gone;
                objHelper.Message = "Invalid Model State";
                return BadRequest(objHelper);
            }

            try
            {
               await employeeDetailService.ToogleStatus(id, isActive);
                objHelper.Status = StatusCodes.Status200OK;;
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


        [ActionFilters.AuditLog]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                await employeeDetailService.Delete(id);
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