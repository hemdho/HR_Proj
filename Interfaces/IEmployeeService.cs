using HR.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HR.WebApi.Interfaces
{
    public interface IEmployeeService<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetEmployee(int id);

        Task<T> GetEmployee_Address(int id);
        Task<T> GetEmployee_Bank(int id);
        Task<T> GetEmployee_BasicInfo(int id);
        Task<T> GetEmployee_Contact(int id);
        Task<T> GetEmployee_Contract(int id);
        Task<T> GetEmployee_Document(int id);
        Task<T> GetEmployee_Emergency(int id);
        Task<T> GetEmployee_Probation(int id);
        Task<T> GetEmployee_Reference(int id);
        Task<T> GetEmployee_Resignation(int id);
        Task<T> GetEmployee_RightToWork(int id);
        Task<T> GetEmployee_Salary(int id);

        Task Insert(T entity); //Save employee with their related information
        Task InsertEmployee(T entity); //Save in employee table only
        Task InsertEmployee_Address(T entity);
        Task InsertEmployee_Bank(T entity);
        Task InsertEmployee_BasicInfo(T entity);
        Task InsertEmployee_Contact(T entity);

        Task InsertEmployee_Document(T entity);
        Task InsertEmployee_Emergency(T entity);
        Task InsertEmployee_Probation(T entity);
        Task InsertEmployee_Reference(T entity);
        Task InsertEmployee_Resignation(T entity);
        Task InsertEmployee_RightToWork(T entity);

        Task Update(T entity);

        Task ToogleStatus(int id, Int16 isActive);

        bool IsDuplicate(T entity);

        Task<IEnumerable<T>> PaginatedList(int pageIndex, int pageSize);

        Task<IList<T>> FindPaginatedList(int pageIndex, int pageSize, Expression<Func<T, bool>> expression);

        Task<IList<T>> FindAnyValue(int pageIndex, int pageSize, string searchValue);
    }
}
