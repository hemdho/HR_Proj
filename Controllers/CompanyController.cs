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
    [ActionFilters.TokenVerify]
    [ActionFilters.Log]
    public class CompanyController : ControllerBase
    {
        public ICommonRepository<Company> companyRepository { get; set; }
        public CompanyController(ICommonRepository<Company> commonRepository)
        {
            companyRepository = commonRepository;
        }

        // GET: api/Company/GetAll
        // GET: api/Company/GetAll/1000
        [HttpGet]
        [HttpGet("{recordLimit}")]
        public async Task<IActionResult> GetAll(int RecordLimit)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {

                var vList = await companyRepository.GetAll(RecordLimit);
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

        // GET: api/Company/Get/10
        //[ActionFilters.AuditLog]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                var vList = await companyRepository.Get(id);
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

        // GET: api/Company/FindPagination/( Pagination Body )
        [HttpGet]
        public async Task<IActionResult> FindPagination(Pagination pagination)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                pagination.RecordCount = companyRepository.RecordCount(pagination.CommonSearch);
                var vList = await companyRepository.FindPaginated(pagination.PageIndex, pagination.PageSize, pagination.CommonSearch);
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

        //[HttpPost, DisableRequestSizeLimit]
        //public IActionResult Upload(IList<IFormFile> files)
        //{
        //    ResponseHelper objHelper = new ResponseHelper();
        //    try
        //    {
        //        Common.Documents objuploder = new Common.Documents();
        //        if (objuploder.UploadFile(files, "Company_Logo"))
        //        {

        //            objHelper.Status = StatusCodes.Status200OK;
        //            objHelper.Message = "Saved Successfully";
        //        }
        //        else
        //        {
        //            objHelper.Status = StatusCodes.Status200OK;
        //            objHelper.Message = "Get Successfully";
        //        }
        //        return Ok(objHelper);

        //    }
        //    catch
        //    {
        //        objHelper.Status = StatusCodes.Status500InternalServerError;
        //        objHelper.Message = "Get Unsuccessful";
        //        return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
        //    }
        //}

        // {Post: api/Company/Add/(Company Body) - without Id
        [ActionFilters.AuditLog]
        [HttpPost]
        public async Task<IActionResult> Add(Company company)
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
                if (companyRepository.Exists(company))
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Data already available";
                    return Ok(objHelper);
                }
                await companyRepository.Insert(company);
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Saved Successfully";
                objHelper.Data = company;
                return Ok(objHelper);
            }
            catch
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = "Save Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }

        // Put: api/Company/Edit/(Company Body) - with Id
        [ActionFilters.AuditLog]
        [HttpPut]
        public async Task<IActionResult> Edit(Company company)
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
                if (companyRepository.Exists(company))
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Data already available";
                    return Ok(objHelper);
                }
                await companyRepository.Update(company);
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Saved Successfully";
                return Ok(objHelper);
            }
            catch
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = "Save Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }

        // Put: api/Company/UpdateStatus/10,0(InActive)
        // Put: api/Company/UpdateStatus/10,0(Active)
        [ActionFilters.AuditLog]
        [HttpPut("{id},{isActive}")]
        public async Task<IActionResult> UpdateStatus(int id, short isActive)
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
                await companyRepository.ToogleStatus(id, isActive);
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Saved Successfully";
                return Ok(objHelper);
            }
            catch
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = "Save Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }

        // Delete: api/Company/Delete/10
        [ActionFilters.AuditLog]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                await companyRepository.Delete(id);
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Saved Successfully";
                return Ok(objHelper);
            }
            catch
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = "Save Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }
    }
}
