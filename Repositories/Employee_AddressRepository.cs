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
    public class Employee_AddressRepository<T> : ICommonRepository<Employee_Address>
    {
        private readonly ApplicationDbContext adbContext;

        public Employee_AddressRepository()
        {
            adbContext = Startup.applicationDbContext;
        }

        public async Task<IEnumerable<Employee_Address>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    IEnumerable<Employee_Address> vList = adbContext.employee_address.Take(RecordLimit).ToList();
                    return await Task.FromResult(vList);
                }
                else
                {
                    IEnumerable<Employee_Address> vList = adbContext.employee_address.ToList();
                    return await Task.FromResult(vList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_Address>> GetByEmp_Id(int emp_Id)
        {
            try
            {
                IEnumerable<Employee_Address> vList = adbContext.employee_address.Where(w => w.Emp_Id == emp_Id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_Address>> Get(int id)
        {
            try
            {
                IEnumerable<Employee_Address> vList = adbContext.employee_address.Where(w => w.Emp_Address_Id == id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(Employee_Address entity)
        {
            try
            {
                entity.AddedOn = DateTime.Now;
                adbContext.employee_address.Add(entity);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert_Multiple(IList<Employee_Address> entity)
        {
            try
            {
                if(entity!= null && entity.Count() > 0)
                {
                    foreach (var employee in entity)
                    {
                        employee.AddedOn = DateTime.Now;
                        adbContext.employee_address.Add(employee);
                    }
                    await Task.FromResult(adbContext.SaveChangesAsync());
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
                var vList = adbContext.employee_address.Where(w => w.Emp_Id == emp_Id && w.isActive != isActive).ToList();
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
                var vList = adbContext.employee_address.Where(w => w.Emp_Address_Id == id && w.isActive != isActive).FirstOrDefault();
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

        public async Task Update(Employee_Address entity)
        {
            try
            {
                //Update Old Department
                var vList = adbContext.employee_address.Where(w => w.Emp_Id == entity.Emp_Id).FirstOrDefault();
                if (vList != null)
                {
                    vList.Emp_Id = entity.Emp_Id;
                    vList.Address_Type = entity.Address_Type;
                    vList.Address1 = entity.Address1;
                    vList.Address2 = entity.Address2;
                    vList.Address3 = entity.Address3;
                    vList.Address4 = entity.Address4;
                    vList.PostCode = entity.PostCode;
                    vList.City = entity.City;
                    vList.State = entity.State;
                    vList.Country = entity.Country;
                    vList.LandlineNo = entity.LandlineNo;
                    vList.isDefault = entity.isDefault;
                    vList.Emp_Doc_Id = entity.Emp_Doc_Id;
                    vList.isActive = entity.isActive;
                    vList.UpdatedBy = entity.UpdatedBy;
                    vList.UpdatedOn = DateTime.Now;

                    adbContext.employee_address.Update(vList);
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

        public async Task Update_Addresses(IList<Employee_Address> entity)
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

        public async Task DeleteByEmp_Id(int emp_Id)
        {
            try
            {
                var vList = adbContext.employee_address.Where(w => w.Emp_Id == emp_Id).ToList();
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
                var vList = adbContext.employee_address.Where(w => w.Emp_Address_Id == id).FirstOrDefault();
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

        public async Task<IEnumerable<Employee_Address>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    return adbContext.employee_address.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    var vList = adbContext.employee_address.Where(w => new[] { Convert.ToString(w.Emp_Id), Convert.ToString(w.Emp_Address_Id), w.City, w.State, w.Country, w.Address1, w.Address2, w.Address3, w.Address4, w.Address_Type, w.PostCode }.Any(a => a.Contains(searchValue))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
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
                    var vCount = (from emp in adbContext.employee_address
                                  select emp.Emp_Address_Id).Count();
                    return vCount;
                }
                else
                {
                    var vList = adbContext.employee_address.
                        Where(w => new[] { Convert.ToString(w.Emp_Id), Convert.ToString(w.Emp_Address_Id),
                            w.City, w.State, w.Country, w.Address1, w.Address2,
                            w.Address3, w.Address4, w.Address_Type, w.PostCode }.Any(a => a.Contains(searchValue))).Count();

                    return vList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(Employee_Address entity)
        {
            try
            {
                int intCount = 0;
                if (entity.Emp_Address_Id > 0) //Update Validation
                    intCount = adbContext.employee_address.Where(w => w.Emp_Address_Id != entity.Emp_Address_Id && w.Emp_Id == entity.Emp_Id && w.Address1 == entity.Address1 && w.Address2 == entity.Address2 && w.Address3 == entity.Address3 && w.Address4 == entity.Address4).Count();
                else //Insert Validation
                    intCount = adbContext.employee_address.Where(w => w.Address1 == entity.Address1 && w.Address2 == entity.Address2 && w.Address3 == entity.Address3 && w.Address4 == entity.Address4).Count();
                return (intCount > 0 ? true : false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}