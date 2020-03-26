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
    public class Employee_ReferenceRepository<T> : ICommonRepository<Employee_Reference>
    {
        private readonly ApplicationDbContext adbContext;

        public Employee_ReferenceRepository()
        {
            adbContext = Startup.applicationDbContext;
        }


        public async Task<IEnumerable<Employee_Reference>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    IEnumerable<Employee_Reference> vList = adbContext.employee_reference.Take(RecordLimit).ToList();
                    return await Task.FromResult(vList);
                }
                else
                {
                    IEnumerable<Employee_Reference> vList = adbContext.employee_reference.ToList();
                    return await Task.FromResult(vList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_Reference>> GetByEmp_Id(int emp_Id)
        {
            try
            {
                IEnumerable<Employee_Reference> vList = adbContext.employee_reference.Where(w => w.Emp_Id == emp_Id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_Reference>> Get(int id)
        {
            try
            {
                IEnumerable<Employee_Reference> vList = adbContext.employee_reference.Where(w => w.Emp_Ref_Id == id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(Employee_Reference entity)
        {
            try
            {
                entity.AddedOn = DateTime.Now;
                adbContext.employee_reference.Add(entity);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert_Multiple(IList<Employee_Reference> entity)
        {
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var employee in entity)
                    {
                        employee.AddedOn = DateTime.Now;
                        adbContext.employee_reference.Add(employee);
                    }
                    await Task.FromResult(adbContext.SaveChangesAsync());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Employee_Reference entity)
        {
            try
            {
                var vList = adbContext.employee_reference.Where(x => x.Emp_Ref_Id == entity.Emp_Ref_Id && x.Emp_Id == entity.Emp_Id).FirstOrDefault();
                if (vList != null)
                {
                    vList.Emp_Id = entity.Emp_Id;
                    vList.Ref_Name = entity.Ref_Name;
                    vList.Ref_ContactNo = entity.Ref_ContactNo;
                    vList.Ref_Relationship = entity.Ref_Relationship;

                    vList.isActive = entity.isActive;
                    vList.UpdatedBy = entity.UpdatedBy;
                    vList.UpdatedOn = DateTime.Now;

                    adbContext.employee_reference.Update(vList);
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

        public async Task Update_Reference(IList<Employee_Reference> entity)
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
                var vList = adbContext.employee_reference.Where(w => w.Emp_Id == emp_Id && w.isActive != isActive).ToList();
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
                var vList = adbContext.employee_reference.Where(w => w.Emp_Ref_Id == id && w.isActive != isActive).FirstOrDefault();
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
                var vList = adbContext.employee_reference.Where(w => w.Emp_Id == emp_Id).ToList();
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
                var vList = adbContext.employee_reference.Where(w => w.Emp_Ref_Id == id).FirstOrDefault();
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

        public async Task<IEnumerable<Employee_Reference>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    return adbContext.employee_reference.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    var vList = adbContext.employee_reference.Where(w => new[] { Convert.ToString(w.Emp_Id), w.Ref_Name, w.Ref_ContactNo, w.Ref_Relationship}.Any(a => a.Contains(searchValue))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
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
                    var vCount = (from emp in adbContext.employee_reference
                                  select emp.Emp_Ref_Id).Count();
                    return vCount;
                }
                else
                {
                    var vList = adbContext.employee_reference.
                        Where(w => new[] { Convert.ToString(w.Emp_Id), w.Ref_Name, w.Ref_ContactNo, w.Ref_Relationship }.Any(a => a.Contains(searchValue))).Count();

                    return vList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(Employee_Reference entity)
        {
            try
            {
                int intCount = 0;
                if (entity.Emp_Ref_Id > 0) //Update Validation
                    intCount = adbContext.employee_reference.Where(w => w.Emp_Ref_Id != entity.Emp_Ref_Id && w.Emp_Id == entity.Emp_Id && w.Ref_Name == entity.Ref_Name && w.Ref_ContactNo == entity.Ref_ContactNo && w.Ref_Relationship == entity.Ref_Relationship).Count();
                else //Insert Validation
                    intCount = adbContext.employee_reference.Where(w => w.Emp_Id == entity.Emp_Id && w.Ref_Name == entity.Ref_Name && w.Ref_ContactNo == entity.Ref_ContactNo && w.Ref_Relationship == entity.Ref_Relationship).Count();
                return (intCount > 0 ? true : false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
