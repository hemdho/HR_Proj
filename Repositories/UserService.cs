using HR.WebApi.Common;
using HR.WebApi.DAL;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HR.WebApi.Repositories
{
    public class UserService<T> : IUserService<User>
    {
        private readonly ApplicationDbContext adbContext;
        private string EmailDefaultPassword = AppSettings.EmailDefaultPassword;
        private int PasswordExpiryTime = AppSettings.PasswordExpiryTime;
        private int PasswordExpiryDays = AppSettings.PasswordExpiryDays;

        private User_PasswordRepository user_PasswordRepository;
        public TokenService tokenService;

        public UserService(TokenService tokenService, User_PasswordRepository user_PasswordRepository)
        {
            adbContext = Startup.applicationDbContext;
            this.tokenService = tokenService;
            this.user_PasswordRepository = user_PasswordRepository;
        }

        //Get users data
        public IEnumerable<User> GetAll(int RecordLimit)
        {
            try
            {
                IList<User> vList;
                if (RecordLimit > 0)
                    vList = adbContext.users.Take(RecordLimit).ToList();
                else
                    vList = adbContext.users.ToList();

                return vList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //get user by userId
        public User GetUserById(int id)
        {
            try
            {
                return adbContext.users.Where(w => w.User_Id == id).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //get user by LoginId
        public User GetUserByLoginId(string login_Id)
        {
            try
            {
                return adbContext.users.Where(w => w.Login_Id == login_Id).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UserCreate(User entity)
        {
            adbContext.BeginTransaction();
            try
            {
                entity.Password = user_PasswordRepository.GeneratePassword(entity.Password);
                entity.PasswordExpiryDate = DateTime.Now.AddDays(PasswordExpiryTime);
                entity.Attempted = 1;
                entity.isLocked = 0;
                entity.isActive = 1;
                entity.AddedOn = DateTime.Now;

                adbContext.users.Add(entity);
                adbContext.SaveChanges();

                user_PasswordRepository.AddUser_Password(entity.User_Id, entity.Password);

                adbContext.CommitTransaction();
            }
            catch (Exception ex)
            {
                adbContext.RollBackTransaction();
                throw ex;
            }
        }

        public void UserUpdate(User entity)
        {
            adbContext.BeginTransaction();
            try
            {
                if (user_PasswordRepository.AddUser_Password(entity.User_Id, entity.Password))
                {
                    entity.UpdatedOn = DateTime.Now;

                    adbContext.users.Update(entity);
                    adbContext.SaveChanges();
                }
                adbContext.CommitTransaction();
            }
            catch (Exception ex)
            {
                adbContext.RollBackTransaction();
                throw ex;
            }
        }

        public bool UserExist(User entity)
        {
            try
            {
                int intCount = 0;
                if (entity.User_Id > 0)
                    intCount = adbContext.users.Where(w => w.User_Id != entity.User_Id && w.Login_Id == entity.Login_Id).Count();
                else
                    intCount = adbContext.users.Where(w => w.Login_Id == entity.Login_Id).Count();
                return (intCount > 0 ? true : false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Set Active User
        public void ActivateUser(int id)
        {
            try
            {
                adbContext.users.Update(new User { User_Id = id, isActive = 1 });
                adbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Set Disable User
        public void InactivateUser(int id)
        {
            try
            {
                adbContext.users.Update(new User { User_Id = id, isActive = 0 });
                adbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Delete User
        public void Delete(int id)
        {
            try
            {
                adbContext.users.Remove(new User { User_Id = id });
                adbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //User Change Password
        public string ChangePassword(int user_Id, string oldPassword, string newPassword)
        {
            string message = string.Empty;
            try
            {
                User vList = GetUserById(user_Id);
                if (vList != null)
                {
                    if (user_PasswordRepository.ChangePassword(user_Id, oldPassword, newPassword, vList.Password))
                    {
                        vList.Password = user_PasswordRepository.GeneratePassword(newPassword);
                        vList.UpdatedOn = DateTime.Now;
                        vList.UpdatedBy = user_Id;

                        adbContext.users.Update(vList);
                        adbContext.SaveChanges();

                        message = "Saved Successfully";

                        // remove token
                        tokenService.Delete(user_Id);
                    }
                    else
                    {
                        message = "Old Password Does Not Match or Password Already Exist. Please Enter New Password.";
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                throw ex;
            }

            return message;
        }

        //Admin change password of any user
        public void AdminChangePassword(string login_Id, string password)
        {
            try
            {
                var vList = GetUserByLoginId(login_Id);
                vList.Password = user_PasswordRepository.GeneratePassword(password);
                vList.PasswordExpiryDate = DateTime.Now;
                vList.UpdatedOn = DateTime.Now;

                adbContext.users.Update(vList);
                adbContext.SaveChanges();
                user_PasswordRepository.AdminChangePassword(vList.User_Id, password);
                //  remove token
                tokenService.Delete(vList.User_Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Find Paginated 
        public IEnumerable<User> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (!String.IsNullOrEmpty(searchValue))
                    return adbContext.users.Where(w => new[] { Convert.ToString(w.User_Id), w.Login_Id, w.User_Type, w.Email }.Any(a => a.Contains(searchValue))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                else
                    return adbContext.users.Where(W => W.Login_Id.Contains(searchValue)).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User_Response SignIn(User_Request userRequest)
        {
            User_Response objUserResponse = new User_Response();
            try
            {
                var vList = GetUserByLoginId(userRequest.Login_Id);
                if (vList != null && vList.isActive == 1)
                {
                    objUserResponse.isAvailable = true;
                    bool blnVerify = user_PasswordRepository.VerifyPassword(userRequest.Password, vList.Password.ToString());
                    if (blnVerify)
                    {
                        objUserResponse.User_Id = vList.User_Id;
                        objUserResponse.Login_Id = vList.Login_Id;
                        objUserResponse.Company_Id = vList.Company_Id;

                        if (vList.PasswordExpiryDate <= DateTime.Now)   //need one check for temporary password
                            objUserResponse.isTemporary = true;
                        else
                        {
                            objUserResponse.Token_No = tokenService.GenerateToken();
                            tokenService.Add(new User_Token
                            {
                                User_Id = vList.User_Id,
                                Token_No = objUserResponse.Token_No,
                                AddedBy = vList.User_Id
                            });
                            objUserResponse.isTemporary = false;
                            objUserResponse.isVerify = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //create UserLog
                AddUserLog(userRequest.Ip_Address, userRequest.Host_Name, userRequest.User_Id, userRequest.Login_Id);
            }
            return objUserResponse;
        }

        public void SignOut(int id)
        {
            try
            {
                tokenService.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddUserLog(string Ip_Address, string Host_Name, int? User_Id, string Login_Id)
        {
            try
            {
                adbContext.Add<UserLog>(new UserLog { AddedOn = DateTime.Now, Ip_Address = Ip_Address, Host_Name = Host_Name, User_Id = User_Id, User_Name = Login_Id });
                adbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Finalizer with Disposed

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing) adbContext.Dispose();
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true); GC.SuppressFinalize(this);
        }
        #endregion Finalizer with Disposed
    }
}
