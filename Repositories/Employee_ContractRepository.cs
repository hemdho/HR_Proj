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
    public class Employee_ContractRepository<T> : ICommonRepository<Employee_Contract>
    {
        private readonly ApplicationDbContext adbContext;

        public Employee_ContractRepository()
        {
            adbContext = Startup.applicationDbContext;
        }

        public async Task<IEnumerable<Employee_Contract>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    IEnumerable<Employee_Contract> vList = adbContext.employee_contract.Take(RecordLimit).ToList();
                    return await Task.FromResult(vList);
                }
                else
                {
                    IEnumerable<Employee_Contract> vList = adbContext.employee_contract.ToList();
                    return await Task.FromResult(vList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_Contract>> GetByEmp_Id(int emp_Id)
        {
            try
            {
                IEnumerable<Employee_Contract> vList = adbContext.employee_contract.Where(w => w.Emp_Id == emp_Id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_Contract>> Get(int id)
        {
            try
            {
                IEnumerable<Employee_Contract> vList = adbContext.employee_contract.Where(w => w.Emp_Contract_Id == id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(Employee_Contract entity)
        {
            try
            {
                entity.AddedOn = DateTime.Now;
                adbContext.employee_contract.Add(entity);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert_Multiple(IList<Employee_Contract> entity)
        {
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var employee in entity)
                    {
                        employee.AddedOn = DateTime.Now;
                        adbContext.employee_contract.Add(employee);
                    }
                    await Task.FromResult(adbContext.SaveChangesAsync());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Employee_Contract entity)
        {
            try
            {
                Employee_Contract lstEmp_contract = new Employee_Contract();
                lstEmp_contract = adbContext.employee_contract.Where(x => x.Emp_Contract_Id == entity.Emp_Contract_Id && x.Emp_Id == entity.Emp_Id).FirstOrDefault();
                if (lstEmp_contract != null)
                {
                    lstEmp_contract.Emp_Id = entity.Emp_Id;
                    lstEmp_contract.Emp_Contract_Code = entity.Emp_Contract_Code;
                    lstEmp_contract.Emp_Contract_Name = entity.Emp_Contract_Name;
                    lstEmp_contract.Emp_Contract_HoursDaily = entity.Emp_Contract_HoursDaily;
                    lstEmp_contract.Emp_Contract_HoursWeekly = entity.Emp_Contract_HoursWeekly;
                    lstEmp_contract.Emp_Contract_Days = entity.Emp_Contract_Days;
                    lstEmp_contract.Emp_Contract_Type = entity.Emp_Contract_Type;
                    lstEmp_contract.Emp_Contract_Start = entity.Emp_Contract_Start;
                    lstEmp_contract.Emp_Contract_End = entity.Emp_Contract_End;
                    lstEmp_contract.Emp_Doc_Id = entity.Emp_Doc_Id;
                    lstEmp_contract.isRequired = entity.isRequired;
                    lstEmp_contract.Notes = entity.Notes;
                    lstEmp_contract.Version_Id = entity.Version_Id;

                    lstEmp_contract.isActive = entity.isActive;
                    lstEmp_contract.UpdatedBy = entity.UpdatedBy;
                    lstEmp_contract.UpdatedOn = DateTime.Now;

                    adbContext.employee_contract.Update(lstEmp_contract);
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

        public async Task Update_Contract(IList<Employee_Contract> entity)
        {
            if (entity != null && entity.Count() > 0)
            {
                foreach (var employee in entity)
                {
                    await Update(employee);
                }
            }
        }

        public async Task ToogleStatusByEmp_Id(int emp_Id, short isActive)
        {
            try
            {
                //update flag isActive
                var vList = adbContext.employee_contract.Where(w => w.Emp_Id == emp_Id && w.isActive != isActive).ToList();
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
                var vList = adbContext.employee_contract.Where(w => w.Emp_Contract_Id == id && w.isActive != isActive).FirstOrDefault();
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
                var vList = adbContext.employee_contract.Where(w => w.Emp_Id == emp_Id).ToList();
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
                var vList = adbContext.employee_contract.Where(w => w.Emp_Contract_Id == id).FirstOrDefault();
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

        public async Task<IEnumerable<Employee_Contract>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    return adbContext.employee_contract.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    var vList = adbContext.employee_contract.Where(w => new[] { Convert.ToString(w.Emp_Id), w.Emp_Contract_Code, w.Emp_Contract_Name, Convert.ToString(w.Emp_Contract_HoursDaily), Convert.ToString(w.Emp_Contract_HoursWeekly), w.Emp_Contract_Days, w.Emp_Contract_Type, Convert.ToString(w.Emp_Doc_Id), w.Notes, w.Version_Id }.Any(a => a.Contains(searchValue))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
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
                    var vCount = (from emp in adbContext.employee_contract
                                  select emp.Emp_Contract_Id).Count();
                    return vCount;
                }
                else
                {
                    var vList = adbContext.employee_contract.
                        Where(w => new[] { Convert.ToString(w.Emp_Id), w.Emp_Contract_Code, w.Emp_Contract_Name, Convert.ToString(w.Emp_Contract_HoursDaily), Convert.ToString(w.Emp_Contract_HoursWeekly), w.Emp_Contract_Days, w.Emp_Contract_Type, Convert.ToString(w.Emp_Doc_Id), w.Notes, w.Version_Id }.Any(a => a.Contains(searchValue))).Count();

                    return vList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(Employee_Contract entity)
        {
            try
            {
                int intCount = 0;
                if (entity.Emp_Contract_Id > 0) //Update Validation
                    intCount = adbContext.employee_contract.Where(w => w.Emp_Contract_Id != entity.Emp_Contract_Id && w.Emp_Id == entity.Emp_Id && w.Emp_Contract_Code == entity.Emp_Contract_Code && w.Emp_Contract_Name == entity.Emp_Contract_Name).Count();
                else //Insert Validation
                    intCount = adbContext.employee_contract.Where(w => w.Emp_Id == entity.Emp_Id && w.Emp_Contract_Code == entity.Emp_Contract_Code && w.Emp_Contract_Name == entity.Emp_Contract_Name).Count();
                return (intCount > 0 ? true : false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
