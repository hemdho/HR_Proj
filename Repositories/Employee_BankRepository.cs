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
    public class Employee_BankRepository<T> : ICommonRepository<Employee_Bank>
    {
        private readonly ApplicationDbContext adbContext;

        public Employee_BankRepository()
        {
            adbContext = Startup.applicationDbContext;
        }

        public async Task<IEnumerable<Employee_Bank>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    IEnumerable<Employee_Bank> vList = adbContext.employee_bank.Take(RecordLimit).ToList();
                    return await Task.FromResult(vList);
                }
                else
                {
                    IEnumerable<Employee_Bank> vList = adbContext.employee_bank.ToList();
                    return await Task.FromResult(vList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_Bank>> GetByEmp_Id(int emp_Id)
        {
            try
            {
                IEnumerable<Employee_Bank> vList = adbContext.employee_bank.Where(w => w.Emp_Id == emp_Id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_Bank>> Get(int id)
        {
            try
            {
                IEnumerable<Employee_Bank> vList = adbContext.employee_bank.Where(w => w.Emp_Bank_Id == id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(Employee_Bank entity)
        {
            try
            {
                entity.AddedOn = DateTime.Now;
                adbContext.employee_bank.Add(entity);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Insert_Multiple(IList<Employee_Bank> entity)
        {
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var employee in entity)
                    {
                        employee.AddedOn = DateTime.Now;
                        adbContext.employee_bank.Add(employee);
                    }
                    await Task.FromResult(adbContext.SaveChangesAsync());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Employee_Bank entity)
        {
            try
            {
                var lstEmp_bank = adbContext.employee_bank.Where(x => x.Emp_Bank_Id == entity.Emp_Bank_Id && x.Emp_Id == entity.Emp_Id).FirstOrDefault();
                if (lstEmp_bank != null)
                {
                    lstEmp_bank.Emp_Id = entity.Emp_Id;
                    lstEmp_bank.Bank_Code = entity.Bank_Code;
                    lstEmp_bank.Bank_Name = entity.Bank_Name;
                    lstEmp_bank.Account_Title = entity.Account_Title;
                    lstEmp_bank.Account_No = entity.Account_No;
                    lstEmp_bank.Account_Holder = entity.Account_Holder;
                    lstEmp_bank.Account_Type = entity.Account_Type;
                    lstEmp_bank.Account_Code = entity.Account_Code;
                    lstEmp_bank.isPayed = entity.isPayed;
                    lstEmp_bank.Emp_Doc_Id = entity.Emp_Doc_Id;
                    lstEmp_bank.isRequired = entity.isRequired;
                    lstEmp_bank.Notes = entity.Notes;
                    lstEmp_bank.Version_Id = entity.Version_Id;


                    lstEmp_bank.isActive = entity.isActive;
                    lstEmp_bank.UpdatedBy = entity.UpdatedBy;
                    lstEmp_bank.UpdatedOn = DateTime.Now;

                    adbContext.employee_bank.Update(lstEmp_bank);
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
        public async Task Update_BankDetails(IList<Employee_Bank> entity)
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
                var vList = adbContext.employee_bank.Where(w => w.Emp_Id == emp_Id && w.isActive != isActive).ToList();
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
                var vList = adbContext.employee_bank.Where(w => w.Emp_Bank_Id == id && w.isActive != isActive).FirstOrDefault();
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
                var vList = adbContext.employee_bank.Where(w => w.Emp_Id == emp_Id).ToList();
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
                var vList = adbContext.employee_bank.Where(w => w.Emp_Bank_Id == id).FirstOrDefault();
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

        public async Task<IEnumerable<Employee_Bank>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    return adbContext.employee_bank.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    var vList = adbContext.employee_bank.Where(w => new[] { Convert.ToString(w.Emp_Id), w.Bank_Name, w.Bank_Code, w.Account_Code, Convert.ToString(w.Account_No), w.Account_Holder, w.Account_Type, w.Notes, w.Version_Id }.Any(a => a.Contains(searchValue))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
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
                    var vCount = (from emp in adbContext.employee_bank
                                  select emp.Emp_Bank_Id).Count();
                    return vCount;
                }
                else
                {
                    var vList = adbContext.employee_bank.
                        Where(w => new[] { Convert.ToString(w.Emp_Id), w.Bank_Name, w.Bank_Code, w.Account_Code, Convert.ToString(w.Account_Title), Convert.ToString(w.Account_No), w.Account_Holder, w.Account_Type, w.Notes, w.Version_Id }.Any(a => a.Contains(searchValue))).Count();

                    return vList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(Employee_Bank entity)
        {
            try
            {
                int intCount = 0;
                if (entity.Emp_Bank_Id > 0) //Update Validation
                    intCount = adbContext.employee_bank.Where(w => w.Emp_Bank_Id != entity.Emp_Bank_Id && w.Emp_Id == entity.Emp_Id && w.Bank_Name == entity.Bank_Name && w.Bank_Code == entity.Bank_Code && w.Account_Code == entity.Account_Code && w.Account_No == entity.Account_No).Count();
                else //Insert Validation
                    intCount = adbContext.employee_bank.Where(w => w.Bank_Name == entity.Bank_Name && w.Bank_Code == entity.Bank_Code && w.Account_Code == entity.Account_Code && w.Account_No == entity.Account_No).Count();
                return (intCount > 0 ? true : false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}