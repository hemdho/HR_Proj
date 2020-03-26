using HR.WebApi.DAL;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using HR.WebApi.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace HR.WebApi.Repositories
{
    public class DepartmentRepository<T> : ICommonRepository<DepartmentView>, ICommonQuery<DepartmentView>
    {
        private readonly ApplicationDbContext adbContext;

        public DepartmentRepository()
        {
            adbContext = Startup.applicationDbContext;
        }

        public async Task<IEnumerable<DepartmentView>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    var vList = (from dept in adbContext.department
                                 join comp in adbContext.company on dept.Company_Id equals comp.Company_Id
                                 where dept.isActive == 1 && comp.isActive == 1
                                 select new DepartmentView
                                 {
                                     Dept_Id = dept.Dept_Id,
                                     Dept_Code = dept.Dept_Code,
                                     Dept_Name = dept.Dept_Name,
                                     isActive = dept.isActive,
                                     Notes = dept.Notes,
                                     Company_Id = comp.Company_Id,
                                     Company_Name = comp.Company_Name
                                 }
                                ).Take(RecordLimit).ToList();
                    return await Task.FromResult(vList);
                }
                else
                {
                    var vList = (from dept in adbContext.department
                                 join comp in adbContext.company on dept.Company_Id equals comp.Company_Id
                                 where dept.isActive == 1 && comp.isActive == 1
                                 select new DepartmentView
                                 {
                                     Dept_Id = dept.Dept_Id,
                                     Dept_Code = dept.Dept_Code,
                                     Dept_Name = dept.Dept_Name,
                                     isActive = dept.isActive,
                                     Notes = dept.Notes,
                                     Company_Id = comp.Company_Id,
                                     Company_Name = comp.Company_Name
                                 }
                                ).ToList();
                    return await Task.FromResult(vList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DepartmentView>> Get(int id)
        {
            try
            {
                var vList = (from dept in adbContext.department
                             join comp in adbContext.company on dept.Company_Id equals comp.Company_Id
                             where dept.Dept_Id == id && dept.isActive == 1 && comp.isActive == 1
                             select new DepartmentView
                             {
                                 Dept_Id = dept.Dept_Id,
                                 Dept_Code = dept.Dept_Code,
                                 Dept_Name = dept.Dept_Name,
                                 isActive = dept.isActive,
                                 Notes = dept.Notes,
                                 Company_Id = comp.Company_Id,
                                 Company_Name = comp.Company_Name
                             }
                            ).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(DepartmentView entity)
        {
            try
            {
                //Insert New Department
                var vList = new Department
                {
                    Dept_Code = entity.Dept_Code.Trim(),
                    Dept_Name = entity.Dept_Name.Trim(),
                    Company_Id = entity.Company_Id,
                    Notes = entity.Notes,
                    isActive = entity.isActive,
                    AddedOn = DateTime.Now
                };
                adbContext.department.Add(vList);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(DepartmentView entity)
        {
            try
            {
                //Update Old Department
                var vList = adbContext.department.Where(w => w.Dept_Id == entity.Dept_Id).FirstOrDefault();
                if (vList != null)
                {
                    vList.Dept_Code = entity.Dept_Code.Trim();
                    vList.Dept_Name = entity.Dept_Name.Trim();
                    vList.Company_Id = entity.Company_Id;
                    vList.Notes = entity.Notes;
                    vList.isActive = entity.isActive;
                    vList.UpdatedBy = entity.UpdatedBy;
                    vList.UpdatedOn = DateTime.Now;

                    adbContext.department.Update(vList);
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

        public async Task ToogleStatus(int id, Int16 isActive)
        {
            try
            {
                //update flag isActive
                var vList = adbContext.department.Where(w => w.Dept_Id == id && w.isActive != isActive).SingleOrDefault();
                if (vList != null)
                {
                    vList.isActive = isActive;

                    adbContext.department.Update(vList);
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

        public async Task Delete(int id)
        {
            try
            {
                //Delete Department
                var vList = adbContext.department.Where(w => w.Dept_Id == id).ToList().SingleOrDefault();
                if (vList != null)
                {
                    adbContext.department.Remove(vList);
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

        public bool Exists(DepartmentView entity)
        {
            int intCount = 0;
            if (entity.Dept_Id > 0) //Update Validation
                intCount = adbContext.department.Where(w => w.Company_Id == entity.Company_Id && w.Dept_Id != entity.Dept_Id && (w.Dept_Code == entity.Dept_Code || w.Dept_Name == entity.Dept_Name)).Count();
            else //Insert Validation
                intCount = adbContext.department.Where(w => w.Company_Id == entity.Company_Id && (w.Dept_Code == entity.Dept_Code || w.Dept_Name == entity.Dept_Name)).Count();
            return (intCount > 0 ? true : false);
        }

        public async Task<IEnumerable<DepartmentView>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    //Find Department with Paging
                    var vList = (from dept in adbContext.department
                                 join comp in adbContext.company on dept.Company_Id equals comp.Company_Id
                                 where dept.isActive == 1 && comp.isActive == 1
                                 select new DepartmentView
                                 {
                                     Dept_Id = dept.Dept_Id,
                                     Dept_Code = dept.Dept_Code,
                                     Dept_Name = dept.Dept_Name,
                                     isActive = dept.isActive,
                                     Notes = dept.Notes,
                                     Company_Id = comp.Company_Id,
                                     Company_Name = comp.Company_Name
                                 }
                                ).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                    return await Task.FromResult(vList);
                }
                else
                {
                    //Find Department with Paging & Searching
                    var vList = (from dept in adbContext.department.Where(w => new[] { w.Dept_Name.ToLower(), w.Dept_Code.ToLower() }.Any(a => a.Contains(searchValue.ToLower())))
                                 join comp in adbContext.company on dept.Company_Id equals comp.Company_Id
                                 where dept.isActive == 1 && comp.isActive == 1
                                 select new DepartmentView
                                 {
                                     Dept_Id = dept.Dept_Id,
                                     Dept_Code = dept.Dept_Code,
                                     Dept_Name = dept.Dept_Name,
                                     isActive = dept.isActive,
                                     Notes = dept.Notes,
                                     Company_Id = comp.Company_Id,
                                     Company_Name = comp.Company_Name
                                 }
                                ).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                    return await Task.FromResult(vList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<DepartmentView> GetByCompanyId(int id)
        {
            try
            {
                var vList = (from dept in adbContext.department
                             join comp in adbContext.company on dept.Company_Id equals comp.Company_Id
                             where comp.Company_Id == id && dept.isActive == 1 && comp.isActive == 1
                             select new DepartmentView
                             {
                                 Dept_Id = dept.Dept_Id,
                                 Dept_Code = dept.Dept_Code,
                                 Dept_Name = dept.Dept_Name,
                                 isActive = dept.isActive,
                                 Notes = dept.Notes,
                                 Company_Id = comp.Company_Id,
                                 Company_Name = comp.Company_Name
                             }
                            ).ToList();
                return vList;
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
                    //Find Department all no of rows
                    var vCount = (from dept in adbContext.department
                                  join comp in adbContext.company on dept.Company_Id equals comp.Company_Id
                                  where dept.isActive == 1 && comp.isActive == 1
                                  select dept.Dept_Id
                                ).Count();
                    return vCount;
                }
                else
                {
                    //Find Department no of rows with Searching
                    var vCount = (from dept in adbContext.department.Where(w => new[] { w.Dept_Name.ToLower(), w.Dept_Code.ToLower() }.Any(a => a.Contains(searchValue.ToLower())))
                                  join comp in adbContext.company on dept.Company_Id equals comp.Company_Id
                                  where dept.isActive == 1 && comp.isActive == 1
                                  select dept.Dept_Id
                                ).Count();
                    return vCount;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DepartmentView>> GetBy(SearchBy searchBy)
        {
            try
            {
                var vList = (from dept in adbContext.department.Where(String.Format("{0}=={1}", searchBy.FieldName, searchBy.FieldValue))
                             join comp in adbContext.company on dept.Company_Id equals comp.Company_Id
                             where dept.isActive == 1 && comp.isActive == 1
                             select new DepartmentView
                             {
                                 Dept_Id = dept.Dept_Id,
                                 Dept_Code = dept.Dept_Code,
                                 Dept_Name = dept.Dept_Name,
                                 Company_Id = comp.Company_Id
                             }
                            ).Take(searchBy.RecordLimit).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}