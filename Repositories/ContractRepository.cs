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
    public class ContractRepository<T> : ICommonRepository<ContractView>, ICommonQuery<ContractView>
    {
        private readonly ApplicationDbContext adbContext;

        public ContractRepository()
        {
            adbContext = Startup.applicationDbContext;
        }

        public async Task<IEnumerable<ContractView>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    var vList = (from con in adbContext.contract
                                 join c in adbContext.company on con.Company_Id equals c.Company_Id
                                 where con.isActive == 1 && c.isActive == 1
                                 select new ContractView
                                 {
                                     Contract_Id = con.Contract_Id,
                                     Company_Id = con.Company_Id,
                                     Company_Name = c.Company_Name,
                                     Contract_Code = con.Contract_Code,
                                     Contract_Name = con.Contract_Name,
                                     Contract_HoursDaily = con.Contract_HoursDaily,
                                     Contract_HoursWeekly = con.Contract_HoursWeekly,
                                     Contract_Days = con.Contract_Days,
                                     Contract_Type = con.Contract_Type,
                                     Notes = con.Notes,
                                     isActive = con.isActive,
                                 }
                                ).Take(RecordLimit).ToList();

                    return await Task.FromResult(vList);
                }
                else
                {
                    var vList = (from con in adbContext.contract
                                 join c in adbContext.company on con.Company_Id equals c.Company_Id
                                 where con.isActive == 1 && c.isActive == 1
                                 select new ContractView
                                 {
                                     Contract_Id = con.Contract_Id,
                                     Company_Id = con.Company_Id,
                                     Company_Name = c.Company_Name,
                                     Contract_Code = con.Contract_Code,
                                     Contract_Name = con.Contract_Name,
                                     Contract_HoursDaily = con.Contract_HoursDaily,
                                     Contract_HoursWeekly = con.Contract_HoursWeekly,
                                     Contract_Days = con.Contract_Days,
                                     Contract_Type = con.Contract_Type,
                                     Notes = con.Notes,
                                     isActive = con.isActive,
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

        public async Task<IEnumerable<ContractView>> Get(int id)
        {
            try
            {
                var vList = (from con in adbContext.contract
                             join c in adbContext.company on con.Company_Id equals c.Company_Id
                             where con.Contract_Id == id && con.isActive == 1 && c.isActive == 1
                             select new ContractView
                             {
                                 Contract_Id = con.Contract_Id,
                                 Company_Id = con.Company_Id,
                                 Company_Name = c.Company_Name,
                                 Contract_Code = con.Contract_Code,
                                 Contract_Name = con.Contract_Name,
                                 Contract_HoursDaily = con.Contract_HoursDaily,
                                 Contract_HoursWeekly = con.Contract_HoursWeekly,
                                 Contract_Days = con.Contract_Days,
                                 Contract_Type = con.Contract_Type,
                                 Notes = con.Notes,
                                 isActive = con.isActive,
                             }
                                ).ToList();
                if (vList.Count > 0)
                    return await Task.FromResult(vList);
                else
                    throw new Exception("Data Not Available");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ContractView>> GetBy(SearchBy searchBy)
        {
            try
            {
                var vList = (from con in adbContext.contract.Where(String.Format("{0}=={1}", searchBy.FieldName, searchBy.FieldValue))
                             join c in adbContext.company on con.Company_Id equals c.Company_Id
                             where con.isActive == 1 && c.isActive == 1
                             select new ContractView
                             {
                                 Contract_Id = con.Contract_Id,

                                 Company_Id = con.Company_Id,
                                 Company_Name = c.Company_Name,
                                 
                                 Contract_Code = con.Contract_Code,
                                 Contract_Name = con.Contract_Name,
                                 Contract_HoursDaily = con.Contract_HoursDaily,
                                 Contract_HoursWeekly = con.Contract_HoursWeekly,
                                 Contract_Days = con.Contract_Days,
                                 Contract_Type = con.Contract_Type,
                                 Notes = con.Notes
                             }
                            ).Take(searchBy.RecordLimit).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ContractView>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    //Find Department with Paging
                    var vList = (from con in adbContext.contract
                                 join c in adbContext.company on con.Company_Id equals c.Company_Id
                                 select new ContractView
                                 {
                                     Contract_Id = con.Contract_Id,
                                     Company_Id = con.Company_Id,
                                     Company_Name = c.Company_Name,
                                     Contract_Code = con.Contract_Code,
                                     Contract_Name = con.Contract_Name,
                                     Contract_HoursDaily = con.Contract_HoursDaily,
                                     Contract_HoursWeekly = con.Contract_HoursWeekly,
                                     Contract_Days = con.Contract_Days,
                                     Contract_Type = con.Contract_Type,
                                     Notes = con.Notes,
                                     isActive = con.isActive
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
                    var vList = (from con in adbContext.contract.Where(w => new[] { w.Contract_Name, w.Contract_Code, w.Contract_Days, w.Contract_Type }.Any(a => a.Contains(searchValue)))
                                 join c in adbContext.company on con.Company_Id equals c.Company_Id
                                 select new ContractView
                                 {
                                     Contract_Id = con.Contract_Id,
                                     Company_Id = con.Company_Id,
                                     Company_Name = c.Company_Name,
                                     Contract_Code = con.Contract_Code,
                                     Contract_Name = con.Contract_Name,
                                     Contract_HoursDaily = con.Contract_HoursDaily,
                                     Contract_HoursWeekly = con.Contract_HoursWeekly,
                                     Contract_Days = con.Contract_Days,
                                     Contract_Type = con.Contract_Type,
                                     Notes = con.Notes,
                                     isActive = con.isActive,
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

        public async Task ToogleStatus(int id, short isActive)
        {
            try
            {

                var vList = adbContext.contract.Where(w => w.Contract_Id == id && w.isActive != isActive).ToList().SingleOrDefault();
                if (vList != null)
                {
                    vList.isActive = isActive;
                    adbContext.contract.Update(vList);
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

        public async Task Insert(ContractView entity)
        {
            try
            {
                Contract vList = new Contract
                {
                    //lstContract.Contract_Id = entity.Contract_Id,
                    Company_Id = entity.Company_Id,
                    Contract_Code = entity.Contract_Code,
                    Contract_Name = entity.Contract_Name,
                    Contract_HoursDaily = entity.Contract_HoursDaily,
                    Contract_HoursWeekly = entity.Contract_HoursWeekly,
                    Contract_Days = entity.Contract_Days,
                    Contract_Type = entity.Contract_Type,

                    isActive = entity.isActive,

                    AddedBy = entity.AddedBy,
                    AddedOn = DateTime.Now
                };

                adbContext.contract.Add(vList);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
        public async Task Update(ContractView entity)
        {
            try
            {
                var vList = adbContext.contract.Where(x => x.Contract_Id == entity.Contract_Id).FirstOrDefault();
                if (vList != null)
                {
                    //lstContract.Contract_Id = entity.Contract_Id;
                    vList.Company_Id = entity.Company_Id;
                    vList.Contract_Code = entity.Contract_Code;
                    vList.Contract_Name = entity.Contract_Name;
                    vList.Contract_HoursDaily = entity.Contract_HoursDaily;
                    vList.Contract_HoursWeekly = entity.Contract_HoursWeekly;
                    vList.Contract_Days = entity.Contract_Days;
                    vList.Contract_Type = entity.Contract_Type;
                    vList.Notes = entity.Notes;
                    vList.isActive = entity.isActive;
                    vList.UpdatedBy = entity.UpdatedBy;
                    vList.UpdatedOn = DateTime.Now;

                    adbContext.contract.Update(vList);
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
                var vList = adbContext.contract.Where(w => w.Contract_Id == id).ToList().SingleOrDefault();
                if (vList != null)
                {
                    adbContext.contract.Remove(vList);
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

        public bool Exists(ContractView entity)
        {
            int intCount = 0;
            if (entity.Contract_Id > 0) //Update Validation
                intCount = adbContext.contract.Where(w => w.Company_Id == entity.Company_Id && w.Contract_Id != entity.Contract_Id && (w.Contract_Code == entity.Contract_Code || w.Contract_Name == entity.Contract_Name)).Count();
            else //Insert Validation
                intCount = adbContext.contract.Where(w => w.Company_Id == entity.Company_Id && (w.Contract_Code == entity.Contract_Code || w.Contract_Name == entity.Contract_Name)).Count();
            return (intCount > 0 ? true : false);
        }

        public int RecordCount(string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    //Find Site all no of rows
                    var vCount = (from con in adbContext.contract
                                  join c in adbContext.company on con.Company_Id equals c.Company_Id
                                  select con.Contract_Id
                                 ).Count();
                    return vCount;
                }
                else
                {
                    //Find Site no of rows with Searching
                    var vCount = (from con in adbContext.contract.Where(w => new[] {w.Contract_Name, w.Contract_Code, w.Contract_Days, w.Contract_Type }.Any(a => a.Contains(searchValue)))
                                  join c in adbContext.company on con.Company_Id equals c.Company_Id
                                  select con.Contract_Id
                                 ).Count(); 
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
