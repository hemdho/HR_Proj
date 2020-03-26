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
    public class RolesRepository<T> : ICommonRepository<Roles>
    {
        private readonly ApplicationDbContext adbContext;

        public RolesRepository()
        {
            adbContext = Startup.applicationDbContext;
        }

        public async Task<IEnumerable<Roles>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    var vList = adbContext.roles.AsEnumerable<Roles>().Take(RecordLimit).ToList();
                    return await Task.FromResult(vList);
                }
                else
                {
                    var vList = adbContext.roles.AsEnumerable<Roles>().ToList();
                    return await Task.FromResult(vList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Roles>> Get(int id)
        {
            try
            {
                var vList = adbContext.roles.AsEnumerable<Roles>().Where(w => w.Id == id).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Roles>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    //Find Roles with Paging
                    var vList = adbContext.roles.AsEnumerable<Roles>().Skip(pageIndex * pageSize).Take(pageSize).ToList();
                    if (vList != null)
                        return await Task.FromResult(vList);
                    else
                        throw new Exception("Data Not Available");
                }
                else
                {
                    //Find Roles with Paging & Searching
                    var vList = adbContext.roles.AsEnumerable<Roles>().Where(w => new[] { w.Name.ToLower() }.Any(a => a.Contains(searchValue.ToLower()))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
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

        public async Task Insert(Roles entity)
        {
            try
            {
                entity.AddedOn = DateTime.Now;
                adbContext.roles.Add(entity);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Roles entity)
        {
            try
            {
                try
                {
                    var lstRoles = adbContext.roles.Where(x => x.Id == entity.Id).FirstOrDefault();
                    if (lstRoles != null)
                    {
                        lstRoles.Name = entity.Name;

                        lstRoles.isActive = entity.isActive;
                        lstRoles.UpdatedBy = entity.UpdatedBy;
                        lstRoles.UpdatedOn = DateTime.Now;

                        adbContext.roles.Update(lstRoles);
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
                var vList = adbContext.roles.AsEnumerable<Roles>().Where(w => w.Id == id && w.isActive != isActive).ToList().SingleOrDefault();
                if (vList != null)
                {
                    vList.isActive = isActive;
                    adbContext.roles.Update(vList);
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
                //Delete Roles
                var vList = adbContext.roles.AsEnumerable<Roles>().Where(w => w.Id == id).ToList().SingleOrDefault();
                if (vList != null)
                {
                    adbContext.roles.Remove(vList);
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

        public bool Exists(Roles entity)
        {
            try
            {
                int intCount = 0;
                if (entity.Id > 0)
                    intCount = adbContext.roles.AsEnumerable<Roles>().Where(w => w.Id != entity.Id && (w.Name == entity.Name)).Count();
                else
                    intCount = adbContext.roles.AsEnumerable<Roles>().Where(w => w.Name == entity.Name).Count();
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
                    //Find Roles all no of rows
                    var vCount = adbContext.roles.AsEnumerable<Roles>().Count();
                    return vCount;
                }
                else
                {
                    //Find Roles no of rows with Searching
                    var vCount = adbContext.roles.AsEnumerable<Roles>().Where(w => new[] { w.Name.ToLower() }.Any(a => a.Contains(searchValue.ToLower()))).Count();
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
