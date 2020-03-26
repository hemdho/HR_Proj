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
    public class SiteRepository<T> : ICommonRepository<SiteView>, ICommonQuery<SiteView>
    {
        private readonly ApplicationDbContext adbContext;

        public SiteRepository()
        {
            adbContext = Startup.applicationDbContext;
        }

        public async Task<IEnumerable<SiteView>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    var vList = (from s in adbContext.site
                                 join c in adbContext.company on s.Company_Id equals c.Company_Id
                                 where s.isActive == 1 && c.isActive == 1
                                 select new SiteView
                                 {
                                     Site_Id = s.Site_Id,
                                     Company_Id = s.Company_Id,
                                     Company_Name = c.Company_Name,
                                     Site_Code = s.Site_Code,
                                     Site_Name =s.Site_Name,
                                     Address1 = s.Address1,
                                     Address2 = s.Address2,
                                     Address3 = s.Address3,
                                     Address4 = s.Address4,
                                     PostCode = s.PostCode,
                                     City =s.City,
                                     State = s.State,
                                     Country = s.Country,
                                     isActive = s.isActive
                                    
                                 }
                                ).Take(RecordLimit).ToList();

                    return await Task.FromResult(vList);
                }
                else
                {
                    var vList = (from s in adbContext.site
                                 join c in adbContext.company on s.Company_Id equals c.Company_Id
                                 where s.isActive == 1 && c.isActive == 1
                                 select new SiteView
                                 {
                                     Site_Id = s.Site_Id,
                                     Company_Id = s.Company_Id,
                                     Company_Name = c.Company_Name,
                                     Site_Code = s.Site_Code,
                                     Site_Name = s.Site_Name,

                                     Address1 = s.Address1,
                                     Address2 = s.Address2,
                                     Address3 = s.Address3,
                                     Address4 = s.Address4,
                                     PostCode = s.PostCode,
                                     City = s.City,
                                     State = s.State,
                                     Country = s.Country,
                                     isActive = s.isActive
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

        public async Task<IEnumerable<SiteView>> Get(int id)
        {
            try
            {
                var vList = (from s in adbContext.site
                             join c in adbContext.company on s.Company_Id equals c.Company_Id
                             where s.Site_Id == id && s.isActive == 1 && c.isActive == 1
                             select new SiteView
                             {
                                 Site_Id = s.Site_Id,
                                 Company_Id = s.Company_Id,
                                 Company_Name = c.Company_Name,
                                 Site_Code = s.Site_Code,
                                 Site_Name = s.Site_Name,

                                 Address1 = s.Address1,
                                 Address2 = s.Address2,
                                 Address3 = s.Address3,
                                 Address4 = s.Address4,
                                 PostCode = s.PostCode,
                                 City = s.City,
                                 State = s.State,
                                 Country = s.Country,
                                 isActive = s.isActive
                             }
                            ).ToList();
                if(vList.Count > 0)
                return await Task.FromResult(vList);
                else
                throw new Exception("Data Not Available");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<SiteView>> GetBy(SearchBy searchBy)
        {
            try
            {
                var vList = (from s in adbContext.site.Where(String.Format("{0}=={1}", searchBy.FieldName, searchBy.FieldValue))
                             join comp in adbContext.company on s.Company_Id equals comp.Company_Id
                             where s.isActive == 1 && comp.isActive == 1
                             select new SiteView
                             {
                                 Site_Id = s.Site_Id,
                                 Company_Id = s.Company_Id,
                                 Company_Name = comp.Company_Name,
                                 Site_Code = s.Site_Code,
                                 Site_Name = s.Site_Name,
                                 Address1 = s.Address1,
                                 Address2 = s.Address2,
                                 Address3 = s.Address3,
                                 Address4 = s.Address4,
                                 PostCode = s.PostCode,
                                 City = s.City,
                                 State = s.State,
                                 Country = s.Country
                             }
                            ).Take(searchBy.RecordLimit).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<SiteView>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    //Find Department with Paging
                    var vList = (from s in adbContext.site
                                 join c in adbContext.company on s.Company_Id equals c.Company_Id
                                 select new SiteView
                                 {
                                     Site_Id = s.Site_Id,
                                     Company_Id = s.Company_Id,
                                     Company_Name = c.Company_Name,
                                     Site_Code = s.Site_Code,
                                     Site_Name = s.Site_Name,
                                     Address1 = s.Address1,
                                     Address2 = s.Address2,
                                     Address3 = s.Address3,
                                     Address4 = s.Address4,
                                     PostCode = s.PostCode,
                                     City = s.City,
                                     State = s.State,
                                     Country = s.Country,
                                     isActive = s.isActive
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
                    var vList = (from s in adbContext.site.Where(w => new[] { w.Site_Name.ToLower(), w.Site_Code.ToLower() }.Any(a => a.Contains(searchValue.ToLower())))
                                 join c in adbContext.company on s.Company_Id equals c.Company_Id
                                 select new SiteView
                                 {
                                     Site_Id = s.Site_Id,
                                     Company_Id = s.Company_Id,
                                     Company_Name = c.Company_Name,
                                     Site_Code = s.Site_Code,
                                     Site_Name = s.Site_Name,
                                     Address1 = s.Address1,
                                     Address2 = s.Address2,
                                     Address3 = s.Address3,
                                     Address4 = s.Address4,
                                     PostCode = s.PostCode,
                                     City = s.City,
                                     State = s.State,
                                     Country = s.Country,
                                     isActive = s.isActive
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

        public async Task Insert(SiteView entity)
        {
            try
            {
                var vList = new Site
                {
                    Company_Id = entity.Company_Id,
                    Site_Code = entity.Site_Code,
                    Site_Name = entity.Site_Name,
                    Address1 = entity.Address1,
                    Address2 = entity.Address2,
                    Address3 = entity.Address3,
                    Address4 = entity.Address4,
                    PostCode = entity.PostCode,
                    City = entity.City,
                    State = entity.State,
                    Country = entity.Country,
                    isActive = entity.isActive,
                    AddedBy = entity.AddedBy,
                    AddedOn = DateTime.Now
                };
                adbContext.site.Add(vList);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(SiteView entity)
        {
            try
            {
                var vList = adbContext.site.Where(x => x.Site_Id == entity.Site_Id).FirstOrDefault();

                if (vList != null)
                {
                    vList.Company_Id = entity.Company_Id;
                    vList.Site_Code = entity.Site_Code.Trim();
                    vList.Site_Name = entity.Site_Name.Trim();
                    vList.Address1 = entity.Address1;
                    vList.Address2 = entity.Address2;
                    vList.Address3 = entity.Address3;
                    vList.Address4 = entity.Address4;
                    vList.PostCode = entity.PostCode;
                    vList.City = entity.City;
                    vList.State = entity.State;
                    vList.Country = entity.Country;
                    vList.isActive = entity.isActive;
                    vList.UpdatedBy = entity.UpdatedBy;
                    vList.UpdatedOn = DateTime.Now;

                    adbContext.site.Update(vList);

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

        public async Task ToogleStatus(int id, short isActive)
        {
            try
            {
                Site lstSite = adbContext.site.Where(w => w.Site_Id == id && w.isActive != isActive).ToList().SingleOrDefault();
                if (lstSite != null)
                {
                    lstSite.isActive = isActive;

                    adbContext.site.Update(lstSite);
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
                var vList = adbContext.site.Where(w => w.Site_Id == id).ToList().SingleOrDefault();
                if (vList != null)
                {
                    adbContext.site.Remove(vList);
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
                    //Find Site all no of rows
                    var vCount = (from s in adbContext.site
                                  join c in adbContext.company on s.Company_Id equals c.Company_Id
                                  select s.Site_Id                                  
                                 ).Count();
                    return vCount;
                }
                else
                {
                    //Find Site no of rows with Searching
                    var vCount = (from s in adbContext.site.Where(w => new[] {w.Site_Name.ToLower(), w.Site_Code.ToLower()}.Any(a => a.Contains(searchValue.ToLower())))
                                  join c in adbContext.company on s.Company_Id equals c.Company_Id
                                  select s.Site_Id
                                 ).Count();
                    return vCount;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(SiteView entity)
        {
            int intCount = 0;
            if (entity.Site_Id > 0) //Update Validation
            intCount = adbContext.site.Where(w => w.Company_Id == entity.Company_Id && w.Site_Id != entity.Site_Id && (w.Site_Code == entity.Site_Code || w.Site_Name == entity.Site_Name)).Count();
            else //Insert Validation
                intCount = adbContext.site.Where(w => w.Company_Id == entity.Company_Id && (w.Site_Code == entity.Site_Code || w.Site_Name == entity.Site_Name)).Count();
            return (intCount > 0 ? true : false);
        }
    }
}
