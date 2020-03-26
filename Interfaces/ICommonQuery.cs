using HR.WebApi.ModelView;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR.WebApi.Interfaces
{
    public interface ICommonQuery<T>
    {
        Task<IEnumerable<T>> GetBy(SearchBy searchBy);

        //Task<IEnumerable<T>> GetAdvanceSearch(IList<SearchBy> searchByList);
    }
}
