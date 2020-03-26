using log4net;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics;
using System.Reflection;
//using log4net;

namespace HR.WebApi.ActionFilters
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Log : ActionFilterAttribute, IExceptionFilter
    {
        private static readonly ILog Logfile = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Trace.WriteLine(CommonUtility.Common.Log.AddLog("OnActionExecuting", filterContext.RouteData));
            Logfile.Info(CommonUtility.Common.Log.AddLog("OnActionExecuting", filterContext.RouteData));
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Trace.TraceWarning(CommonUtility.Common.Log.AddLog("OnActionExecuted", filterContext.RouteData));
            Logfile.Warn(CommonUtility.Common.Log.AddLog("OnActionExecuted", filterContext.RouteData));
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //Trace.TraceInformation(CommonUtility.Common.Log.AddLog("OnResultExecuting", filterContext.RouteData));
            Logfile.Info(CommonUtility.Common.Log.AddLog("OnResultExecuted", filterContext.RouteData));
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //Trace.TraceWarning(CommonUtility.Common.Log.AddLog("OnResultExecuted", filterContext.RouteData));
            Logfile.Warn(CommonUtility.Common.Log.AddLog("OnResultExecuted", filterContext.RouteData));
        }

        public void OnException(ExceptionContext exceptionContext)
        {
            //Trace.TraceError(CommonUtility.Common.Log.AddLog("OnException", exceptionContext.RouteData));
            Logfile.Error(CommonUtility.Common.Log.AddLog("OnException", exceptionContext.RouteData));
        }
    }
}