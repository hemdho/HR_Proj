using HR.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Interfaces
{
    public interface IDepartmentRepository
    {
        List<Department> GetDepartments();

        Department GetDepartment(int id);

        void Insert(Department department);

        void Update(Department department);

        void Delete(int id);

        void Save();
    }
}
