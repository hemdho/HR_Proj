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
    public class Employee_BasicInfoRepository<T> : ICommonRepository<Employee_BasicInfo>
    {
        private readonly ApplicationDbContext adbContext;

        public Employee_BasicInfoRepository()
        {
            adbContext = Startup.applicationDbContext;
        }

        public async Task<IEnumerable<Employee_BasicInfo>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    IEnumerable<Employee_BasicInfo> vList = adbContext.employee_basicinfo.Take(RecordLimit).ToList();
                    return await Task.FromResult(vList);
                }
                else
                {
                    IEnumerable<Employee_BasicInfo> vList = adbContext.employee_basicinfo.ToList();
                    return await Task.FromResult(vList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_BasicInfo>> GetByEmp_Id(int emp_Id)
        {
            try
            {
                IEnumerable<Employee_BasicInfo> vList = adbContext.employee_basicinfo.Where(w => w.Emp_Id == emp_Id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<Employee_BasicInfo>> Get(int id)
        {
            try
            {
                IEnumerable<Employee_BasicInfo> vList = adbContext.employee_basicinfo.Where(w => w.BasicInfo_Id == id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(Employee_BasicInfo entity)
        {
            try
            {
                if (entity != null)
                {
                    entity.AddedOn = DateTime.Now;
                    adbContext.employee_basicinfo.Add(entity);
                    await Task.FromResult(adbContext.SaveChangesAsync());
                }
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task Insert_Multiple(IList<Employee_BasicInfo> entity)
        {
            try
            {
                foreach (var employee in entity)
                {
                    employee.AddedOn = DateTime.Now;
                    adbContext.employee_basicinfo.Add(employee);
                }
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Employee_BasicInfo entity)
        {
            try
            {
                var lstEmp_basicinfo = adbContext.employee_basicinfo.Where(x => x.Emp_Id == entity.Emp_Id).FirstOrDefault();
                if (lstEmp_basicinfo != null)
                {
                    lstEmp_basicinfo.Emp_Id = entity.Emp_Id;
                    lstEmp_basicinfo.FirstName = entity.FirstName;
                    lstEmp_basicinfo.MiddleName = entity.MiddleName;
                    lstEmp_basicinfo.LastName = entity.LastName;
                    lstEmp_basicinfo.Title = entity.Title;
                    lstEmp_basicinfo.DOB = entity.DOB;
                    lstEmp_basicinfo.Gender = entity.Gender;
                    lstEmp_basicinfo.BloodGroup = entity.BloodGroup;
                    lstEmp_basicinfo.Nationality = entity.Nationality;
                    lstEmp_basicinfo.Ethnicity_Code = entity.Ethnicity_Code;
                    lstEmp_basicinfo.Version_Id = entity.Version_Id;


                    lstEmp_basicinfo.isActive = entity.isActive;
                    lstEmp_basicinfo.UpdatedBy = entity.UpdatedBy;
                    lstEmp_basicinfo.UpdatedOn = DateTime.Now;

                    adbContext.employee_basicinfo.Update(lstEmp_basicinfo);
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


        public async Task ToogleStatusByEmp_Id(int emp_Id, short isActive)
        {
            try
            {
                //update flag isActive
                var vList = adbContext.employee_basicinfo.Where(w => w.Emp_Id == emp_Id && w.isActive != isActive).ToList();
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
                var vList = adbContext.employee_basicinfo.Where(w => w.BasicInfo_Id == id && w.isActive != isActive).FirstOrDefault();
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
                var vList = adbContext.employee_basicinfo.Where(w => w.Emp_Id == emp_Id).ToList();
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
                var vList = adbContext.employee_basicinfo.Where(w => w.BasicInfo_Id == id).FirstOrDefault();
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

        public async Task<IEnumerable<Employee_BasicInfo>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    return adbContext.employee_basicinfo.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    var vList = adbContext.employee_basicinfo.Where(w => new[] { Convert.ToString(w.Emp_Id), w.FirstName, w.MiddleName, w.LastName, w.Title, w.Gender, w.BloodGroup, w.Nationality, w.Ethnicity_Code, w.Version_Id }.Any(a => a.Contains(searchValue))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
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
                    var vCount = (from emp in adbContext.employee_basicinfo
                                  select emp.BasicInfo_Id).Count();
                    return vCount;
                }
                else
                {
                    var vList = adbContext.employee_basicinfo.
                        Where(w => new[] { Convert.ToString(w.Emp_Id), w.FirstName, w.MiddleName, w.LastName, w.Title, w.Gender, w.BloodGroup, w.Nationality, w.Ethnicity_Code, w.Version_Id }.Any(a => a.Contains(searchValue))).Count();

                    return vList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(Employee_BasicInfo entity)
        {
            try
            {
                int intCount = 0;
                if (entity.BasicInfo_Id > 0) //Update Validation
                    intCount = adbContext.employee_basicinfo.Where(w => w.BasicInfo_Id != entity.BasicInfo_Id && w.Emp_Id == entity.Emp_Id && w.FirstName == entity.FirstName && w.MiddleName == entity.MiddleName && w.LastName == entity.LastName).Count();
                else //Insert Validation
                    intCount = adbContext.employee_basicinfo.Where(w => w.Emp_Id == entity.Emp_Id && w.FirstName == entity.FirstName && w.MiddleName == entity.MiddleName && w.LastName == entity.LastName).Count();
                return (intCount > 0 ? true : false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}