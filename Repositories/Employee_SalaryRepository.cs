using HR.CommonUtility;
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
    public class Employee_SalaryRepository<T> : ICommonRepository<Employee_Salary>
    {
        private readonly ApplicationDbContext adbContext;

        public Employee_SalaryRepository()
        {
            adbContext = Startup.applicationDbContext;
        }

        public async Task<IEnumerable<Employee_Salary>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    IEnumerable<Employee_Salary> vList = adbContext.employee_salary.Take(RecordLimit).ToList();
                    return await Task.FromResult(vList);
                }
                else
                {
                    IEnumerable<Employee_Salary> vList = adbContext.employee_salary.ToList();
                    return await Task.FromResult(vList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_Salary>> GetByEmp_Id(int emp_Id)
        {
            try
            {
                IEnumerable<Employee_Salary> vList = adbContext.employee_salary.Where(w => w.Emp_Id == emp_Id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_Salary>> Get(int id)
        {
            try
            {
                IEnumerable<Employee_Salary> vList = adbContext.employee_salary.Where(w => w.Emp_Salary_Id == id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(Employee_Salary entity)
        {
            try
            {
                entity.Salary = Crypt.EncryptString(entity.Salary);
                entity.AddedOn = DateTime.Now;
                adbContext.employee_salary.Add(entity);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert_Multiple(IList<Employee_Salary> entity)
        {
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var employee in entity)
                    {
                        employee.AddedOn = DateTime.Now;
                        adbContext.employee_salary.Add(employee);
                    }
                    await Task.FromResult(adbContext.SaveChangesAsync());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Employee_Salary entity)
        {
            try
            {
                var vList = adbContext.employee_salary.Where(x => x.Emp_Salary_Id == entity.Emp_Salary_Id && x.Emp_Id == entity.Emp_Id).FirstOrDefault();
                if (vList != null)
                {
                    vList.Emp_Id = entity.Emp_Id;
                    vList.Company_Id = entity.Company_Id;
                    vList.Dept_Id = entity.Dept_Id;
                    vList.Desig_Id = entity.Desig_Id;
                    vList.ApprisalFrom = entity.ApprisalFrom;
                    vList.ApprisalTo = entity.ApprisalTo;
                    //vList.Salary = Crypt.EncryptString(entity.Salary);
                    vList.Reporting_Id = entity.Reporting_Id;
                    vList.isActive = entity.isActive;
                    vList.UpdatedBy = entity.UpdatedBy;
                    vList.UpdatedOn = DateTime.Now;

                    adbContext.employee_salary.Update(vList);
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

        public async Task Update_Salary(IList<Employee_Salary> entity)
        {
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var employee in entity)
                    {
                        await Update(employee);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ToogleStatusByEmp_Id(int emp_Id, short isActive)
        {
            try
            {
                //update flag isActive
                var vList = adbContext.employee_salary.Where(w => w.Emp_Id == emp_Id && w.isActive != isActive).ToList();
                if (vList.Count() > 0)
                {
                    vList.ForEach(a => a.isActive = isActive);
                    await Task.FromResult(adbContext.SaveChanges());
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
                var vList = adbContext.employee_salary.Where(w => w.Emp_Salary_Id == id && w.isActive != isActive).FirstOrDefault();
                if (vList != null)
                {
                    vList.isActive = isActive;
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

        public async Task DeleteByEmp_Id(int emp_Id)
        {
            try
            {
                var vList = adbContext.employee_salary.Where(w => w.Emp_Id == emp_Id).ToList();
                if (vList.Count() > 0)
                {
                    vList.ForEach(w => w.isActive = 0);
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
                var vList = adbContext.employee_salary.Where(w => w.Emp_Salary_Id == id).FirstOrDefault();
                if (vList != null)
                {
                    vList.isActive = 0;
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

        public async Task<IEnumerable<Employee_Salary>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    return adbContext.employee_salary.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    var vList = adbContext.employee_salary.Where(w => new[] { Convert.ToString(w.Emp_Id), Convert.ToString(w.ApprisalFrom), Convert.ToString(w.ApprisalTo) }.Any(a => a.Contains(searchValue))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                    if (vList.Count() > 0)
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
                    var vCount = (from emp in adbContext.employee_salary
                                  select emp.Emp_Salary_Id).Count();
                    return vCount;
                }
                else
                {
                    var vList = adbContext.employee_salary.
                        Where(w => new[] { Convert.ToString(w.Emp_Id), Convert.ToString(w.ApprisalFrom), Convert.ToString(w.ApprisalTo) }.Any(a => a.Contains(searchValue))).Count();

                    return vList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(Employee_Salary entity)
        {
            try
            {
                int intCount = 0;
                if (entity.Emp_Salary_Id > 0) //Update Validation
                    intCount = adbContext.employee_salary.Where(w => w.Emp_Salary_Id != entity.Emp_Salary_Id && w.Emp_Id == entity.Emp_Id && w.Company_Id == entity.Company_Id && w.Salary == entity.Salary).Count();
                else //Insert Validation
                    intCount = adbContext.employee_salary.Where(w => w.Company_Id == entity.Company_Id && w.Emp_Id == entity.Emp_Id && w.Salary == entity.Salary).Count();
                return (intCount > 0 ? true : false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}