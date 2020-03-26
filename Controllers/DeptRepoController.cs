using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_PROJ.Interfaces;
using HR_PROJ.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_PROJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeptRepoController : ControllerBase
    {

        public IDepartmentRepository departmentRepository { get; set; }
        public DeptRepoController(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        // GET: api/Department
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Department> lstDepartment = departmentRepository.GetDepartments();
            return Ok(lstDepartment);
        }

        // GET: api/Department/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            Department lstDepartment = departmentRepository.GetDepartment(id);
            return Ok(lstDepartment);
        }

        // POST: api/Department
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Department/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}