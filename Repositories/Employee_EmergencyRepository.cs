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
    public class Employee_EmergencyRepository<T> : ICommonRepository<Employee_Emergency>
    {
        private readonly ApplicationDbContext adbContext;

        public Employee_EmergencyRepository()
        {
            adbContext = Startup.applicationDbContext;
        }


        public async Task<IEnumerable<Employee_Emergency>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    IEnumerable<Employee_Emergency> vList = adbContext.employee_emergency.Take(RecordLimit).ToList();
                    return await Task.FromResult(vList);
                }
                else
                {
                    IEnumerable<Employee_Emergency> vList = adbContext.employee_emergency.ToList();
                    return await Task.FromResult(vList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_Emergency>> GetByEmp_Id(int emp_Id)
        {
            try
            {
                IEnumerable<Employee_Emergency> vList = adbContext.employee_emergency.Where(w => w.Emp_Id == emp_Id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_Emergency>> Get(int id)
        {
            try
            {
                IEnumerable<Employee_Emergency> vList = adbContext.employee_emergency.Where(w => w.Emp_Emergency_Id == id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(Employee_Emergency entity)
        {
            try
            {
                entity.AddedOn = DateTime.Now;
                adbContext.employee_emergency.Add(entity);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert_Multiple(IList<Employee_Emergency> entity)
        {
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var employee in entity)
                    {
                        employee.AddedOn = DateTime.Now;
                        adbContext.employee_emergency.Add(employee);
                    }
                    await Task.FromResult(adbContext.SaveChangesAsync());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Employee_Emergency entity)
        {
            try
            {
                var lstEmp_EmergencyDetail = adbContext.employee_emergency.Where(x => x.Emp_Emergency_Id == entity.Emp_Emergency_Id && x.Emp_Id == entity.Emp_Id).FirstOrDefault();
                if (lstEmp_EmergencyDetail != null)
                {
                    lstEmp_EmergencyDetail.Emp_Id = entity.Emp_Id;
                    lstEmp_EmergencyDetail.ContactName = entity.ContactName;
                    lstEmp_EmergencyDetail.ContactNo = entity.ContactNo;
                    lstEmp_EmergencyDetail.Relationship = entity.Relationship;
                    lstEmp_EmergencyDetail.isDefault = entity.isDefault;

                    lstEmp_EmergencyDetail.isActive = entity.isActive;
                    lstEmp_EmergencyDetail.UpdatedBy = entity.UpdatedBy;
                    lstEmp_EmergencyDetail.UpdatedOn = DateTime.Now;

                    adbContext.employee_emergency.Update(lstEmp_EmergencyDetail);
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

        public async Task Update_EmergencyDetails(IList<Employee_Emergency> entity)
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
                var vList = adbContext.employee_emergency.Where(w => w.Emp_Id == emp_Id && w.isActive != isActive).ToList();
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
                var vList = adbContext.employee_emergency.Where(w => w.Emp_Emergency_Id == id && w.isActive != isActive).FirstOrDefault();
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
                var vList = adbContext.employee_emergency.Where(w => w.Emp_Id == emp_Id).ToList();
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
                var vList = adbContext.employee_emergency.Where(w => w.Emp_Emergency_Id == id).FirstOrDefault();
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

        public async Task<IEnumerable<Employee_Emergency>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    return adbContext.employee_emergency.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    var vList = adbContext.employee_emergency.Where(w => new[] { Convert.ToString(w.Emp_Id), w.ContactName, w.ContactNo, w.Relationship }.Any(a => a.Contains(searchValue))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
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
                    var vCount = (from emp in adbContext.employee_emergency
                                  select emp.Emp_Emergency_Id).Count();
                    return vCount;
                }
                else
                {
                    var vList = adbContext.employee_emergency.
                        Where(w => new[] { Convert.ToString(w.Emp_Id), w.ContactName, w.ContactNo, w.Relationship }.Any(a => a.Contains(searchValue))).Count();

                    return vList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(Employee_Emergency entity)
        {
            try
            {
                int intCount = 0;
                if (entity.Emp_Emergency_Id > 0) //Update Validation
                    intCount = adbContext.employee_emergency.Where(w => w.Emp_Emergency_Id != entity.Emp_Emergency_Id && w.Emp_Id == entity.Emp_Id && w.ContactName == entity.ContactName && w.ContactNo == entity.ContactNo && w.Relationship == entity.Relationship).Count();
                else //Insert Validation
                    intCount = adbContext.employee_emergency.Where(w => w.ContactName == entity.ContactName && w.ContactNo == entity.ContactNo && w.Relationship == entity.Relationship).Count();
                return (intCount > 0 ? true : false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}