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
    public class DesignationRepository<T> : ICommonRepository<DesignationView>, ICommonQuery<DesignationView>
    {
        private readonly ApplicationDbContext adbContext;

        public DesignationRepository()
        {
            adbContext = Startup.applicationDbContext;
        }

        public async Task<IEnumerable<DesignationView>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    var vList = (from desig in adbContext.designation
                                 join dept in adbContext.department on desig.Dept_Id equals dept.Dept_Id
                                 join comp in adbContext.company on desig.Company_Id equals comp.Company_Id
                                 join zone in adbContext.zone on desig.Zone_Id equals zone.Zone_Id
                                 where desig.isActive == 1 && dept.isActive == 1 && comp.isActive == 1 && zone.isActive == 1
                                 select new DesignationView
                                 {
                                     Desig_Id = desig.Desig_Id,
                                     Company_Id = desig.Company_Id,
                                     Dept_Id = desig.Dept_Id,
                                     Zone_Id = desig.Zone_Id,
                                     Desig_Code = desig.Desig_Code,
                                     Desig_Name = desig.Desig_Name,
                                     Report_To = desig.Report_To,
                                     AdditionalReport_To = desig.AdditionalReport_To,
                                     Level = desig.Level,
                                     isActive = desig.isActive,
                                     Company_Name = comp.Company_Name,
                                     Dept_Name = dept.Dept_Name,
                                     Zone_Name = zone.Zone_Name
                                 }
                                ).Take(RecordLimit).ToList();
                    return await Task.FromResult(vList);
                }
                else
                {
                    var vList = (from desig in adbContext.designation
                                 join dept in adbContext.department on desig.Dept_Id equals dept.Dept_Id
                                 join comp in adbContext.company on desig.Company_Id equals comp.Company_Id
                                 join zone in adbContext.zone on desig.Zone_Id equals zone.Zone_Id
                                 where desig.isActive == 1 && dept.isActive == 1 && comp.isActive == 1 && zone.isActive == 1
                                 select new DesignationView
                                 {
                                     Desig_Id = desig.Desig_Id,
                                     Company_Id = desig.Company_Id,
                                     Dept_Id = desig.Dept_Id,
                                     Zone_Id = desig.Zone_Id,
                                     Desig_Code = desig.Desig_Code,
                                     Desig_Name = desig.Desig_Name,
                                     Report_To = desig.Report_To,
                                     AdditionalReport_To = desig.AdditionalReport_To,
                                     Level = desig.Level,
                                     isActive = desig.isActive,
                                     Company_Name = comp.Company_Name,
                                     Dept_Name = dept.Dept_Name,
                                     Zone_Name = zone.Zone_Name
                                 }).ToList();
                    return await Task.FromResult(vList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DesignationView>> Get(int id)
        {
            try
            {
                var vList = (from desig in adbContext.designation
                             join dept in adbContext.department on desig.Dept_Id equals dept.Dept_Id
                             join comp in adbContext.company on desig.Company_Id equals comp.Company_Id
                             join zone in adbContext.zone on desig.Zone_Id equals zone.Zone_Id
                             where desig.Desig_Id == id && desig.isActive == 1 && dept.isActive == 1 && comp.isActive == 1 && zone.isActive == 1
                             select new DesignationView
                             {
                                 Desig_Id = desig.Desig_Id,
                                 Company_Id = desig.Company_Id,
                                 Dept_Id = desig.Dept_Id,
                                 Zone_Id = desig.Zone_Id,
                                 Desig_Code = desig.Desig_Code,
                                 Desig_Name = desig.Desig_Name,
                                 Report_To = desig.Report_To,
                                 AdditionalReport_To = desig.AdditionalReport_To,
                                 Level = desig.Level,
                                 isActive = desig.isActive,
                                 Company_Name = comp.Company_Name,
                                 Dept_Name = dept.Dept_Name,
                                 Zone_Name = zone.Zone_Name
                             }).ToList();
                return await Task.FromResult(vList);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DesignationView>> GetBy(SearchBy searchBy)
        {
            try
            {
                var vList = (from desig in adbContext.designation.Where(String.Format("{0}=={1}", searchBy.FieldName, searchBy.FieldValue))
                             join dept in adbContext.department on desig.Dept_Id equals dept.Dept_Id
                             join comp in adbContext.company on desig.Company_Id equals comp.Company_Id
                             join zone in adbContext.zone on desig.Zone_Id equals zone.Zone_Id
                             select new DesignationView
                             {
                                 Desig_Id = desig.Desig_Id,
                                 Desig_Code = desig.Desig_Code,
                                 Desig_Name = desig.Desig_Name,
                                 Report_To = desig.Report_To,
                                 AdditionalReport_To = desig.AdditionalReport_To,
                                 Company_Name = comp.Company_Name,
                                 Dept_Name = dept.Dept_Name,
                                 Zone_Name = zone.Zone_Name
                             }).Take(searchBy.RecordLimit).ToList();

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DesignationView>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    //Find Designation with Paging
                    var vList = (from desig in adbContext.designation
                                 join dept in adbContext.department on desig.Dept_Id equals dept.Dept_Id
                                 join comp in adbContext.company on desig.Company_Id equals comp.Company_Id
                                 join zone in adbContext.zone on desig.Zone_Id equals zone.Zone_Id
                                 where desig.isActive == 1 && dept.isActive == 1 && comp.isActive == 1 && zone.isActive == 1
                                 select new DesignationView
                                 {
                                     Desig_Id = desig.Desig_Id,
                                     Company_Id = desig.Company_Id,
                                     Dept_Id = desig.Dept_Id,
                                     Zone_Id = desig.Zone_Id,
                                     Desig_Code = desig.Desig_Code,
                                     Desig_Name = desig.Desig_Name,
                                     Report_To = desig.Report_To,
                                     AdditionalReport_To = desig.AdditionalReport_To,
                                     Level = desig.Level,
                                     isActive = desig.isActive,
                                     Company_Name = comp.Company_Name,
                                     Dept_Name = dept.Dept_Name,
                                     Zone_Name = zone.Zone_Name
                                 }
                                 ).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                    if (vList.Count() > 0)
                        return await Task.FromResult(vList);
                    else
                        throw new Exception("Data Not Available");
                }
                else
                {
                    //Find Designation with Paging & Searching
                    var vList = (from desig in adbContext.designation.Where(w => new[] { w.Desig_Name.ToLower(), w.Desig_Code.ToLower() }.Any(a => a.Contains(searchValue.ToLower())))
                                 join dept in adbContext.department on desig.Dept_Id equals dept.Dept_Id
                                 join comp in adbContext.company on desig.Company_Id equals comp.Company_Id
                                 join zone in adbContext.zone on desig.Zone_Id equals zone.Zone_Id
                                 where desig.isActive == 1 && dept.isActive == 1 && comp.isActive == 1 && zone.isActive == 1
                                 select new DesignationView
                                 {
                                     Desig_Id = desig.Desig_Id,
                                     Company_Id = desig.Company_Id,
                                     Dept_Id = desig.Dept_Id,
                                     Zone_Id = desig.Zone_Id,
                                     Desig_Code = desig.Desig_Code,
                                     Desig_Name = desig.Desig_Name,
                                     Report_To = desig.Report_To,
                                     AdditionalReport_To = desig.AdditionalReport_To,
                                     Level = desig.Level,
                                     isActive = desig.isActive,
                                     Company_Name = comp.Company_Name,
                                     Dept_Name = dept.Dept_Name,
                                     Zone_Name = zone.Zone_Name
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

        public async Task Insert(DesignationView entity)
        {
            try
            {
                //Insert New Designation
                var vList = new Designation
                {
                    Company_Id = entity.Company_Id,
                    Dept_Id = entity.Dept_Id,
                    Zone_Id = entity.Zone_Id,
                    Desig_Code = entity.Desig_Code.Trim(),
                    Desig_Name = entity.Desig_Name.Trim(),
                    Report_To = entity.Report_To,
                    AdditionalReport_To = entity.AdditionalReport_To,
                    Level = entity.Level,

                    isActive = entity.isActive,
                    AddedBy = entity.AddedBy,
                    AddedOn = DateTime.Now
                };

                adbContext.designation.Add(vList);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(DesignationView entity)
        {
            try
            {
                var lstDesig = adbContext.designation.Where(x => x.Desig_Id == entity.Desig_Id).FirstOrDefault();
                if (lstDesig != null)
                {
                    lstDesig.Company_Id = entity.Company_Id;
                    lstDesig.Dept_Id = entity.Dept_Id;
                    lstDesig.Zone_Id = entity.Zone_Id;
                    lstDesig.Desig_Code = entity.Desig_Code;
                    lstDesig.Desig_Name = entity.Desig_Name;
                    lstDesig.Report_To = entity.Report_To;
                    lstDesig.AdditionalReport_To = entity.AdditionalReport_To;
                    lstDesig.Level = entity.Level;

                    lstDesig.isActive = entity.isActive;
                    lstDesig.UpdatedBy = entity.UpdatedBy;
                    lstDesig.UpdatedOn = DateTime.Now;

                    adbContext.designation.Update(lstDesig);
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
                var vList = adbContext.designation.Where(w => w.Desig_Id == id && w.isActive != isActive).SingleOrDefault();
                if (vList != null)
                {
                    vList.isActive = isActive;
                    adbContext.designation.Update(vList);
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
                //Delete Designation
                var vList = adbContext.designation.Where(w => w.Desig_Id == id).SingleOrDefault();
                if (vList != null)
                {
                    adbContext.designation.Remove(vList);
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

        public bool Exists(DesignationView entity)
        {
            try
            {
                int intCount = 0;
                if (entity.Desig_Id > 0) //Update Validation
                    intCount = adbContext.designation.Where(w => w.Desig_Id != entity.Desig_Id && (w.Desig_Code == entity.Desig_Code || w.Desig_Name == entity.Desig_Name)).Count();
                else //Insert Validation
                    intCount = adbContext.designation.Where(w => w.Desig_Code == entity.Desig_Code || w.Desig_Name == entity.Desig_Name).Count();
                return (intCount > 0 ? true : false);
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
                    //Find Designation all no of rows
                    var vCount = (from desig in adbContext.designation
                                  join dept in adbContext.department on desig.Dept_Id equals dept.Dept_Id
                                  join comp in adbContext.company on desig.Company_Id equals comp.Company_Id
                                  join zone in adbContext.zone on desig.Zone_Id equals zone.Zone_Id
                                  where desig.isActive == 1 && dept.isActive == 1 && comp.isActive == 1 && zone.isActive == 1
                                  select desig.Desig_Id
                                ).Count();
                    return vCount;
                }
                else
                {
                    //Find Designation no of rows with Searching
                    var vCount = (from desig in adbContext.designation.Where(w => new[] { w.Desig_Name.ToLower(), w.Desig_Code.ToLower() }.Any(a => a.Contains(searchValue.ToLower())))
                                  join dept in adbContext.department on desig.Dept_Id equals dept.Dept_Id
                                  join comp in adbContext.company on desig.Company_Id equals comp.Company_Id
                                  join zone in adbContext.zone on desig.Zone_Id equals zone.Zone_Id
                                  where desig.isActive == 1 && dept.isActive == 1 && comp.isActive == 1 && zone.isActive == 1
                                  select desig.Desig_Id
                                ).Count();
                    return vCount;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
