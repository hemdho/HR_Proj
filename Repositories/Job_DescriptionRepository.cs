using HR.WebApi.DAL;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using HR.WebApi.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HR.WebApi.Repositories
{
    public class Job_DescriptionRepository<T> : ICommonRepository<Job_DiscriptionView>, ICommonQuery<Job_DiscriptionView>
    {
        private readonly ApplicationDbContext adbContext;

        public Job_DescriptionRepository()
        {
            adbContext = Startup.applicationDbContext;
        }

        public async Task<IEnumerable<Job_DiscriptionView>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    var vList = (from jd in adbContext.job_description
                                 join comp in adbContext.company on jd.Company_Id equals comp.Company_Id
                                 join dep in adbContext.department on jd.Dept_Id equals dep.Dept_Id
                                 join desig in adbContext.designation on jd.Desig_Id equals desig.Desig_Id
                                 where jd.isActive == 1 && comp.isActive == 1
                                 select new Job_DiscriptionView
                                 {
                                     JD_Id = jd.JD_Id,
                                     Company_Id = jd.Company_Id,
                                     Dept_Id = jd.Dept_Id,
                                     Desig_Id = jd.Desig_Id,
                                     JD_Code = jd.JD_Code,
                                     JD_Name = jd.JD_Name,
                                     JD_Description = jd.JD_Description,

                                     Notes = jd.Notes,
                                     isActive = jd.isActive,

                                     Company_Name = comp.Company_Name,
                                     Dept_Name = dep.Dept_Name,
                                     Desig_Name = desig.Desig_Name
                                 }
                                ).Take(RecordLimit).ToList();

                    return await Task.FromResult(vList);
                }
                else
                {
                    var vList = (from jd in adbContext.job_description
                                 join comp in adbContext.company on jd.Company_Id equals comp.Company_Id
                                 join dep in adbContext.department on jd.Dept_Id equals dep.Dept_Id
                                 join desig in adbContext.designation on jd.Desig_Id equals desig.Desig_Id
                                 where jd.isActive == 1 && comp.isActive == 1
                                 select new Job_DiscriptionView
                                 {
                                     JD_Id = jd.JD_Id,
                                     Company_Id = jd.Company_Id,
                                     Dept_Id = jd.Dept_Id,
                                     Desig_Id = jd.Desig_Id,
                                     JD_Code = jd.JD_Code,
                                     JD_Name = jd.JD_Name,
                                     JD_Description = jd.JD_Description,

                                     Notes = jd.Notes,
                                     isActive = jd.isActive,

                                     Company_Name = comp.Company_Name,
                                     Dept_Name = dep.Dept_Name,
                                     Desig_Name = desig.Desig_Name
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

        public async Task<IEnumerable<Job_DiscriptionView>> Get(int id)
        {
            try
            {

                var vList = (from jd in adbContext.job_description
                             join comp in adbContext.company on jd.Company_Id equals comp.Company_Id
                             join dep in adbContext.department on jd.Dept_Id equals dep.Dept_Id
                             join desig in adbContext.designation on jd.Desig_Id equals desig.Desig_Id
                             where jd.JD_Id == id && jd.isActive == 1 && comp.isActive == 1
                             select new Job_DiscriptionView
                             {
                                 JD_Id = jd.JD_Id,
                                 Company_Id = jd.Company_Id,
                                 Dept_Id = jd.Dept_Id,
                                 Desig_Id = jd.Desig_Id,
                                 JD_Code = jd.JD_Code,
                                 JD_Name = jd.JD_Name,
                                 JD_Description = jd.JD_Description,

                                 Notes = jd.Notes,
                                 isActive = jd.isActive,

                                 Company_Name = comp.Company_Name,
                                 Dept_Name = dep.Dept_Name,
                                 Desig_Name = desig.Desig_Name
                             }
                            ).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Job_DiscriptionView>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    //Find Department with Paging
                    var vList = (from jd in adbContext.job_description
                                 join comp in adbContext.company on jd.Company_Id equals comp.Company_Id
                                 join dep in adbContext.department on jd.Dept_Id equals dep.Dept_Id
                                 join desig in adbContext.designation on jd.Desig_Id equals desig.Desig_Id
                                 select new Job_DiscriptionView
                                 {
                                     JD_Id = jd.JD_Id,
                                     JD_Code = jd.JD_Code,
                                     JD_Name = jd.JD_Name,
                                     JD_Description = jd.JD_Description,

                                     isActive = jd.isActive,
                                     Notes = jd.Notes,

                                     Company_Name = comp.Company_Name,
                                     Dept_Name = dep.Dept_Name,
                                     Desig_Name = desig.Desig_Name
                                 }
                                ).Skip(pageIndex * pageSize).Take(pageSize).ToList();

                    if (vList != null)
                        return await Task.FromResult(vList);
                    else
                        throw new Exception("Data Not Available");
                }
                else
                {
                    //Find Department with Paging & Searching
                    var vList = (from jd in adbContext.job_description.Where(w => new[] { w.JD_Name.ToLower(), w.JD_Code.ToLower(), w.JD_Description.ToLower() }.Any(a => a.Contains(searchValue.ToLower())))
                                 join comp in adbContext.company on jd.Company_Id equals comp.Company_Id
                                 join dep in adbContext.department on jd.Dept_Id equals dep.Dept_Id
                                 join desig in adbContext.designation on jd.Desig_Id equals desig.Desig_Id
                                 select new Job_DiscriptionView
                                 {
                                     JD_Id = jd.JD_Id,
                                     JD_Code = jd.JD_Code,
                                     JD_Name = jd.JD_Name,
                                     JD_Description = jd.JD_Description,

                                     isActive = jd.isActive,
                                     Notes = jd.Notes,

                                     Company_Name = comp.Company_Name,
                                     Dept_Name = dep.Dept_Name,
                                     Desig_Name = desig.Desig_Name
                                 }
                                ).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                    if (vList.Count() > 0)
                        return await Task.FromResult(vList);
                    else
                        throw new Exception("Data Not Available");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Job_DiscriptionView>> GetBy(SearchBy searchBy)
        {
            try
            {
                var vList = (from jd in adbContext.job_description.Where(String.Format("{0}=={1}", searchBy.FieldName, searchBy.FieldValue))
                             join comp in adbContext.company on jd.Company_Id equals comp.Company_Id
                             join dep in adbContext.department on jd.Dept_Id equals dep.Dept_Id
                             join desig in adbContext.designation on jd.Desig_Id equals desig.Desig_Id
                             where jd.isActive == 1 && comp.isActive == 1
                             select new Job_DiscriptionView
                             {
                                 JD_Id = jd.JD_Id,
                                 
                                 Company_Id = jd.Company_Id,
                                 Company_Name = comp.Company_Name,

                                 Dept_Id = jd.Dept_Id,
                                 Dept_Name = dep.Dept_Name,

                                 Desig_Id = jd.Desig_Id,
                                 Desig_Name = desig.Desig_Name,

                                 JD_Code = jd.JD_Code,
                                 JD_Name = jd.JD_Name,
                                 JD_Description = jd.JD_Description,
                                 Notes = jd.Notes                                 
                             }
                            ).Take(searchBy.RecordLimit).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(Job_DiscriptionView entity)
        {
            try
            {
                var vList = new Job_Description
                {
                    Company_Id = entity.Company_Id,
                    Dept_Id = entity.Dept_Id,
                    Desig_Id = entity.Desig_Id,
                    JD_Code = entity.JD_Code.Trim(),
                    JD_Name = entity.JD_Name.Trim(),
                    JD_Description = entity.JD_Description,
                    Notes = entity.Notes,

                    isActive = entity.isActive,
                    AddedBy = entity.UpdatedBy,
                    AddedOn = DateTime.Now
                };

                adbContext.job_description.Add(vList);

                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Job_DiscriptionView entity)
        {
            try
            {
                var vList = adbContext.job_description.Where(x => x.JD_Id == entity.JD_Id).FirstOrDefault();
                if (vList != null)
                {
                    vList.Company_Id = entity.Company_Id;
                    vList.Dept_Id = entity.Dept_Id;
                    vList.Desig_Id = entity.Desig_Id;
                    vList.JD_Code = entity.JD_Code.Trim();
                    vList.JD_Name = entity.JD_Name.Trim();
                    vList.JD_Description = entity.JD_Description;
                    vList.Notes = entity.Notes;

                    vList.isActive = entity.isActive;
                    vList.UpdatedBy = entity.UpdatedBy;
                    vList.UpdatedOn = DateTime.Now;

                    adbContext.job_description.Update(vList);
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
                //update flag isActive=0
                var vList = adbContext.job_description.Where(w => w.JD_Id == id && w.isActive != isActive).ToList().SingleOrDefault();
                if (vList != null)
                {
                    vList.isActive = isActive;
                    adbContext.job_description.Update(vList);
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
                //Delete Job_Description
                var vList = adbContext.job_description.Where(w => w.JD_Id == id).ToList().SingleOrDefault();
                if (vList != null)
                {
                    adbContext.job_description.Remove(vList);
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

        public int RecordCount(string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    //Find Job_Description all no of rows
                    var vCount = (from jd in adbContext.job_description
                                  join comp in adbContext.company on jd.Company_Id equals comp.Company_Id
                                  join dep in adbContext.department on jd.Dept_Id equals dep.Dept_Id
                                  join desig in adbContext.designation on jd.Desig_Id equals desig.Desig_Id
                                  select jd.JD_Id
                                ).Count();
                    return vCount;
                }
                else
                {
                    //Find Job_Description no of rows with Searching
                    var vCount = (from jd in adbContext.job_description.Where(w => new[] { w.JD_Name.ToLower(), w.JD_Code.ToLower(), w.JD_Description.ToLower()}.Any(a => a.Contains(searchValue.ToLower())))
                                  join comp in adbContext.company on jd.Company_Id equals comp.Company_Id
                                  join dep in adbContext.department on jd.Dept_Id equals dep.Dept_Id
                                  join desig in adbContext.designation on jd.Desig_Id equals desig.Desig_Id
                                  select jd.JD_Id
                                ).Count();
                    return vCount;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(Job_DiscriptionView entity)
        {
            int intCount = 0;
            if (entity.JD_Id > 0) //Update Validation
                intCount = adbContext.job_description.Where(w => w.Company_Id == entity.Company_Id && w.JD_Id != entity.JD_Id && (w.JD_Code == entity.JD_Code || w.JD_Name == entity.JD_Name)).Count();
            else //Insert Validation
                intCount = adbContext.job_description.Where(w => w.Company_Id == entity.Company_Id && (w.JD_Code == entity.JD_Code || w.JD_Name == entity.JD_Name)).Count();
            return (intCount > 0 ? true : false);
        }
    }
}
