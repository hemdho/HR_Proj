using HR.WebApi.DAL;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Repositories
{
    public class ModuleRepository<T> : ICommonRepository<Module>
    {
        private readonly ApplicationDbContext adbContext;

        public ModuleRepository()
        {
            adbContext = Startup.applicationDbContext;
        }

        public async Task<IEnumerable<Module>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                    return await Task.FromResult(adbContext.module.AsEnumerable().Take(RecordLimit).ToList());
                else
                    return await Task.FromResult(adbContext.module.AsEnumerable().ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Module>> Get(int id)
        {
            try
            {
                return await Task.FromResult(adbContext.module.AsEnumerable().Where(w => w.Id == id).ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Module>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    //Find with Paging
                    var vList = adbContext.module.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                    if (vList.Count() > 0)
                        return await Task.FromResult(vList);
                    else
                        throw new Exception("Data Not Available");
                }
                else
                {
                    //Find with Paging & Searching
                    var vList = adbContext.module.Where(w => new[] { w.Name.ToLower(), w.Description.ToLower(), w.DisplayName.ToLower(), w.Url.ToLower() }.Any(a => a.Contains(searchValue.ToLower()))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
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

        public async Task Insert(Module entity)
        {
            try
            {
                entity.AddedOn = DateTime.Now;
                adbContext.module.Add(entity);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Update(Module entity)
        {
            var vList = adbContext.module.Where(x => x.Id == entity.Id).FirstOrDefault();
            if (vList != null)
            {
                try
                {
                    vList.Id = entity.Id;
                    vList.Name = entity.Name;
                    vList.Description = entity.Description;
                    vList.DisplayName = entity.DisplayName;
                    vList.Url = entity.Url;
                    vList.isActive = entity.isActive;

                    adbContext.module.Update(vList);

                    await Task.FromResult(adbContext.SaveChanges());
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public async Task ToogleStatus(int id, short isActive)
        {
            try
            {
                var vList = adbContext.module.AsEnumerable().Where(w => w.Id == id && w.isActive != isActive).ToList().SingleOrDefault();
                vList.isActive = isActive;
                adbContext.module.Update(vList);
                await Task.FromResult(adbContext.SaveChanges());
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
                var vList = adbContext.module.AsEnumerable().Where(w => w.Id == id).ToList().SingleOrDefault();
                adbContext.module.Remove(vList);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(Module entity)
        {
            try
            {
                int intCount = 0;
                if (entity.Id > 0)
                    intCount = adbContext.module.AsEnumerable().Where(w => w.Id != entity.Id && (w.Name == entity.Name && w.DisplayName == entity.DisplayName && w.Url == entity.Url)).Count();
                else
                    intCount = adbContext.module.AsEnumerable().Where(w => w.Id != entity.Id && (w.Name == entity.Name && w.DisplayName == entity.DisplayName && w.Url == entity.Url)).Count();
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
                    //Find Module all no of rows
                    var vCount = adbContext.module.Count();
                    return vCount;
                }
                else
                {
                    //Find Module no of rows with Searching
                    var vCount = adbContext.module.Where(w => new[] { w.Name.ToLower(), w.DisplayName.ToLower(), w.Description.ToLower(), w.Url.ToLower() }.Any(a => a.Contains(searchValue.ToLower()))).Count();
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
