using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR.CommonUtility;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using HR.WebApi.ModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ActionFilters.Log]
    [ActionFilters.TokenVerify]
    public class Employee_ResignationController : ControllerBase
    {
        public ICommonRepository<Employee_Resignation> employee_ResignationRepository { get; set; }
        public Employee_ResignationController(ICommonRepository<Employee_Resignation> commonRepository)
        {
            employee_ResignationRepository = commonRepository;
        }

        // GET ALL Data with Record Limit or without Record Limit
        // GET: api/Employee_Resignation/GetAll or api/Employee_Resignation/GetAll/100
        [HttpGet()]
        [HttpGet("{recordLimit}")]
        public async Task<IActionResult> GetAll(int recordLimit)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                var vList = await employee_ResignationRepository.GetAll(recordLimit);
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

        // GET Employee_Resignation employeewise data
        // GET: api/Employee_Resignation/Get/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                var vList = await employee_ResignationRepository.Get(id);
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

        // GET Employee_Resignation wise pagination with search or without search
        // GET: api/Employee_Resignation/FindPagination - body data { PageIndex:0 , PageSize:10, CommonSearch: "test" }
        [HttpGet]
        public async Task<IActionResult> FindPagination(Pagination pagination)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                pagination.RecordCount = employee_ResignationRepository.RecordCount(pagination.CommonSearch);
                var vList = await employee_ResignationRepository.FindPaginated(pagination.PageIndex, pagination.PageSize, pagination.CommonSearch);
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

        //Add Data
        // POST: api/Employee_Resignation/Add - body data Employee_Resignation
        [ActionFilters.AuditLog]
        [HttpPost]
        public async Task<IActionResult> Add(Employee_Resignation employee_Resignation)
        {
            ResponseHelper objHelper = new ResponseHelper();
            if (!ModelState.IsValid)
            {
                objHelper.Status = StatusCodes.Status424FailedDependency;
                objHelper.Message = "Invalid Model State";
                return BadRequest(objHelper);
            }

            try
            {
                if (employee_ResignationRepository.Exists(employee_Resignation))
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Data already available";
                    return Ok(objHelper);
                }
                await employee_ResignationRepository.Insert(employee_Resignation);
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Saved Successfully";
                objHelper.Data = employee_Resignation;
                return Ok(objHelper);
            }
            catch
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = "Get Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }

        //Edit Data
        // PUT: api/Employee_Resignation/Edit - body data Employee_Resignation
        [ActionFilters.AuditLog]
        [HttpPut]
        public async Task<IActionResult> Edit(Employee_Resignation employee_Resignation)
        {
            ResponseHelper objHelper = new ResponseHelper();
            if (!ModelState.IsValid)
            {
                objHelper.Status = StatusCodes.Status424FailedDependency;
                objHelper.Message = "Invalid Model State";
                return BadRequest(objHelper);
            }

            try
            {
                if (employee_ResignationRepository.Exists(employee_Resignation))
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Data already available";
                    return Ok(objHelper);
                }

                await employee_ResignationRepository.Update(employee_Resignation);
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

        //Update Status
        // PUT: api/Employee_Resignation/UpdateStatus/id,isActive
        [ActionFilters.AuditLog]
        [HttpPut("{id},{isActive}")]
        public async Task<IActionResult> UpdateStatus(int id, short isActive)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                await employee_ResignationRepository.ToogleStatus(id, isActive);
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

        //Delete Data
        // DELETE: api/Employee_Resignation/Delete/1
        [ActionFilters.AuditLog]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                await employee_ResignationRepository.Delete(id);
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