using HR.WebApi.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HR.WebApi.ActionFilters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class TokenVerify : ActionFilterAttribute
    {
        private readonly ApplicationDbContext adbContext;

        public TokenVerify()
        {
            adbContext = Startup.applicationDbContext;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var vHeaders = filterContext.HttpContext.Request.Headers;

            //if (String.IsNullOrEmpty(vHeaders["LOGIN_ID"]) && String.IsNullOrEmpty(Convert.ToString(vHeaders["TOKEN_NO"])))
            //{
            //    filterContext.Result = new UnauthorizedResult(); return;
            //}
            //else
            //{
            //    if (!TokenCheck(Convert.ToString(vHeaders["LOGIN_ID"]), Convert.ToString(vHeaders["TOKEN_NO"])))
            //        filterContext.Result = new UnauthorizedResult(); return;
            //}
        }

        private bool TokenCheck(string strLoginId, string strTokenNo)
        {
            var vCount = (from t in adbContext.user_token
                          join u in adbContext.users on t.User_Id equals u.User_Id
                          where u.Login_Id == strLoginId && u.isActive == 1 && t.isActive == 1 && t.Token_No == strTokenNo && t.Token_ExpiryDate > DateTime.Now
                          select new { t.Token_No }).Count();
            // && CommonUtility.TokenManager.ValidateToken(vList.Token_No) == strUserCode
            return (vCount > 0 ? true : false);
        }
    }
}
