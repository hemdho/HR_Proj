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
    public class Employee_ProbationRepository<T> : ICommonRepository<Employee_Probation>
    {
        private readonly ApplicationDbContext adbContext;

        public Employee_ProbationRepository()
        {
            adbContext = Startup.applicationDbContext;
        }

        public async Task<IEnumerable<Employee_Probation>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    IEnumerable<Employee_Probation> vList = adbContext.employee_probation.Take(RecordLimit).ToList();
                    return await Task.FromResult(vList);
                }
                else
                {
                    IEnumerable<Employee_Probation> vList = adbContext.employee_probation.ToList();
                    return await Task.FromResult(vList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_Probation>> GetByEmp_Id(int emp_Id)
        {
            try
            {
                IEnumerable<Employee_Probation> vList = adbContext.employee_probation.Where(w => w.Emp_Id == emp_Id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_Probation>> Get(int id)
        {
            try
            {
                IEnumerable<Employee_Probation> vList = adbContext.employee_probation.Where(w => w.Emp_Probation_Id == id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(Employee_Probation entity)
        {
            try
            {
                entity.AddedOn = DateTime.Now;
                adbContext.employee_probation.Add(entity);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert_Multiple(IList<Employee_Probation> entity)
        {
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var employee in entity)
                    {
                        employee.AddedOn = DateTime.Now;
                        adbContext.employee_probation.Add(employee);
                    }
                    await Task.FromResult(adbContext.SaveChangesAsync());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Employee_Probation entity)
        {
            try
            {
                var vList = adbContext.employee_probation.Where(x => x.Emp_Probation_Id == entity.Emp_Probation_Id && x.Emp_Id == entity.Emp_Id).FirstOrDefault();
                if (vList != null)
                {
                    vList.Emp_Id = entity.Emp_Id;
                    vList.ProbationEndDate = entity.ProbationEndDate;
                    vList.ProbationPeriod = entity.ProbationPeriod;
                    vList.NextReviewDate = entity.NextReviewDate;
                    vList.Status = entity.Status;
                    vList.Notes = entity.Notes;
                    vList.isActive = entity.isActive;
                    vList.UpdatedBy = entity.UpdatedBy;
                    vList.UpdatedOn = DateTime.Now;

                    adbContext.employee_probation.Update(vList);
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
        
        public async Task Update_Probation(IList<Employee_Probation> entity)
        {
            try
            {
                if(entity != null && entity.Count() > 0)
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
                var vList = adbContext.employee_probation.Where(w => w.Emp_Id == emp_Id && w.isActive != isActive).ToList();
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
                var vList = adbContext.employee_probation.Where(w => w.Emp_Probation_Id == id && w.isActive != isActive).FirstOrDefault();
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
                var vList = adbContext.employee_probation.Where(w => w.Emp_Id == emp_Id).ToList();
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
                var vList = adbContext.employee_probation.Where(w => w.Emp_Probation_Id == id).FirstOrDefault();
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

        public async Task<IEnumerable<Employee_Probation>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    return adbContext.employee_probation.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    var vList = adbContext.employee_probation.Where(w => new[] { Convert.ToString(w.Emp_Id), w.ProbationPeriod, w.Status, w.Notes }.Any(a => a.Contains(searchValue))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
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
                    var vCount = (from emp in adbContext.employee_probation
                                  select emp.Emp_Probation_Id).Count();
                    return vCount;
                }
                else
                {
                    var vList = adbContext.employee_probation.
                        Where(w => new[] { Convert.ToString(w.Emp_Id), w.ProbationPeriod, w.Status, w.Notes }.Any(a => a.Contains(searchValue))).Count();

                    return vList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(Employee_Probation entity)
        {
            try
            {
                int intCount = 0;
                if (entity.Emp_Probation_Id > 0) //Update Validation
                    intCount = adbContext.employee_probation.Where(w => w.Emp_Probation_Id != entity.Emp_Probation_Id && w.Emp_Id == entity.Emp_Id && w.ProbationPeriod == entity.ProbationPeriod).Count();
                else //Insert Validation
                    intCount = adbContext.employee_probation.Where(w => w.Emp_Id == entity.Emp_Id && w.ProbationPeriod == entity.ProbationPeriod ).Count();
                return (intCount > 0 ? true : false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}