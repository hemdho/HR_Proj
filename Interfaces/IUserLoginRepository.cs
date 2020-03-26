using HR.WebApi.Model;
using HR.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Interfaces
{
    public interface IUserLoginRepository
    {
        User_Response SignIn(User_Request user_Request);

        void SignOut(int id);
    }
}
