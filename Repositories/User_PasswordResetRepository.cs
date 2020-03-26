using HR.WebApi.Common;
using HR.WebApi.DAL;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using System;
using System.Linq;

namespace HR.WebApi.Repositories
{
    public class User_PasswordResetRepository : IUser_PasswordReset
    {
        private string EmailDefaultPassword = AppSettings.EmailDefaultPassword;
        private int PasswordExpiryTime = AppSettings.PasswordExpiryTime;
        private int PasswordExpiryDays = AppSettings.PasswordExpiryDays;
        private string PasswordResetLink = AppSettings.PasswordResetLink;

        private TokenService objTokenService;
        private User_PasswordRepository objUser_PasswordRepo;
        private UserService<User> objUserService;

        private readonly ApplicationDbContext adbContext;
        public User_PasswordResetRepository(TokenService tokenService, User_PasswordRepository user_PasswordRepository, UserService<User> userService)
        {
            adbContext = Startup.applicationDbContext;
            objTokenService = tokenService;
            objUser_PasswordRepo = user_PasswordRepository;
            objUserService = userService;
        }

        public void Insert(int userId, string Link)
        {
            try
            {
                var vList = new User_PasswordReset
                {
                    User_Id = userId,
                    PasswordReset_ExpiryDate = DateTime.Now.AddDays(PasswordExpiryDays),
                    PasswordReset_Link = Link,
                    PasswordReset_Status = "Active",
                    AddedBy = userId,
                    AddedOn = DateTime.Now
                };
                adbContext.user_passwordreset.Add(vList);
                adbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //User Forgot Password - loginId
        public void ForgotPassword(string loginId)
        {
            try
            {
                var vList = objUserService.GetUserByLoginId(loginId);
                if (vList != null)
                {
                    vList.Password = objUser_PasswordRepo.GeneratePassword(EmailDefaultPassword);
                    vList.PasswordExpiryDate = DateTime.Now.AddDays(PasswordExpiryDays);
                    vList.UpdatedBy = vList.User_Id;
                    vList.UpdatedOn = DateTime.Now;

                    adbContext.users.Update(vList);
                    adbContext.SaveChanges();

                    PasswordResetLink += EncryptLink(vList.Login_Id);
                    Insert(vList.User_Id, PasswordResetLink);

                    //  remove and generate new token
                    objTokenService.Add(new User_Token { User_Id = vList.User_Id });

                    #region Sent link to User 

                    Common.Email vEmailConfiguration = new Common.Email();
                    var usermodel = vEmailConfiguration.GetEmailBody(vList.Email, "mehul.parmar@bnscolorama.co.uk", "", "Reset Password Link", "Please find below link for your reset password  " + PasswordResetLink);
                    vEmailConfiguration.SendEmail(usermodel, vEmailConfiguration.GetEmailConfiguration(vList.Company_Id));

                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string EncryptLink(string loginId)
        {
            return CommonUtility.Crypt.EncryptString(loginId);
        }

        public bool VerifyLink(string link)
        {
            string strSplitLoginId = link.Replace(PasswordResetLink, String.Empty).Trim();
            var vCount = (from pr in adbContext.user_passwordreset
                          join u in adbContext.users on pr.User_Id equals u.User_Id
                          where u.Login_Id == strSplitLoginId && u.isActive == 1
                            && pr.PasswordReset_Link == link
                            && pr.PasswordReset_ExpiryDate > DateTime.Now
                          select new { u.Login_Id }).Count();
            return (vCount > 0 ? true : false);
        }

        public bool ResetPassword(string loginId, string password)
        {
            try
            {
                var vList = objUserService.GetUserByLoginId(loginId);
                if (vList != null)
                {
                    //verfiy last passwords
                    if (objUser_PasswordRepo.IsPasswordExist(vList.User_Id, password))
                        return false;

                    vList.Password = objUser_PasswordRepo.GeneratePassword(password);
                    vList.PasswordExpiryDate = DateTime.Now.AddDays(PasswordExpiryDays);
                    vList.UpdatedBy = vList.User_Id;
                    vList.UpdatedOn = DateTime.Now;

                    adbContext.users.Update(vList);
                    adbContext.SaveChanges();

                    //remove token
                    objTokenService.Delete(vList.User_Id);

                    //remove link
                    adbContext.user_passwordreset.Remove(new User_PasswordReset { User_Id = vList.User_Id });
                    adbContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
    }
}
