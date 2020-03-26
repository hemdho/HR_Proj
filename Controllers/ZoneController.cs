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
    public class ZoneController : ControllerBase
    {
        public ICommonRepository<ZoneView> zoneRepository { get; set; }
        public ICommonQuery<ZoneView> commonQueryRepo { get; set; }
        private ResponseHelper objHelper = new ResponseHelper();
        public ZoneController(ICommonRepository<ZoneView> commonRepository, ICommonQuery<ZoneView> commonQueryRepo)
        {
            zoneRepository = commonRepository; 
            this.commonQueryRepo = commonQueryRepo;
        }

        // GET: api/Zone/GetAll/1000
        // GET: api/Zone/GetAll
        [HttpGet]
        [HttpGet("{RecordLimit}")]
        public async Task<IActionResult> GetAll(int RecordLimit)
        {
            try
            {
                var vList = await zoneRepository.GetAll(RecordLimit);
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

        // GET: api/Zone/GetAll
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var vList = await zoneRepository.Get(id);
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

        // GET: api/Zone/FindPagination
        [HttpGet]
        public async Task<IActionResult> FindPagination(Pagination pagination)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                pagination.RecordCount = zoneRepository.RecordCount(pagination.CommonSearch);
                var vList = await zoneRepository.FindPaginated(pagination.PageIndex, pagination.PageSize, pagination.CommonSearch);
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

        // GET Zone search by
        // GET: api/Zone/GetBy/
        [HttpGet]
        public async Task<IActionResult> GetBy(SearchBy searchBy)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                var vList = await commonQueryRepo.GetBy(searchBy);
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

        // POST: api/Zone/Add
        [ActionFilters.AuditLog]
        [HttpPost]
        public async Task<IActionResult> Add(ZoneView zone)
        {
            if (!ModelState.IsValid)
            {
                objHelper.Status = StatusCodes.Status424FailedDependency;
                objHelper.Message = "Invalid Model State";
                return BadRequest(objHelper);
            }

            try
            {
                if (zoneRepository.Exists(zone))
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Data already available";
                    return Ok(objHelper);
                }
                await zoneRepository.Insert(zone);
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Saved Successfully";
                objHelper.Data = zone;
                return Ok(objHelper);
            }
            catch
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = "Save Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }

        // PUT: api/Zone/Edit
        [ActionFilters.AuditLog]
        [HttpPut]
        public async Task<IActionResult> Edit(ZoneView zone)
        {
            if (!ModelState.IsValid)
            {
                objHelper.Status = StatusCodes.Status424FailedDependency;
                objHelper.Message = "Invalid Model State";
                return BadRequest(objHelper);
            }

            try
            {
                if (zoneRepository.Exists(zone))
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Data already available";
                    return Ok(objHelper);
                }
                await zoneRepository.Update(zone);
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

        // PUT: api/Zone/UpdateStatus/34,1
        // PUT: api/Zone/UpdateStatus/34,0
        [ActionFilters.AuditLog]
        [HttpPut("{id},{isActive}")]
        public async Task<IActionResult> UpdateStatus(int id, short isActive)
        {
            try
            {
                await zoneRepository.ToogleStatus(id, isActive);
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Saved Successfully";
                objHelper.Data = await zoneRepository.Get(id);
                return Ok(objHelper);
            }
            catch
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = "Save Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }

        // DELETE: api/Zone/Delete/5
        [ActionFilters.AuditLog]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await zoneRepository.Delete(id);
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