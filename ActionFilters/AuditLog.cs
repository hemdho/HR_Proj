using HR.WebApi.DAL;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR.WebApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System.Net;
using System.Diagnostics;
using log4net;
using System.Reflection;

namespace HR.WebApi.ActionFilters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuditLog : ActionFilterAttribute
    {
        private static readonly ILog Logfile = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ApplicationDbContext adbContext;

        public AuditLog()
        {
            adbContext = Startup.applicationDbContext;
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //add into event log            
            string strRemoteIp = Convert.ToString(filterContext.HttpContext.Connection.LocalIpAddress);
            AddAuditLog(filterContext.RouteData, strRemoteIp, filterContext);
        }

        public void AddAuditLog(RouteData routeData, string strRemoteIp, ResultExecutedContext resultState)
        {
            try
            {
                Model.AuditLog lstLog = new Model.AuditLog();
                lstLog.User_Id = Convert.ToInt16(routeData.Values["id"]);
                lstLog.Description = "Process : " + routeData.Values["controller"];
                lstLog.Type = "Id : " + resultState.Result;
                lstLog.HostName = Dns.GetHostName();
                lstLog.IpAddress = strRemoteIp;
                lstLog.Status = ActionStat(Convert.ToString(routeData.Values["action"]));
                lstLog.AddedBy = Convert.ToInt16(routeData.Values["id"]);
                lstLog.AddedOn = DateTime.Now;

                adbContext.auditlog.Add(lstLog);
                adbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //Write Log manually in text file
                Logfile.Error("Error on AuditLog", ex);
            }
        }

        public string ActionStat(string actionName)
        {
            switch (actionName)
            {
                case "Get":
                    return "View";
                case "Post":
                    return "Add";
                case "Put":
                    return "Edit";
                case "Delete":
                    return "Delete";
                default:
                    return actionName;
            }
        }
    }
}