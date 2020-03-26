using HR.CommonUtility;
using HR.WebApi.DAL;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using System;
using System.Linq;

namespace HR.WebApi.Common
{
    public class TokenService : ITokenService
    {
        private readonly ApplicationDbContext adbContext;

        public TokenService()
        {
            adbContext = Startup.applicationDbContext;
        }

        public void Add(User_Token entity)
        {
            //adbContext.BeginTransaction();
            try
            {
                string strToken = (entity.Token_No == String.Empty ? GenerateToken() : entity.Token_No);

                var vToken = adbContext.user_token.Where(w => w.User_Id == entity.User_Id).ToList();
                if (vToken.Count() > 0)
                {
                    entity.Token_No = strToken;
                    entity.Token_ExpiryDate = DateTime.Now.AddMinutes(30);
                    entity.isActive = 1;
                    entity.UpdatedOn = DateTime.Now;
                    entity.UpdatedBy = entity.UpdatedBy;
                    adbContext.user_token.Update(entity);
                }
                else
                {
                    adbContext.Add<User_Token>(new User_Token
                    {
                        Token_ExpiryDate = DateTime.Now.AddMinutes(30),
                        isActive = 1,
                        User_Id = entity.User_Id,
                        Token_No = strToken,
                        AddedBy = entity.User_Id,
                        AddedOn = DateTime.Now
                    });
                }
                adbContext.SaveChanges();
                //adbContext.CommitTransaction();
            }
            catch (Exception ex)
            {
                //adbContext.RollBackTransaction();
                throw ex;
            }
        }

        public void Remove(User_Token entity)
        {
            //adbContext.BeginTransaction();
            try
            {
                adbContext.Update<User_Token>(new User_Token
                {
                    Token_ExpiryDate = DateTime.Now.AddMinutes(-30),
                    isActive = 0,
                    User_Id = entity.User_Id,
                    Token_No = String.Empty,
                    UpdatedBy = entity.User_Id,
                    UpdatedOn = DateTime.Now
                });
                adbContext.SaveChanges();
                //adbContext.CommitTransaction();
            }
            catch (Exception ex)
            {
                //adbContext.RollBackTransaction();
                throw ex;
            }
        }

        public void Delete(int id)
        {
            try
            {
                adbContext.Update<User_Token>(new User_Token
                {
                    Token_ExpiryDate = DateTime.Now.AddMinutes(-30),
                    isActive = 0,
                    User_Id = id,
                    Token_No = String.Empty,
                    UpdatedBy = id,
                    UpdatedOn = DateTime.Now
                });
                adbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GenerateToken()
        {
            string strToken = string.Empty;
            try
            {
                strToken = TokenManager.GenerateToken();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strToken;
        }

        public bool Verify(int userId = 0, string strTokenNo = "Default")
        {
            int intCount = 0;
            try
            {
                intCount = adbContext.user_token.Where(w => w.User_Id == userId && w.Token_No == strTokenNo && w.Token_ExpiryDate < DateTime.Now).Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (intCount > 0 ? true : false);
        }

        public bool ValidateByUser(int userId)
        {
            int intCount = 0;
            try
            {
                intCount = adbContext.user_token.Where(w => w.User_Id == userId && w.Token_ExpiryDate < DateTime.Now).Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (intCount > 0 ? true : false);
        }

        public string GetTokenByUserId(int userId)
        {
            string strTokenNo = String.Empty;
            try
            {
                strTokenNo = adbContext.user_token.Where(w => w.User_Id == userId).Select(s => s.Token_No).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strTokenNo;
        }
    }
}
