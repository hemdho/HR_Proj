using HR.WebApi.Model;
using System.Collections.Generic;

namespace HR.WebApi.Interfaces
{
    public interface IUserService<T>
    {
        IEnumerable<T> GetAll(int RecordLimit);

        T GetUserById(int id);

        T GetUserByLoginId(string loginId);
        
        void UserCreate(T entity);

        void UserUpdate(T entity);

        bool UserExist(T entity);

        IEnumerable<T> FindUserById(int id);
        IEnumerable<T> FindUserByLogInId(string loginId);

        string ChangePassword(int id, string oldPassword, string newPassword);

        void AdminChangePassword(string loginId, string password);

        void ActivateUser(int id); //ToogleStatus

        void InactivateUser(int id); //ToogleStatus      

        IEnumerable<T> FindPaginated(int pageIndex, int pageSize, string searchValue);

        User_Response SignIn(User_Request user_Request);

        void SignOut(int id);
    }
}
