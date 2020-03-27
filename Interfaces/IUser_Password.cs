using HR.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Interfaces
{
    public interface IUser_Password
    {
        bool AddUser_Password(int id, string password);

        bool ChangePassword(int id, string oldPassword, string newPassword,string oldEncyPassword);

        void AdminChangePassword(int loginId, string password);

        bool IsPasswordExist(int id, string password); //Validate last 5 password

        bool VerifyPassword(string password, string cryptPass); //verify with crypt

        string GeneratePassword(string password); //Generate Encrypted Password
    }
}
