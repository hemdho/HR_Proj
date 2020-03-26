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
    public class Employee_RightToWorkRepository<T> : ICommonRepository<Employee_RightToWork>
    {
        private readonly ApplicationDbContext adbContext;

        public Employee_RightToWorkRepository()
        {
            adbContext = Startup.applicationDbContext;
        }

        public async Task<IEnumerable<Employee_RightToWork>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    IEnumerable<Employee_RightToWork> vList = adbContext.employee_righttowork.Take(RecordLimit).ToList();
                    return await Task.FromResult(vList);
                }
                else
                {
                    IEnumerable<Employee_RightToWork> vList = adbContext.employee_righttowork.ToList();
                    return await Task.FromResult(vList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_RightToWork>> GetByEmp_Id(int emp_Id)
        {
            try
            {
                IEnumerable<Employee_RightToWork> vList = adbContext.employee_righttowork.Where(w => w.Emp_Id == emp_Id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_RightToWork>> Get(int id)
        {
            try
            {
                IEnumerable<Employee_RightToWork> vList = adbContext.employee_righttowork.Where(w => w.RightToWork_Id == id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(Employee_RightToWork entity)
        {
            try
            {
                entity.AddedOn = DateTime.Now;
                adbContext.employee_righttowork.Add(entity);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert_Multiple(IList<Employee_RightToWork> entity)
        {
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var employee in entity)
                    {
                        employee.AddedOn = DateTime.Now;
                        adbContext.employee_righttowork.Add(employee);
                    }
                    await Task.FromResult(adbContext.SaveChangesAsync());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Employee_RightToWork entity)
        {
            try
            {
                var vList = adbContext.employee_righttowork.Where(x => x.RightToWork_Id == entity.RightToWork_Id && x.Emp_Id == entity.Emp_Id).FirstOrDefault();
                if (vList != null)
                {
                    vList.Emp_Id = entity.Emp_Id;
                    vList.Category = entity.Category;
                    vList.Description = entity.Description;
                    vList.IssueDate = entity.IssueDate;
                    vList.IssueAuthority = entity.IssueAuthority;
                    vList.IssueCountry = entity.IssueCountry;
                    vList.ExpiryDate = entity.ExpiryDate;
                    vList.Emp_Doc_Id = entity.Emp_Doc_Id;
                    vList.isRequired = entity.isRequired;
                    vList.Notes = entity.Notes;

                    vList.isActive = entity.isActive;
                    vList.UpdatedBy = entity.UpdatedBy;
                    vList.UpdatedOn = DateTime.Now;

                    adbContext.employee_righttowork.Update(vList);
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

        public async Task Update_RightToWork(IList<Employee_RightToWork> entity)
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
                var vList = adbContext.employee_righttowork.Where(w => w.Emp_Id == emp_Id && w.isActive != isActive).ToList();
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
                var vList = adbContext.employee_righttowork.Where(w => w.RightToWork_Id == id && w.isActive != isActive).FirstOrDefault();
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
                var vList = adbContext.employee_righttowork.Where(w => w.Emp_Id == emp_Id).ToList();
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
                var vList = adbContext.employee_righttowork.Where(w => w.RightToWork_Id == id).FirstOrDefault();
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

        public async Task<IEnumerable<Employee_RightToWork>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    return adbContext.employee_righttowork.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    var vList = adbContext.employee_righttowork.Where(w => new[] { Convert.ToString(w.Emp_Id), w.Category, w.Description, w.IssueAuthority, w.IssueCountry, w.Notes }.Any(a => a.Contains(searchValue))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
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
                    var vCount = (from emp in adbContext.employee_righttowork
                                  select emp.RightToWork_Id).Count();
                    return vCount;
                }
                else
                {
                    var vList = adbContext.employee_righttowork.
                        Where(w => new[] { Convert.ToString(w.Emp_Id), w.Category, w.Description, w.IssueAuthority, w.IssueCountry, w.Notes }.Any(a => a.Contains(searchValue))).Count();

                    return vList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(Employee_RightToWork entity)
        {
            try
            {
                int intCount = 0;
                if (entity.RightToWork_Id > 0) //Update Validation
                    intCount = adbContext.employee_righttowork.Where(w => w.RightToWork_Id != entity.RightToWork_Id && w.Emp_Id == entity.Emp_Id && w.Category == entity.Category).Count();
                else //Insert Validation
                    intCount = adbContext.employee_righttowork.Where(w => w.Emp_Id == entity.Emp_Id && w.Category == entity.Category).Count();
                return (intCount > 0 ? true : false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}