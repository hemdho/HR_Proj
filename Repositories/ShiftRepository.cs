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
    public class ShiftRepository<T> : ICommonRepository<ShiftView>, ICommonQuery<ShiftView>
    {
        private readonly ApplicationDbContext adbContext;

        public ShiftRepository()
        {
            adbContext = Startup.applicationDbContext;
        }

        public async Task<IEnumerable<ShiftView>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    var vList = (from s in adbContext.shift
                                 join comp in adbContext.company on s.Company_Id equals comp.Company_Id
                                 where s.isActive == 1 && comp.isActive == 1
                                 select new ShiftView
                                 {
                                     Shift_Id = s.Shift_Id,
                                     Company_Id = s.Company_Id,
                                     Company_Name = comp.Company_Name,
                                     Shift_Code = s.Shift_Code,
                                     Shift_Name = s.Shift_Name,
                                     Shift_Start = s.Shift_Start,
                                     Shift_End = s.Shift_End,
                                     NightShift = s.NightShift,
                                     Shift_Variable = s.Shift_Variable,
                                     isActive = s.isActive
                                 }
                                ).Take(RecordLimit).ToList();

                    return await Task.FromResult(vList);
                }
                else
                {
                    var vList = (from s in adbContext.shift
                                 join comp in adbContext.company on s.Company_Id equals comp.Company_Id
                                 where s.isActive == 1 && comp.isActive == 1
                                 select new ShiftView
                                 {
                                     Shift_Id = s.Shift_Id,
                                     Company_Id = s.Company_Id,
                                     Company_Name = comp.Company_Name,
                                     Shift_Code = s.Shift_Code,
                                     Shift_Name = s.Shift_Name,
                                     Shift_Start = s.Shift_Start,
                                     Shift_End = s.Shift_End,
                                     NightShift = s.NightShift,
                                     Shift_Variable = s.Shift_Variable,
                                     isActive = s.isActive
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

        public async Task<IEnumerable<ShiftView>> Get(int id)
        {
            try
            {
                var vList = (from s in adbContext.shift
                             join comp in adbContext.company on s.Company_Id equals comp.Company_Id
                             where s.Shift_Id == id && s.isActive == 1 && comp.isActive == 1
                             select new ShiftView
                             {
                                 Shift_Id = s.Shift_Id,
                                 Company_Id = s.Company_Id,
                                 Company_Name = comp.Company_Name,
                                 Shift_Code = s.Shift_Code,
                                 Shift_Name = s.Shift_Name,
                                 Shift_Start = s.Shift_Start,
                                 Shift_End = s.Shift_End,
                                 NightShift = s.NightShift,
                                 Shift_Variable = s.Shift_Variable,
                                 isActive = s.isActive
                             }
                                ).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ShiftView>> GetBy(SearchBy searchBy)
        {
            try
            {
                var vList = (from s in adbContext.shift.Where(String.Format("{0}=={1}", searchBy.FieldName, searchBy.FieldValue))
                             join comp in adbContext.company on s.Company_Id equals comp.Company_Id
                             where s.isActive == 1 && comp.isActive == 1
                             select new ShiftView
                             {
                                 Shift_Id = s.Shift_Id,
                                 Company_Id = s.Company_Id,
                                 Company_Name = comp.Company_Name,
                                 Shift_Code = s.Shift_Code,
                                 Shift_Name = s.Shift_Name,
                                 Shift_Start = s.Shift_Start,
                                 Shift_End = s.Shift_End,
                                 NightShift = s.NightShift,
                                 Shift_Variable = s.Shift_Variable
                             }
                            ).Take(searchBy.RecordLimit).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ShiftView>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    //Find Department with Paging
                    var vList = (from s in adbContext.shift
                                 join comp in adbContext.company on s.Company_Id equals comp.Company_Id
                                 select new ShiftView
                                 {
                                     Shift_Id = s.Shift_Id,
                                     Company_Id = s.Company_Id,
                                     Company_Name = comp.Company_Name,
                                     Shift_Code = s.Shift_Code,
                                     Shift_Name = s.Shift_Name,
                                     Shift_Start = s.Shift_Start,
                                     Shift_End = s.Shift_End,
                                     NightShift = s.NightShift,
                                     Shift_Variable = s.Shift_Variable,
                                     isActive = s.isActive
                                 }
                                ).Skip(pageIndex * pageSize).Take(pageSize).ToList();

                    if (vList.Count() > 0)
                        return await Task.FromResult(vList);
                    else
                        throw new Exception("Data Not Available");
                }
                else
                {
                    //Find Department with Paging & Searching
                    var vList = (from s in adbContext.shift.Where(w => new[] { w.Shift_Name, w.Shift_Code, w.Shift_Start, w.Shift_End }.Any(a => a.Contains(searchValue))).ToList()
                                 join comp in adbContext.company on s.Company_Id equals comp.Company_Id
                                 select new ShiftView
                                 {
                                     Shift_Id = s.Shift_Id,
                                     Company_Id = s.Company_Id,
                                     Company_Name = comp.Company_Name,
                                     Shift_Code = s.Shift_Code,
                                     Shift_Name = s.Shift_Name,
                                     Shift_Start = s.Shift_Start,
                                     Shift_End = s.Shift_End,
                                     NightShift = s.NightShift,
                                     Shift_Variable = s.Shift_Variable,
                                     isActive = s.isActive
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

        public async Task Insert(ShiftView entity)
        {
            try
            {
                var vList = new Shift
                {
                    Shift_Id = entity.Shift_Id,
                    Company_Id = entity.Company_Id,
                    Shift_Code = entity.Shift_Code,
                    Shift_Name = entity.Shift_Name,
                    Shift_Start = entity.Shift_Start,
                    Shift_End = entity.Shift_End,
                    NightShift = entity.NightShift,
                    Shift_Variable = entity.Shift_Variable,
                    isActive = entity.isActive,
                    AddedOn = DateTime.Now,
                    AddedBy = entity.AddedBy
                };
                adbContext.shift.Add(vList);

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
                var vList = adbContext.shift.Where(w => w.Shift_Id == id && w.isActive != isActive).ToList().SingleOrDefault();
                if (vList != null)
                {
                    vList.isActive = isActive;
                    adbContext.shift.Update(vList);
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

        public async Task Update(ShiftView entity)
        {
            try
            {
                var lstShift = adbContext.shift.Where(x => x.Shift_Id == entity.Shift_Id).FirstOrDefault();
                if (lstShift != null)
                {
                    lstShift.Company_Id = entity.Company_Id;
                    lstShift.Shift_Code = entity.Shift_Code;
                    lstShift.Shift_Name = entity.Shift_Name;
                    lstShift.Shift_Start = entity.Shift_Start;
                    lstShift.Shift_End = entity.Shift_End;
                    lstShift.NightShift = entity.NightShift;
                    lstShift.Shift_Variable = entity.Shift_Variable;

                    lstShift.isActive = entity.isActive;
                    lstShift.UpdatedBy = entity.UpdatedBy;
                    lstShift.UpdatedOn = DateTime.Now;

                    adbContext.shift.Update(lstShift);

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
                var vList = adbContext.shift.Where(w => w.Shift_Id == id).ToList().SingleOrDefault();
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

        public int RecordCount(string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    //Find Shift all no of rows
                    var vCount = (from s in adbContext.shift
                                  join comp in adbContext.company on s.Company_Id equals comp.Company_Id
                                  select s.Shift_Id
                                ).Count();
                    return vCount;
                }
                else
                {
                    //Find Shift no of rows with Searching
                    var vCount = (from s in adbContext.shift.Where(w => new[] { w.Shift_Name, w.Shift_Code, w.Shift_Start, w.Shift_End }.Any(a => a.Contains(searchValue))).ToList()
                                  join comp in adbContext.company on s.Company_Id equals comp.Company_Id
                                  select s.Shift_Id
                               ).Count();
                    return vCount;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(ShiftView entity)
        {
            int intCount = 0;
            if (entity.Shift_Id > 0) //Update Validation
                intCount = adbContext.shift.Where(w => w.Company_Id == entity.Company_Id && w.Shift_Id != entity.Shift_Id && (w.Shift_Code == entity.Shift_Code && w.Shift_Name == entity.Shift_Name)).Count();
            else //Insert Validation
                intCount = adbContext.shift.Where(w => w.Company_Id == entity.Company_Id && (w.Shift_Code == entity.Shift_Code && w.Shift_Name == entity.Shift_Name)).Count();
            return (intCount > 0 ? true : false);
        }
    }
}

