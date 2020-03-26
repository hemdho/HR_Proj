using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HR.WebApi.Interfaces
{
    public interface ICommonRepository<T>
    {
        Task<IEnumerable<T>> GetAll(int RecordLimit);

        Task<IEnumerable<T>> Get(int id);

        Task<IEnumerable<T>> FindPaginated(int pageIndex, int pageSize, string searchValue);

        int RecordCount(string searchValue);

        //Task<IList<T>> FindPaginatedList(int pageIndex, int pageSize, Expression<Func<T, bool>> expression);

        Task Insert(T entity);

        Task Update(T entity);

        Task ToogleStatus(int id, Int16 isActive);

        Task Delete(int id);

        bool Exists(T entity);
    }
}
