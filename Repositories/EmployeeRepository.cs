using HR.WebApi.Controllers;
using HR.WebApi.DAL;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HR.WebApi.Repositories
{
    public class EmployeeRepository<T> : ICommonRepository<Employee>
    { 
        private readonly ApplicationDbContext adbContext;
        
        public EmployeeRepository()
        {
            adbContext = Startup.applicationDbContext;
        }
        public async Task<IEnumerable<Employee>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    IEnumerable<Employee> vList = adbContext.employee.Where(w=>w.isActive == 1).Take(RecordLimit).ToList();
                    return await Task.FromResult(vList);
                }
                else
                {
                    IEnumerable<Employee> vList = adbContext.employee.Where(w => w.isActive == 1).ToList();
                    return await Task.FromResult(vList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<Employee>> Get(int id)
        {
            try
            {
                IEnumerable<Employee> vList = adbContext.employee.Where(w => w.Emp_Id == id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(Employee entity)
        {
            try
            {
                entity.AddedOn = DateTime.Now;
                adbContext.employee.Add(entity);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Employee entity)
        {
            try
            {
                var lstEmp = adbContext.employee.Where(x => x.Emp_Id == entity.Emp_Id).FirstOrDefault();
                if (lstEmp != null)
                {
                    lstEmp.Company_Id = entity.Company_Id;
                    lstEmp.Site_Id = entity.Site_Id;
                    lstEmp.JD_Id = entity.JD_Id;
                    lstEmp.Dept_Id = entity.Dept_Id;
                    lstEmp.Desig_Id = entity.Desig_Id;
                    lstEmp.Zone_Id = entity.Zone_Id;
                    lstEmp.Shift_Id = entity.Shift_Id;
                    lstEmp.Emp_Code = entity.Emp_Code;
                    lstEmp.JoiningDate = entity.JoiningDate;
                    lstEmp.Reporting_Id = entity.Reporting_Id;
                    lstEmp.isSponsored = entity.isSponsored;
                    lstEmp.Tupe = entity.Tupe;
                    lstEmp.NiNo = entity.NiNo;
                    lstEmp.NiCategory = entity.NiCategory;
                    lstEmp.PreviousEmp_Id = entity.PreviousEmp_Id;
                    lstEmp.isActive = entity.isActive;
                    lstEmp.UpdatedBy = entity.UpdatedBy;
                    lstEmp.UpdatedOn = DateTime.Now;

                    adbContext.employee.Update(lstEmp);
                    await Task.FromResult(adbContext.SaveChanges());
                }
                else
                {
                    throw new Exception("Data Not Available");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ToogleStatus(int id, short isActive)
        {
            try
            {
                //update flag isActive
                var vList = adbContext.employee.Where(w => w.Emp_Id == id && w.isActive != isActive).SingleOrDefault();
                if (vList != null)
                {
                    vList.isActive = isActive;
                    adbContext.employee.Update(vList);
                    await Task.FromResult(adbContext.SaveChanges());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var vList = adbContext.employee.Where(w => w.Emp_Id == id).FirstOrDefault();
                if (vList != null)
                {
                    vList.isActive = 0;
                    await Task.FromResult(adbContext.SaveChanges());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(Employee entity)
        {
            try
            {
                int intCount = 0;
                if (entity.Emp_Id > 0)
                    intCount = adbContext.employee.Where(w => w.Emp_Id != entity.Emp_Id && w.Company_Id == entity.Company_Id && (w.Emp_Code == entity.Emp_Code)).Count();
                else
                    intCount = adbContext.employee.Where(w => w.Company_Id == entity.Company_Id && (w.Emp_Code == entity.Emp_Code)).Count();
                return (intCount > 0 ? true : false);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Employee>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    try
                    {
                        return adbContext.employee.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    var vList = adbContext.employee.Where(w => new[] {  w.Emp_Code, Convert.ToString(w.Reporting_Id), w.NiNo, w.NiCategory }.Any(a => a.Contains(searchValue))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                    if (vList != null)
                    {
                        return await Task.FromResult(vList);
                    }
                    else
                    {
                        throw new Exception("Data Not Available");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int RecordCount(string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    var vCount = (from emp in adbContext.employee
                                  select emp.Emp_Id).Count();
                    return vCount;
                }
                else
                {
                    var vList = adbContext.employee.Where
                        (w => new[] { Convert.ToString(w.Reporting_Id), w.NiNo, w.NiCategory }.Any(a => a.Contains(searchValue))).Count();

                    return vList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}