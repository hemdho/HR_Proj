using HR.WebApi.DAL;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Repositories
{
    public class DocumentRepository<T> : ICommonRepository<Document>
    {
        private readonly ApplicationDbContext adbContext;

        public DocumentRepository()
        {
            adbContext = Startup.applicationDbContext;
        }

        public async Task<IEnumerable<Document>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    return await Task.FromResult(adbContext.document.Take(RecordLimit).ToList());
                }
                else
                {
                    return await Task.FromResult(adbContext.document.ToList());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Document>> Get(int id)
        {
            try
            {
                var vList = adbContext.document.Where(w => w.Id == id).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Document>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    //Find Document with Paging
                    var vList = adbContext.document.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                    if (vList.Count() > 0)
                        return await Task.FromResult(vList);
                    else
                        throw new Exception("Data Not Available");
                }
                else
                {
                    //Find Document with Paging & Searching
                    var vList = adbContext.document.Where(w => new[] { w.Name.ToLower(), w.FileType.ToLower(), w.Category.ToLower(), w.Notes.ToLower() }.Any(a => a.Contains(searchValue.ToLower()))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
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

        public async Task Insert(Document entity)
        {
            try
            {
                entity.AddedOn = DateTime.Now;
                adbContext.document.Add(entity);

                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Document entity)
        {
            try
            {
                //Update Old Document
                var lstDocument = adbContext.document.Where(x => x.Id == entity.Id).FirstOrDefault();
                if (lstDocument != null)
                {
                    lstDocument.Name = entity.Name;
                    lstDocument.FileType = entity.FileType;
                    lstDocument.Category = entity.Category;
                    lstDocument.Notes = entity.Notes;

                    lstDocument.isActive = entity.isActive;
                    lstDocument.UpdatedBy = entity.UpdatedBy;
                    lstDocument.UpdatedOn = DateTime.Now;

                    adbContext.document.Update(lstDocument);
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
                var vList = adbContext.document.Where(w => w.Id == id && w.isActive != isActive).SingleOrDefault();
                if (vList != null)
                {
                    vList.isActive = isActive;
                    adbContext.document.Update(vList);
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
                //Delete Document
                var vList = adbContext.document.Where(w => w.Id == id).SingleOrDefault();
                if (vList != null)
                {
                    adbContext.document.Remove(vList);
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

        public bool Exists(Document entity)
        {
            try
            {
                int intCount = 0;
                if (entity.Id > 0) //Update Validation
                    intCount = adbContext.document.Where(w => w.Id != entity.Id && w.Name == entity.Name).Count();
                else //Insert Validation
                    intCount = adbContext.document.Where(w => w.Name == entity.Name).Count();
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
                    //Find Document all no of rows
                    var vCount = adbContext.document.Count();
                    return vCount;
                }
                else
                {
                    //Find Document no of rows with Searching
                    var vCount = adbContext.document.Where(w => new[] { w.Name.ToLower(), w.FileType.ToLower(), w.Category.ToLower(), w.Notes.ToLower() }.Any(a => a.Contains(searchValue.ToLower()))).Count();
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
