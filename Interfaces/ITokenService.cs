using HR.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Interfaces
{
    interface ITokenService
    {
        void Add(User_Token entity);
        void Remove(User_Token entity);
        void Delete(int id);
        bool Verify(int userId, string strTokenNo);
        bool ValidateByUser(int userId);
        string GenerateToken();
        string GetTokenByUserId(int userId);
    }
}
