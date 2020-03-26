using HR.WebApi.DAL;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HR.WebApi.Repositories
{
    public class CompanyRepository<T> : ICommonRepository<Company>
    {
        private readonly ApplicationDbContext adbContext;
        private Company_ContactRepository<Company_Contact> Comp_ContactRepo;

        public CompanyRepository(Company_ContactRepository<Company_Contact> ContactRepository)
        {
            adbContext = Startup.applicationDbContext;
            Comp_ContactRepo = ContactRepository;
        }

        public async Task<IEnumerable<Company>> Get(int id)
        {
            try
            {
                IList<Company> lstCompany = adbContext.company.AsEnumerable().Where(w => w.Company_Id == id).ToList();
                IList<Company_Contact> lstCompany_Contact = adbContext.company_contact.AsEnumerable().Where(w => w.Company_Id == id).ToList();

                return await Task.FromResult(lstCompany);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Company>> GetAll(int RecordLimit)
        {
            try
            {
                IList<Company> lstCompanies = adbContext.company.AsEnumerable().ToList();
                await Comp_ContactRepo.GetAll(RecordLimit).ConfigureAwait(false);
                return await Task.FromResult(lstCompanies);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task Insert(Company entity)
        {
            adbContext.BeginTransaction();
            try
            {

                entity.AddedOn = DateTime.Now;
                
                adbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                adbContext.SaveChanges();
               
                if (entity.Company_Contact != null)
                {
                    await Comp_ContactRepo.Insert_Multiple(entity.Company_Contact).ConfigureAwait(false);
                }
                
                adbContext.CommitTransaction();
            }
            catch (Exception ex)
            {
                adbContext.RollBackTransaction();
                throw ex;
            }
        }

        public async Task ToogleStatus(int id, short isActive)
        {
            adbContext.BeginTransaction();
            try
            {
                var vList = adbContext.company.AsEnumerable<Company>().Where(w => w.Company_Id == id && w.isActive != isActive).ToList().SingleOrDefault();
                if (vList != null)
                {
                    vList.isActive = isActive;
                    adbContext.company.Update(vList);
                    adbContext.SaveChanges();

                    await Comp_ContactRepo.ToogleStatus(id, isActive);

                    adbContext.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                adbContext.RollBackTransaction();
                throw ex;
            }
        }

        public async Task Update(Company entity)
        {
            adbContext.BeginTransaction();
            try
            {
                var vList = adbContext.company.Where(x => x.Company_Id == entity.Company_Id).FirstOrDefault();
                if (vList != null)
                {
                    vList.Company_Code = entity.Company_Code;
                    vList.Company_Name = entity.Company_Name;
                    vList.Company_Parent_Id = entity.Company_Parent_Id;
                    vList.Registration_No = entity.Registration_No;
                    vList.Registration_Date = entity.Registration_Date;

                    vList.Currency = entity.Currency;
                    vList.Language = entity.Language;

                    vList.isActive = entity.isActive;
                    vList.UpdatedBy = entity.UpdatedBy;
                    vList.UpdatedOn = DateTime.Now;

                    adbContext.company.Update(vList);
                    adbContext.SaveChanges();

                    if (entity.Company_Contact != null)
                    {
                        await Comp_ContactRepo.Update_Contact(entity.Company_Contact);
                    }
                    adbContext.CommitTransaction();
                }
                else
                {
                    throw new Exception("Data Not Available");
                }
            }
            catch (Exception ex)
            {
                adbContext.RollBackTransaction();
                throw ex;
            }
        }
        public async Task Delete(int id)
        {
            adbContext.BeginTransaction();
            try
            {
                var vList = adbContext.company.Where(w => w.Company_Id == id).ToList().SingleOrDefault();
                if (vList != null)
                {
                    vList.isActive = 0;
                    adbContext.company.Update(vList);
                    adbContext.SaveChanges();

                    await Comp_ContactRepo.DeleteByCompany(id);

                    adbContext.CommitTransaction();
                }
                else
                {
                    throw new Exception("Data Not Available");
                }
            }
            catch (Exception ex)
            {
                adbContext.RollBackTransaction();
                throw ex;
            }
        }

        public async Task<IEnumerable<Company>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    //Find Company with Paging
                    var vList = (from con in adbContext.company
                                 join c in adbContext.company_contact on con.Company_Id equals c.Company_Id
                                 select con
                                ).Skip(pageIndex * pageSize).Take(pageSize).ToList();

                    if (vList != null)
                        return await Task.FromResult(vList);
                    else
                        throw new Exception("Data Not Available");
                }
                else
                {
                    //Find Company with Paging & Searching
                    var vList = (from con in adbContext.company.Where(w => new[] { w.Company_Name, w.Company_Code }.Any(a => a.Contains(searchValue)))
                                 join c in adbContext.company_contact on con.Company_Id equals c.Company_Id
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
                    //Find Company all no of rows
                    var vCount = (from con in adbContext.company
                                  join c in adbContext.company_contact on con.Company_Id equals c.Company_Id
                                  select con.Company_Id
                                 ).Count();
                    return vCount;
                }
                else
                {
                    //Find Company no of rows with Searching
                    var vCount = (from con in adbContext.company.Where(w => new[] { w.Company_Name, w.Company_Code }.Any(a => a.Contains(searchValue)))
                                  join c in adbContext.company_contact on con.Company_Id equals c.Company_Id
                                  select con.Company_Id
                                 ).Count();
                    return vCount;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(Company entity)
        {
            int intCount = 0;
            if (entity.Company_Id > 0) //Update Validation
                intCount = adbContext.company.Where(w => w.Company_Id != entity.Company_Id && (w.Company_Code == entity.Company_Code && w.Company_Name == entity.Company_Name)).Count();
            else //Insert Validation
                intCount = adbContext.company.Where(w => (w.Company_Code == entity.Company_Code && w.Company_Name == entity.Company_Name)).Count();
            return (intCount > 0 ? true : false);
        }
    }
}
