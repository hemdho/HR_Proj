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
    public class EthinicityRepository<T> : ICommonRepository<Ethinicity>
    {
        private readonly ApplicationDbContext adbContext;

        public EthinicityRepository()
        {
            adbContext = Startup.applicationDbContext;
        }

        public async Task<IEnumerable<Ethinicity>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    var vList = adbContext.ethinicity.Take(RecordLimit).ToList();
                    return await Task.FromResult(vList);
                }
                else
                {
                    var vList = adbContext.ethinicity.ToList();
                    return await Task.FromResult(vList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Ethinicity>> Get(int id)
        {
            try
            {
                IEnumerable<Ethinicity> lstEthinicity = adbContext.ethinicity.Where(w => w.Id == id).ToList();
                return await Task.FromResult(lstEthinicity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Ethinicity>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    //Find Ethinicity with Paging
                    var vList = adbContext.ethinicity.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                    if (vList.Count() >0)
                        return await Task.FromResult(vList);
                    else
                        throw new Exception("Data Not Available");
                }
                else
                {
                    //Find Ethinicity with Paging & Searching
                    var vList = adbContext.ethinicity.Where(w => new[] { w.Ethnicity_Name.ToLower(), w.Ethnicity_Code.ToLower() }.Any(a => a.Contains(searchValue.ToLower()))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
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

        public async Task Insert(Ethinicity entity)
        {
            try
            {
                entity.AddedOn = DateTime.Now;

                adbContext.ethinicity.Add(entity);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Ethinicity entity)
        {
            try
            {
                //Update Old Ethinicity
                Ethinicity lstEthinicity = new Ethinicity();
                lstEthinicity = adbContext.ethinicity.Where(x => x.Id == entity.Id).FirstOrDefault();
                if (lstEthinicity != null)
                {
                    lstEthinicity.Ethnicity_Code = entity.Ethnicity_Code;
                    lstEthinicity.Ethnicity_Name = entity.Ethnicity_Name;
                    lstEthinicity.Language = entity.Language;
                    lstEthinicity.Notes = entity.Notes;

                    lstEthinicity.isActive = entity.isActive;
                    lstEthinicity.UpdatedBy = entity.UpdatedBy;
                    lstEthinicity.UpdatedOn = DateTime.Now;

                    adbContext.ethinicity.Update(lstEthinicity);

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
                var vList = adbContext.ethinicity.Where(w => w.Id == id && w.isActive != isActive).SingleOrDefault();
                if (vList != null)
                {
                    vList.isActive = isActive;
                    adbContext.ethinicity.Update(vList);
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
                var vList = adbContext.ethinicity.Where(w => w.Id == id).SingleOrDefault();
                if (vList != null)
                {
                    adbContext.ethinicity.Remove(vList);
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

        public bool Exists(Ethinicity entity)
        {
            try
            {
                int intCount = 0;
                if (entity.Id > 0)  //Update Validation
                    intCount = adbContext.ethinicity.Where(w => w.Id != entity.Id && (w.Ethnicity_Code == entity.Ethnicity_Code || w.Ethnicity_Name == entity.Ethnicity_Name)).Count();
                else  //Insert Validation
                    intCount = adbContext.ethinicity.Where(w => w.Ethnicity_Code == entity.Ethnicity_Code || w.Ethnicity_Name == entity.Ethnicity_Name).Count();
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
                    //Find Ethinicity all no of rows
                    var vCount = adbContext.ethinicity.Count();
                    return vCount;
                }
                else
                {
                    //Find Ethinicity no of rows with Searching
                    var vCount = adbContext.ethinicity.Where(w => new[] { w.Ethnicity_Name.ToLower(), w.Ethnicity_Code.ToLower() }.Any(a => a.Contains(searchValue.ToLower()))).Count();
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
