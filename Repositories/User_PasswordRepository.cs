using HR.WebApi.DAL;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HR.WebApi.Repositories
{
    public class User_PasswordRepository : IUser_Password
    {
        private readonly ApplicationDbContext adbContext;

        public User_PasswordRepository()
        {
            adbContext = Startup.applicationDbContext;
        }

        //Create new user_password & verify 5 password only which are not same
        public bool Insert(int userId, string password)
        {
            bool blnInserted = false;
            try
            {
                var vList = GetUser_Password(userId);
                if (vList.Count() > 0)
                {
                    var vCount = vList.OrderByDescending(w => w.AddedOn).Skip(4);
                    adbContext.user_password.RemoveRange(vCount);
                    adbContext.SaveChanges();

                    if (!IsPasswordExist(userId, password))
                    {
                        blnInserted = AddNewUserPassword(userId, password);
                    }
                }
                else
                {
                    blnInserted = AddNewUserPassword(userId, password);
                }
            }
            catch (Exception ex)
            {
                blnInserted = false;
                throw ex;
            }
            return blnInserted;
        }

        private bool AddNewUserPassword(int userId, string password)
        {
            try
            {
                var vUserPassword = new User_Password
                {
                    User_Id = userId,
                    Password = GeneratePassword(password),
                    AddedBy = userId,
                    AddedOn = DateTime.Now
                };
                adbContext.user_password.Add(vUserPassword);
                adbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }        

        public bool VerifyPassword(string password, string cryptPass)
        {
            return CommonUtility.Crypt.Verify(password, cryptPass);
        }

        //Verify password should not match from past 5 old passwords
        public bool IsPasswordExist(int user_Id, string newPassword)
        {
            bool blnExist = false;
            var vList = GetUser_Password(user_Id);
            foreach (var item in vList)
            {
                if (VerifyPassword(newPassword, item.Password))
                    return blnExist = true;
                else
                    blnExist = false;
            }
            return blnExist;
        }

        public IEnumerable<User_Password> GetUser_Password(int id)
        {
            try
            {
                return adbContext.user_password.Where(w => w.User_Id == id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ChangePassword(int id, string oldPassword, string newPassword, string oldEncyPassword)
        {
            bool blnInserted = false;
            try
            {
                if (VerifyPassword(oldPassword, oldEncyPassword))
                {
                    if(Insert(id, newPassword))
                    {
                        blnInserted = true;
                    }                    
                }
            }
            catch (Exception ex)
            {
                blnInserted = false;
                throw ex;
            }
            return blnInserted;
        }

        public void AdminChangePassword(int id, string password)
        {
            AddNewUserPassword(id, password);
        }

        public string GeneratePassword(string password)
        {
            return CommonUtility.Crypt.GetPassword(password);
        }
    }
}