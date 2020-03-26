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
    public class Marital_StatusRepository<T> : ICommonRepository<Marital_status>
    {
        private readonly ApplicationDbContext adbContext;

        public Marital_StatusRepository()
        {
            adbContext = Startup.applicationDbContext;
        }

        public async Task<IEnumerable<Marital_status>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    var vList = adbContext.marital_status.Take(RecordLimit).ToList();
                    return await Task.FromResult(vList);
                }
                else
                {
                    var vList = adbContext.marital_status.ToList();
                    return await Task.FromResult(vList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Marital_status>> Get(int id)
        {
            try
            {
                var vList = adbContext.marital_status.Where(w => w.Id == id).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Marital_status>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    //Find Marital_status with Paging
                    var vList = adbContext.marital_status.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                    if (vList.Count() > 0)
                        return await Task.FromResult(vList);
                    else
                        throw new Exception("Data Not Available");
                }
                else
                {
                    //Find Marital_status with Paging & Searching
                    var vList = adbContext.marital_status.Where(w => new[] { w.Marital_Status.ToLower() }.Any(a => a.Contains(searchValue.ToLower()))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
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

        public async Task Insert(Marital_status entity)
        {
            try
            {
                entity.AddedOn = DateTime.Now;
                adbContext.marital_status.Add(entity);

                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Marital_status entity)
        {
            try
            {
                //Update Old Marital_status
                var lstMarital_Status = adbContext.marital_status.Where(x => x.Id == entity.Id).FirstOrDefault();
                if (lstMarital_Status != null)
                {
                    lstMarital_Status.Marital_Status = entity.Marital_Status;

                    lstMarital_Status.isActive = entity.isActive;
                    lstMarital_Status.UpdatedBy = entity.UpdatedBy;
                    lstMarital_Status.UpdatedOn = DateTime.Now;

                    adbContext.marital_status.Update(lstMarital_Status);
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
                var vList = adbContext.marital_status.Where(w => w.Id == id && w.isActive != isActive).SingleOrDefault();
                if (vList != null)
                {
                    vList.isActive = isActive;
                    adbContext.marital_status.Update(vList);
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
                //Delete Marital_status
                var vList = adbContext.marital_status.Where(w => w.Id == id).SingleOrDefault();
                if (vList != null)
                {
                    adbContext.marital_status.Remove(vList);
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

        public bool Exists(Marital_status entity)
        {
            try
            {
                int intCount = 0;
                if (entity.Id > 0) //Update Validation
                    intCount = adbContext.marital_status.Where(w => w.Id != entity.Id && (w.Marital_Status == entity.Marital_Status)).Count();
                else //Insert Validation
                    intCount = adbContext.marital_status.Where(w => w.Marital_Status == entity.Marital_Status).Count();
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
                    //Find Marital_status all no of rows
                    var vCount = adbContext.marital_status.Count();
                    return vCount;
                }
                else
                {
                    //Find Marital_status no of rows with Searching
                    var vCount = adbContext.marital_status.Where(w => new[] { w.Marital_Status.ToLower() }.Any(a => a.Contains(searchValue.ToLower()))).Count();
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
