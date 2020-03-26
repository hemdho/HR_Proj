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
    public class Company_ContactRepository<T> : ICommonRepository<Company_Contact>
    {
        private readonly ApplicationDbContext adbContext;

        public Company_ContactRepository()
        {
            adbContext = Startup.applicationDbContext;
        }

        public async Task<IEnumerable<Company_Contact>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    var vList = adbContext.company_contact.Where(w => w.isActive == 1).Take(RecordLimit).ToList();
                    return await Task.FromResult(vList);
                }
                else
                {
                    var vList = adbContext.company_contact.ToList();
                    return await Task.FromResult(vList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Company_Contact>> Get(int id)
        {
            try
            {
                var vList = adbContext.company_contact.Where(w => w.Company_Id == id && w.isActive == 1).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Company_Contact>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    //Find Company Contact with Paging
                    var vList = (from con in adbContext.company_contact
                                 join c in adbContext.company on con.Company_Id equals c.Company_Id
                                 select con
                                ).Skip(pageIndex * pageSize).Take(pageSize).ToList();

                    if (vList != null)
                        return await Task.FromResult(vList);
                    else
                        throw new Exception("Data Not Available");
                }
                else
                {
                    //Find Company Contact with Paging & Searching
                    var vList = (from con in adbContext.company_contact.Where(w => new[] { w.Contact_Type, w.Contact_Value }.Any(a => a.Contains(searchValue)))
                                 join c in adbContext.company on con.Company_Id equals c.Company_Id
                                 select con
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

        public int RecordCount(string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    //Find Company Contact all no of rows
                    var vCount = (from con in adbContext.company_contact
                                  join c in adbContext.company on con.Company_Id equals c.Company_Id
                                  select con.Company_Contact_Id
                                ).Count();
                    return vCount;
                }
                else
                {
                    //Find Company Contact no of rows with Searching
                    var vCount = (from con in adbContext.company_contact.Where(w => new[] { w.Contact_Type.ToLower(), w.Contact_Value.ToLower() }.Any(a => a.Contains(searchValue.ToLower())))
                                  join c in adbContext.company on con.Company_Id equals c.Company_Id
                                  select con.Company_Contact_Id
                                ).Count();
                    return vCount;
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
                var vList = adbContext.company_contact.Where(w => w.Company_Contact_Id == id).ToList().SingleOrDefault();
                if (vList != null)
                {
                    vList.isActive = 0;
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
        public async Task DeleteByCompany(int id)
        {
            try
            {
                var vList = adbContext.company_contact.Where(w => w.Company_Id == id).ToList();
                if (vList.Count() > 0)
                {
                    vList.ForEach(w => w.isActive = 0);
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

        public bool Exists(Company_Contact entity)
        {
            int intCount = 0;
            if (entity.Company_Contact_Id > 0) //Update Validation
                intCount = adbContext.company_contact.Where(w => w.Company_Id == entity.Company_Id && w.Company_Contact_Id != entity.Company_Contact_Id && (w.Contact_Type == entity.Contact_Type && w.Contact_Value == entity.Contact_Value)).Count();
            else //Insert Validation
                intCount = adbContext.company_contact.Where(w => w.Company_Id != entity.Company_Id && w.Contact_Type == entity.Contact_Type && w.Contact_Value == entity.Contact_Value).Count();
            return (intCount > 0 ? true : false);
        }

        public async Task Insert(Company_Contact entity)
        {
            try
            {
                entity.AddedOn = DateTime.Now;
                adbContext.company_contact.Add(entity);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Insert_Multiple(IEnumerable<Company_Contact> entity)
        {
            try
            {

                entity.ToList().ForEach(w => w.AddedOn = DateTime.Now);
                adbContext.company_contact.AddRange(entity);
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
                var vList = adbContext.company_contact.AsEnumerable().Where(w => w.Company_Id == id && w.isActive != isActive).ToList();
                if (vList != null)
                {
                    vList.ForEach(w => w.isActive = isActive);
                    adbContext.company_contact.UpdateRange(vList);
                    await Task.FromResult(adbContext.SaveChanges());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Company_Contact entity)
        {
            try
            {
                var vList = adbContext.company_contact.Where(w => w.Company_Contact_Id == entity.Company_Contact_Id).FirstOrDefault();
                if (vList != null)
                {
                    vList.Company_Id = entity.Company_Id;
                    vList.Contact_Type = entity.Contact_Type.Trim();
                    vList.Contact_Value = entity.Contact_Value.Trim();
                    vList.isDefault = entity.isDefault;
                    vList.isActive = entity.isActive;
                    vList.UpdatedBy = entity.UpdatedBy;
                    vList.UpdatedOn = DateTime.Now;

                    adbContext.company_contact.Update(vList);
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
        public async Task Update_Contact(IEnumerable<Company_Contact> entity)
        {
            try
            {
                foreach (var contact in entity)
                {
                    await Update(contact);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
