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
    public class ProbationRepository<T> : ICommonRepository<ProbationView>, ICommonQuery<ProbationView>
    {
        private readonly ApplicationDbContext adbContext;

        public ProbationRepository()
        {
            adbContext = Startup.applicationDbContext;
        }

        public async Task<IEnumerable<ProbationView>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    var vList = (from prob in adbContext.probation
                                 join comp in adbContext.company on prob.Company_Id equals comp.Company_Id
                                 where prob.isActive == 1 && comp.isActive == 1
                                 select new ProbationView
                                 {
                                     Prob_Id = prob.Prob_Id,
                                     Company_Id = prob.Company_Id,
                                     Prob_Code = prob.Prob_Code,
                                     Prob_Name = prob.Prob_Name,
                                     Prob_Description = prob.Prob_Description,
                                     Prob_DurationUnit = prob.Prob_DurationUnit,
                                     Prob_Duration = prob.Prob_Duration,
                                     Notes = prob.Notes,
                                     isActive = prob.isActive,
                                     Company_Name = comp.Company_Name
                                 }
                                ).Take(RecordLimit).ToList();
                    return await Task.FromResult(vList);
                }
                else
                {
                    var vList = (from prob in adbContext.probation
                                 join comp in adbContext.company on prob.Company_Id equals comp.Company_Id
                                 where prob.isActive == 1 && comp.isActive == 1
                                 select new ProbationView
                                 {
                                     Prob_Id = prob.Prob_Id,
                                     Company_Id = prob.Company_Id,
                                     Prob_Code = prob.Prob_Code,
                                     Prob_Name = prob.Prob_Name,
                                     Prob_Description = prob.Prob_Description,
                                     Prob_DurationUnit = prob.Prob_DurationUnit,
                                     Prob_Duration = prob.Prob_Duration,
                                     Notes = prob.Notes,
                                     isActive = prob.isActive,
                                     Company_Name = comp.Company_Name
                                 }).ToList();
                    return await Task.FromResult(vList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ProbationView>> Get(int id)
        {
            try
            {
                var vList = (from prob in adbContext.probation
                             join comp in adbContext.company on prob.Company_Id equals comp.Company_Id
                             where prob.Prob_Id == id && prob.isActive == 1 && comp.isActive == 1
                             select new ProbationView
                             {
                                 Prob_Id = prob.Prob_Id,
                                 Company_Id = prob.Company_Id,
                                 Prob_Code = prob.Prob_Code,
                                 Prob_Name = prob.Prob_Name,
                                 Prob_Description = prob.Prob_Description,
                                 Prob_DurationUnit = prob.Prob_DurationUnit,
                                 Prob_Duration = prob.Prob_Duration,
                                 Notes = prob.Notes,
                                 isActive = prob.isActive,
                                 Company_Name = comp.Company_Name
                             }).ToList();
                return await Task.FromResult(vList);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ProbationView>> GetBy(SearchBy searchBy)
        {
            try
            {
                var vList = (from prob in adbContext.probation.Where(String.Format("{0}=={1}", searchBy.FieldName, searchBy.FieldValue))
                             join comp in adbContext.company on prob.Company_Id equals comp.Company_Id
                             where prob.isActive == 1 && comp.isActive == 1
                             select new ProbationView
                             {
                                 Prob_Id = prob.Prob_Id,
                                 Company_Id = prob.Company_Id,
                                 Company_Name = comp.Company_Name,
                                 Prob_Code = prob.Prob_Code,
                                 Prob_Name = prob.Prob_Name,
                                 Prob_Description = prob.Prob_Description,
                                 Prob_DurationUnit = prob.Prob_DurationUnit,
                                 Prob_Duration = prob.Prob_Duration,
                                 Notes = prob.Notes,
                             }
                            ).Take(searchBy.RecordLimit).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ProbationView>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    //Find Probation with Paging
                    var vList = (from prob in adbContext.probation
                                 join comp in adbContext.company on prob.Company_Id equals comp.Company_Id
                                 where prob.isActive == 1 && comp.isActive == 1
                                 select new ProbationView
                                 {
                                     Prob_Id = prob.Prob_Id,
                                     Company_Id = prob.Company_Id,
                                     Prob_Code = prob.Prob_Code,
                                     Prob_Name = prob.Prob_Name,
                                     Prob_Description = prob.Prob_Description,
                                     Prob_DurationUnit = prob.Prob_DurationUnit,
                                     Prob_Duration = prob.Prob_Duration,
                                     Notes = prob.Notes,
                                     isActive = prob.isActive,
                                     Company_Name = comp.Company_Name
                                 }
                                 ).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                    if (vList != null)
                        return await Task.FromResult(vList);
                    else
                        throw new Exception("Data Not Available");
                }
                else
                {
                    //Find Probation with Paging & Searching
                    var vList = (from prob in adbContext.probation.Where(w => new[] { w.Prob_Name.ToLower(), w.Prob_Code.ToLower() }.Any(a => a.Contains(searchValue.ToLower())))
                                 join comp in adbContext.company on prob.Company_Id equals comp.Company_Id
                                 where prob.isActive == 1 && comp.isActive == 1
                                 select new ProbationView
                                 {
                                     Prob_Id = prob.Prob_Id,
                                     Company_Id = prob.Company_Id,
                                     Prob_Code = prob.Prob_Code,
                                     Prob_Name = prob.Prob_Name,
                                     Prob_Description = prob.Prob_Description,
                                     Prob_DurationUnit = prob.Prob_DurationUnit,
                                     Prob_Duration = prob.Prob_Duration,
                                     Notes = prob.Notes,
                                     isActive = prob.isActive,
                                     Company_Name = comp.Company_Name
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

        public async Task Insert(ProbationView entity)
        {
            try
            {
                //Insert New Probation
                var vList = new Probation
                {
                    Prob_Id = entity.Prob_Id,
                    Company_Id = entity.Company_Id,
                    Prob_Code = entity.Prob_Code,
                    Prob_Name = entity.Prob_Name,
                    Prob_Description = entity.Prob_Description,
                    Prob_DurationUnit = entity.Prob_DurationUnit,
                    Prob_Duration = entity.Prob_Duration,
                    Notes = entity.Notes,

                    isActive = entity.isActive,
                    AddedBy = entity.AddedBy,
                    AddedOn = DateTime.Now
                };

                adbContext.probation.Add(vList);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(ProbationView entity)
        {
            var lstProb = adbContext.probation.Where(x => x.Prob_Id == entity.Prob_Id).FirstOrDefault();

            if (lstProb != null)
            {
                try
                {
                    //lstProb.Prob_Id = entity.Prob_Id;
                    lstProb.Company_Id = entity.Company_Id;
                    lstProb.Prob_Code = entity.Prob_Code;
                    lstProb.Prob_Name = entity.Prob_Name;
                    lstProb.Prob_Description = entity.Prob_Description;
                    lstProb.Prob_DurationUnit = entity.Prob_DurationUnit;
                    lstProb.Prob_Duration = entity.Prob_Duration;

                    lstProb.isActive = entity.isActive;
                    lstProb.UpdatedBy = entity.UpdatedBy;
                    lstProb.UpdatedOn = DateTime.Now;

                    adbContext.probation.Update(lstProb);

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
                var vList = adbContext.probation.Where(w => w.Prob_Id == id && w.isActive != isActive).ToList().SingleOrDefault();
                vList.isActive = isActive;
                adbContext.probation.Update(vList);
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
                var vList = adbContext.probation.Where(w => w.Prob_Id == id).ToList().SingleOrDefault();
                adbContext.probation.Remove(vList);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(ProbationView entity)
        {
            try
            {
                int intCount = 0;
                if (entity.Prob_Id > 0)
                    intCount = adbContext.probation.Where(w => w.Prob_Id != entity.Prob_Id && (w.Prob_Code == entity.Prob_Code || w.Prob_Name == entity.Prob_Name)).Count();
                else
                    intCount = adbContext.probation.Where(w => w.Prob_Code == entity.Prob_Code || w.Prob_Name == entity.Prob_Name).Count();
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
                    //Find all no of rows
                    var vCount = (from prob in adbContext.probation
                                  join comp in adbContext.company on prob.Company_Id equals comp.Company_Id
                                  where prob.isActive == 1 && comp.isActive == 1
                                  select prob.Prob_Id
                                ).Count();
                    return vCount;
                }
                else
                {
                    //Find no of rows with Searching
                    var vCount = (from prob in adbContext.probation.Where(w => new[] { w.Prob_Name.ToLower(), w.Prob_Code.ToLower() }.Any(a => a.Contains(searchValue.ToLower())))
                                  join comp in adbContext.company on prob.Company_Id equals comp.Company_Id
                                  where prob.isActive == 1 && comp.isActive == 1
                                  select prob.Prob_Id
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
