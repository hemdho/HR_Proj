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
    public class User_RoleRepository<T> : ICommonRepository<User_Role>
    {
        private readonly ApplicationDbContext adbContext;

        public User_RoleRepository()
        {
            adbContext = Startup.applicationDbContext;
        }

        public async Task<IEnumerable<User_Role>> GetAll(int RecordLimit)
        {
            try
            {
                IList<User_Role> lstUser_Role = adbContext.user_role.AsEnumerable<User_Role>().Take(1000).ToList();               
                return await Task.FromResult(lstUser_Role);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User_Role> Get(int id)
        {
            try
            {
                User_Role lstUser_Role = adbContext.user_role.AsEnumerable<User_Role>().Where(w => w.Id == id).ToList().SingleOrDefault();                
                return await Task.FromResult(lstUser_Role);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<User_Role>> PaginatedList(int pageIndex, int pageSize)
        {
            try
            {
                //IList<User_Role> lstUser_Role = adbContext.user_role.AsEnumerable<User_Role>().Skip(pageIndex * pageSize).Take(pageSize).ToList();
                IList<User_Role> lstUser_Role = adbContext.user_role.AsEnumerable<User_Role>().Skip(pageIndex * pageSize).Take(pageSize).Take(1000).ToList();               
                return await Task.FromResult(lstUser_Role);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(User_Role entity)
        {
            try
            {
                entity.AddedOn = DateTime.Now;
                adbContext.user_role.Add(entity);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(User_Role entity)
        {
            try
            {
                adbContext.user_role.Update(entity);
                await Task.FromResult(adbContext.SaveChanges());
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
                var vList = adbContext.user_role.AsEnumerable<User_Role>().Where(w => w.Id == id && w.isActive != isActive).ToList().SingleOrDefault();
                if (vList != null)
                {
                    vList.isActive = isActive;
                    adbContext.user_role.Update(vList);
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
                //Delete User_Role
                var vList = adbContext.user_role.AsEnumerable<User_Role>().Where(w => w.Id == id).ToList().SingleOrDefault();
                if (vList != null)
                {
                    adbContext.user_role.Remove(vList);
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

        public bool IsDuplicate(User_Role entity)
        {
            if (entity.Id > 0)
            {
                var vCount = adbContext.user_role.Where(w => w.Id != entity.Id && (w.User_Id == entity.User_Id && w.Role_Id == entity.Role_Id)).Count();
                return (vCount > 0 ? true : false);                
            }
            else
            {
                var vCount = adbContext.user_role.Where(w => w.User_Id == entity.User_Id && w.Role_Id == entity.Role_Id).Count();
                return (vCount > 0 ? true : false);
            }
        }

        public async Task<IList<User_Role>> FindPaginatedList(int pageIndex, int pageSize, Expression<Func<User_Role, bool>> expression)
        {
            try
            {
                var vList = adbContext.user_role.Where(expression).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                if (vList != null)
                {
                    return await Task.FromResult(vList);
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

        public async Task<IList<User_Role>> FindAnyValue(string searchValue)
        {
            try
            {
                var vList = adbContext.user_role.Where(w => new[] { Convert.ToString(w.Id), Convert.ToString(w.User_Id), Convert.ToString(w.Role_Id) }.Any(a => a.Contains(searchValue))).ToList();
                if (vList != null)
                {
                    return await Task.FromResult(vList);
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

       

        Task<IEnumerable<User_Role>> ICommonRepository<User_Role>.Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User_Role>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            throw new NotImplementedException();
        }

        public int RecordCount(string searchValue)
        {
            throw new NotImplementedException();
        }

        public bool Exists(User_Role entity)
        {
            throw new NotImplementedException();
        }
    }
}
