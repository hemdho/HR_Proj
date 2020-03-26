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
    public class Employee_ContactRepository<T> : ICommonRepository<Employee_Contact>
    {
        private readonly ApplicationDbContext adbContext;

        public Employee_ContactRepository()
        {
            adbContext = Startup.applicationDbContext;
        }

        public async Task<IEnumerable<Employee_Contact>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    IEnumerable<Employee_Contact> vList = adbContext.employee_contact.Take(RecordLimit).ToList();
                    return await Task.FromResult(vList);
                }
                else
                {
                    IEnumerable<Employee_Contact> vList = adbContext.employee_contact.ToList();
                    return await Task.FromResult(vList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_Contact>> GetByEmp_Id(int emp_Id)
        {
            try
            {
                IEnumerable<Employee_Contact> vList = adbContext.employee_contact.Where(w => w.Emp_Id == emp_Id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<Employee_Contact>> Get(int id)
        {
            try
            {
                IEnumerable<Employee_Contact> vList = adbContext.employee_contact.Where(w => w.Emp_Contact_Id == id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(Employee_Contact entity)
        {
            try
            {
                entity.AddedOn = DateTime.Now;
                adbContext.employee_contact.Add(entity);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Insert_Multiple(IList<Employee_Contact> entity)
        {
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var employee in entity)
                    {
                        employee.AddedOn = DateTime.Now;
                        adbContext.employee_contact.Add(employee);
                    }
                    await Task.FromResult(adbContext.SaveChangesAsync());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Employee_Contact entity)
        {
            try
            {
                var lstEmp_contact = adbContext.employee_contact.Where(x => x.Emp_Contact_Id == entity.Emp_Contact_Id && x.Emp_Id == entity.Emp_Id).FirstOrDefault();
                if (lstEmp_contact != null)
                {
                    lstEmp_contact.Emp_Id = entity.Emp_Id;
                    lstEmp_contact.Contact_Type = entity.Contact_Type;
                    lstEmp_contact.Contact_Value = entity.Contact_Value;
                    lstEmp_contact.isDefault = entity.isDefault;
                    lstEmp_contact.Version_Id = entity.Version_Id;

                    lstEmp_contact.isActive = entity.isActive;
                    lstEmp_contact.UpdatedBy = entity.UpdatedBy;
                    lstEmp_contact.UpdatedOn = DateTime.Now;

                    adbContext.employee_contact.Update(lstEmp_contact);
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

        public async Task Update_Contact(IList<Employee_Contact> entity)
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
                var vList = adbContext.employee_contact.Where(w => w.Emp_Id == emp_Id && w.isActive != isActive).ToList();
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
                var vList = adbContext.employee_contact.Where(w => w.Emp_Contact_Id == id && w.isActive != isActive).FirstOrDefault();
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
                var vList = adbContext.employee_contact.Where(w => w.Emp_Id == emp_Id).ToList();
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
                var vList = adbContext.employee_contact.Where(w => w.Emp_Contact_Id == id).FirstOrDefault();
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


        public async Task<IEnumerable<Employee_Contact>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    return adbContext.employee_contact.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    var vList = adbContext.employee_contact.Where(w => new[] { Convert.ToString(w.Emp_Id), w.Contact_Type, w.Contact_Value, w.Version_Id }.Any(a => a.Contains(searchValue))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
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
                    var vCount = (from emp in adbContext.employee_contact
                                  select emp.Emp_Contact_Id).Count();
                    return vCount;
                }
                else
                {
                    var vList = adbContext.employee_contact.
                        Where(w => new[] { Convert.ToString(w.Emp_Id), w.Contact_Type, w.Contact_Value, w.Version_Id }.Any(a => a.Contains(searchValue))).Count();

                    return vList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(Employee_Contact entity)
        {
            try
            {
                int intCount = 0;
                if (entity.Emp_Contact_Id > 0) //Update Validation
                    intCount = adbContext.employee_contact.Where(w => w.Emp_Contact_Id != entity.Emp_Contact_Id && w.Emp_Id == entity.Emp_Id && w.Contact_Type == entity.Contact_Type && w.Contact_Value == entity.Contact_Value).Count();
                else //Insert Validation
                    intCount = adbContext.employee_contact.Where(w => w.Emp_Id == entity.Emp_Id && w.Contact_Type == entity.Contact_Type && w.Contact_Value == entity.Contact_Value).Count();
                return (intCount > 0 ? true : false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
