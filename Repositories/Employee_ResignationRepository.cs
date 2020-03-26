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
    public class Employee_ResignationRepository<T> : ICommonRepository<Employee_Resignation>
    {
        private readonly ApplicationDbContext adbContext;

        public Employee_ResignationRepository()
        {
            adbContext = Startup.applicationDbContext;
        }


        public async Task<IEnumerable<Employee_Resignation>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    IEnumerable<Employee_Resignation> vList = adbContext.employee_resignation.Take(RecordLimit).ToList();
                    return await Task.FromResult(vList);
                }
                else
                {
                    IEnumerable<Employee_Resignation> vList = adbContext.employee_resignation.ToList();
                    return await Task.FromResult(vList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_Resignation>> GetByEmp_Id(int emp_Id)
        {
            try
            {
                IEnumerable<Employee_Resignation> vList = adbContext.employee_resignation.Where(w => w.Emp_Id == emp_Id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_Resignation>> Get(int id)
        {
            try
            {
                IEnumerable<Employee_Resignation> vList = adbContext.employee_resignation.Where(w => w.Emp_Resignation_Id == id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(Employee_Resignation entity)
        {
            try
            {
                entity.AddedOn = DateTime.Now;
                adbContext.employee_resignation.Add(entity);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert_Multiple(IList<Employee_Resignation> entity)
        {
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var employee in entity)
                    {
                        employee.AddedOn = DateTime.Now;
                        adbContext.employee_resignation.Add(employee);
                    }
                    await Task.FromResult(adbContext.SaveChangesAsync());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Employee_Resignation entity)
        {
            try
            {
                var vList = adbContext.employee_resignation.Where(x => x.Emp_Resignation_Id == entity.Emp_Resignation_Id && x.Emp_Id == entity.Emp_Id).FirstOrDefault();
                if (vList != null)
                {
                    vList.Emp_Id = entity.Emp_Id;
                    vList.ResignationDate = entity.ResignationDate;
                    vList.NoticePeriod = entity.NoticePeriod;
                    vList.LeaveDate = entity.LeaveDate;
                    vList.LeaveReason = entity.LeaveReason;
                    vList.LastWorkingDate = entity.LastWorkingDate;
                    vList.Notes = entity.Notes;

                    vList.isActive = entity.isActive;
                    vList.UpdatedBy = entity.UpdatedBy;
                    vList.UpdatedOn = DateTime.Now;

                    adbContext.employee_resignation.Update(vList);
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

        public async Task Update_Resignation(IList<Employee_Resignation> entity)
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
                var vList = adbContext.employee_resignation.Where(w => w.Emp_Id == emp_Id && w.isActive != isActive).ToList();
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
                var vList = adbContext.employee_resignation.Where(w => w.Emp_Resignation_Id == id && w.isActive != isActive).FirstOrDefault();
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
                var vList = adbContext.employee_resignation.Where(w => w.Emp_Id == emp_Id).ToList();
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
                var vList = adbContext.employee_resignation.Where(w => w.Emp_Resignation_Id == id).FirstOrDefault();
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

        public async Task<IEnumerable<Employee_Resignation>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    return adbContext.employee_resignation.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    var vList = adbContext.employee_resignation.Where(w => new[] { Convert.ToString(w.Emp_Id), w.NoticePeriod, w.LeaveReason, w.Notes }.Any(a => a.Contains(searchValue))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
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
                    var vCount = (from emp in adbContext.employee_resignation
                                  select emp.Emp_Resignation_Id).Count();
                    return vCount;
                }
                else
                {
                    var vList = adbContext.employee_resignation.
                        Where(w => new[] { Convert.ToString(w.Emp_Id), w.NoticePeriod, w.LeaveReason, w.Notes }.Any(a => a.Contains(searchValue))).Count();

                    return vList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(Employee_Resignation entity)
        {
            try
            {
                int intCount = 0;
                if (entity.Emp_Resignation_Id > 0) //Update Validation
                    intCount = adbContext.employee_resignation.Where(w => w.Emp_Resignation_Id != entity.Emp_Resignation_Id && w.Emp_Id == entity.Emp_Id && w.NoticePeriod == entity.NoticePeriod).Count();
                else //Insert Validation
                    intCount = adbContext.employee_resignation.Where(w => w.Emp_Id == entity.Emp_Id && w.NoticePeriod == entity.NoticePeriod).Count();
                return (intCount > 0 ? true : false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
