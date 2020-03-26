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
    public class ZoneRepository<T> : ICommonRepository<ZoneView>, ICommonQuery<ZoneView>
    {
        private readonly ApplicationDbContext adbContext;
        public ZoneRepository()
        {
            adbContext = Startup.applicationDbContext;
        }

        public async Task<IEnumerable<ZoneView>> Get(int id)
        {
            try
            {
                var vList = (from z in adbContext.zone
                             join c in adbContext.company on z.Company_Id equals c.Company_Id
                             where z.Zone_Id == id && z.isActive == 1 && c.isActive == 1
                             select new ZoneView
                             {
                                 Zone_Id = z.Zone_Id,
                                 Company_Id = z.Company_Id,
                                 Company_Name = c.Company_Name,
                                 Zone_Code = z.Zone_Code,
                                 Zone_Name = z.Zone_Name,
                             }
                                ).ToList();

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ZoneView>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    var vList = (from z in adbContext.zone
                                 join c in adbContext.company on z.Company_Id equals c.Company_Id
                                 where z.isActive == 1 && c.isActive == 1
                                 select new ZoneView
                                 {
                                     Zone_Id = z.Zone_Id,
                                     Company_Id = z.Company_Id,
                                     Company_Name = c.Company_Name,
                                     Zone_Code = z.Zone_Code,
                                     Zone_Name = z.Zone_Name,
                                     isActive = z.isActive
                                 }
                                ).Take(RecordLimit).ToList();

                    return await Task.FromResult(vList);
                }
                else
                {
                    var vList = (from z in adbContext.zone
                                 join c in adbContext.company on z.Company_Id equals c.Company_Id
                                 where z.isActive == 1 && c.isActive == 1
                                 select new ZoneView
                                 {
                                     Zone_Id = z.Zone_Id,
                                     Company_Id = z.Company_Id,
                                     Company_Name = c.Company_Name,
                                     Zone_Code = z.Zone_Code,
                                     Zone_Name = z.Zone_Name,
                                     isActive = z.isActive
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

        public async Task Insert(ZoneView entity)
        {
            try
            {
                //Insert New Zone
                var vList = new Zone
                {
                    Zone_Id = entity.Zone_Id,
                    Zone_Name = entity.Zone_Name,
                    Zone_Code = entity.Zone_Code,
                    Company_Id = entity.Company_Id,
                    isActive = entity.isActive,
                    AddedBy = entity.AddedBy,
                    AddedOn = DateTime.Now
                };
                adbContext.zone.Add(vList);
                await Task.FromResult(adbContext.SaveChanges());
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
                Zone lstzone = adbContext.zone.Where(w => w.Zone_Id == id && w.isActive != isActive).ToList().SingleOrDefault();
                if (lstzone == null)
                {
                    lstzone.isActive = isActive;

                    adbContext.zone.Update(lstzone);
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

        public async Task Update(ZoneView entity)
        {
            try
            {
                var vList = adbContext.zone.Where(x => x.Zone_Id == entity.Zone_Id).FirstOrDefault();
                if (vList != null)
                {
                    vList.Company_Id = entity.Company_Id;
                    vList.Zone_Code = entity.Zone_Code;
                    vList.Zone_Name = entity.Zone_Name;
                    vList.isActive = entity.isActive;
                    vList.UpdatedBy = entity.UpdatedBy;
                    vList.UpdatedOn = DateTime.Now;

                    adbContext.zone.Update(vList);
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

        public async Task<IEnumerable<ZoneView>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    //Find Zone with Paging
                    var vList = (from z in adbContext.zone
                                 join c in adbContext.company on z.Company_Id equals c.Company_Id
                                 where z.isActive == 1 && c.isActive == 1
                                 select new ZoneView
                                 {
                                     Zone_Id = z.Zone_Id,
                                     Company_Id = z.Company_Id,
                                     Company_Name = c.Company_Name,
                                     Zone_Code = z.Zone_Code,
                                     Zone_Name = z.Zone_Name,
                                     isActive = z.isActive
                                 }
                                ).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                    if (vList != null)
                        return await Task.FromResult(vList);
                    else
                        throw new Exception("Data Not Available");
                }
                else
                {
                    //Find Zone with Paging & Searching
                    var vList = (from z in adbContext.zone.Where(w => new[] { w.Zone_Name.ToLower(), w.Zone_Code.ToLower() }.Any(a => a.Contains(searchValue.ToLower())))
                                 join c in adbContext.company on z.Company_Id equals c.Company_Id
                                 where z.isActive == 1 && c.isActive == 1
                                 select new ZoneView
                                 {
                                     Zone_Id = z.Zone_Id,
                                     Company_Id = z.Company_Id,
                                     Company_Name = c.Company_Name,
                                     Zone_Code = z.Zone_Code,
                                     Zone_Name = z.Zone_Name,
                                     isActive = z.isActive
                                 }
                                ).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                    if (vList != null)
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

        public int RecordCount(string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    //Find Zone all no of rows
                    var vCount = (from z in adbContext.zone
                                  join c in adbContext.company on z.Company_Id equals c.Company_Id
                                  select z.Zone_Id
                                ).Count();
                    return vCount;
                }
                else
                {
                    //Find Zone no of rows with Searching
                    var vCount = (from z in adbContext.zone.Where(w => new[] { w.Zone_Name, w.Zone_Code }.Any(a => a.Contains(searchValue)))
                                  join c in adbContext.company on z.Company_Id equals c.Company_Id
                                  select z.Zone_Id
                                ).Count();
                    return vCount;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(ZoneView entity)
        {
            int intCount = 0;
            if (entity.Zone_Id > 0) //Update Validation
                intCount = adbContext.zone.Where(w => w.Company_Id == entity.Company_Id && w.Zone_Id != entity.Zone_Id && (w.Zone_Code == entity.Zone_Code && w.Zone_Name == entity.Zone_Name)).Count();
            else //Insert Validation
                intCount = adbContext.zone.Where(w => w.Company_Id == entity.Company_Id && (w.Zone_Code == entity.Zone_Code && w.Zone_Name == entity.Zone_Name)).Count();
            return (intCount > 0 ? true : false);
        }

        public async Task Delete(int id)
        {
            try
            {
                var vList = adbContext.zone.Where(w => w.Zone_Id == id).ToList().SingleOrDefault();
                if (vList != null)
                {
                    adbContext.zone.Remove(vList);
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

        public async Task<IEnumerable<ZoneView>> GetBy(SearchBy searchBy)
        {
            try
            {
                var vList = (from z in adbContext.zone.Where(String.Format("{0}=={1}", searchBy.FieldName, searchBy.FieldValue))
                             join comp in adbContext.company on z.Company_Id equals comp.Company_Id
                             where z.isActive == 1 && comp.isActive == 1
                             select new ZoneView
                             {
                                 Zone_Id = z.Zone_Id,
                                 Zone_Code = z.Zone_Code,
                                 Zone_Name = z.Zone_Name
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
