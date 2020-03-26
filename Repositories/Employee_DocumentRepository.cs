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
    public class Employee_DocumentRepository<T> : ICommonRepository<Employee_Document>
    {
        private readonly ApplicationDbContext adbContext;

        public Employee_DocumentRepository()
        {
            adbContext = Startup.applicationDbContext;
        }
        public async Task<IEnumerable<Employee_Document>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    IEnumerable<Employee_Document> vList = adbContext.employee_document.Take(RecordLimit).ToList();
                    return await Task.FromResult(vList);
                }
                else
                {
                    IEnumerable<Employee_Document> vList = adbContext.employee_document.ToList();
                    return await Task.FromResult(vList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_Document>> GetByEmp_Id(int emp_Id)
        {
            try
            {
                IEnumerable<Employee_Document> vList = adbContext.employee_document.Where(w => w.Emp_Id == emp_Id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_Document>> Get(int id)
        {
            try
            {
                IEnumerable<Employee_Document> vList = adbContext.employee_document.Where(w => w.Emp_Doc_Id == id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task Insert(Employee_Document entity)
        {
            try
            {
                entity.AddedOn = DateTime.Now;
                adbContext.employee_document.Add(entity);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert_Multiple(IList<Employee_Document> entity)
        {
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var employee in entity)
                    {
                        employee.AddedOn = DateTime.Now;
                        adbContext.employee_document.Add(employee);
                    }
                    await Task.FromResult(adbContext.SaveChangesAsync());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task Update(Employee_Document entity)
        {
            try
            {
                var lstEmp_document = adbContext.employee_document.Where(x => x.Emp_Doc_Id == entity.Emp_Doc_Id && x.Emp_Id == entity.Emp_Id).FirstOrDefault();
                if (lstEmp_document != null)
                {
                    lstEmp_document.Emp_Id = entity.Emp_Id;
                    lstEmp_document.Parent_Emp_Doc_Id = entity.Parent_Emp_Doc_Id;
                    lstEmp_document.Doc_Id = entity.Doc_Id;
                    lstEmp_document.Emp_Doc_Name = entity.Emp_Doc_Name;
                    lstEmp_document.Doc_Path = entity.Doc_Path;
                    lstEmp_document.Notes = entity.Notes;

                    lstEmp_document.isActive = entity.isActive;
                    lstEmp_document.UpdatedBy = entity.UpdatedBy;
                    lstEmp_document.UpdatedOn = DateTime.Now;

                    adbContext.employee_document.Update(lstEmp_document);
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

        public async Task Update_Document(IList<Employee_Document> entity)
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
                var vList = adbContext.employee_document.Where(w => w.Emp_Id == emp_Id && w.isActive != isActive).ToList();
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
                var vList = adbContext.employee_document.Where(w => w.Emp_Doc_Id == id && w.isActive != isActive).FirstOrDefault();
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
                var vList = adbContext.employee_document.Where(w => w.Emp_Id == emp_Id).ToList();
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
                var vList = adbContext.employee_document.Where(w => w.Emp_Doc_Id == id).FirstOrDefault();
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

        public async Task<IEnumerable<Employee_Document>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    return adbContext.employee_document.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    var vList = adbContext.employee_document.Where(w => new[] { Convert.ToString(w.Emp_Id), w.Emp_Doc_Name, w.Notes }.Any(a => a.Contains(searchValue))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
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
                    var vCount = (from emp in adbContext.employee_document
                                  select emp.Emp_Doc_Id).Count();
                    return vCount;
                }
                else
                {
                    var vList = adbContext.employee_document.
                        Where(w => new[] { Convert.ToString(w.Emp_Id), w.Emp_Doc_Name, w.Notes }.Any(a => a.Contains(searchValue))).Count();

                    return vList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(Employee_Document entity)
        {
            try
            {
                int intCount = 0;
                if (entity.Emp_Doc_Id > 0) //Update Validation
                    intCount = adbContext.employee_document.Where(w => w.Emp_Doc_Id != entity.Emp_Doc_Id && w.Emp_Id == entity.Emp_Id && (w.Emp_Doc_Name == entity.Emp_Doc_Name)).Count();
                else //Insert Validation
                    intCount = adbContext.employee_document.Where(w => w.Emp_Id == entity.Emp_Id && (w.Emp_Doc_Name == entity.Emp_Doc_Name)).Count();
                return (intCount > 0 ? true : false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
