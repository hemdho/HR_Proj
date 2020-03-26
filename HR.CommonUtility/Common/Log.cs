using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HR.CommonUtility.Common
{
    static public class Log
    {
        static public string AddLog(string methodName, RouteData routeData)
        {
            var vControllerName = routeData.Values["controller"];
            var vActionName = routeData.Values["action"];
            var vMessage = String.Format("{0} controller:{1} action:{2}", methodName, vControllerName, vActionName);
            return vMessage;
        }
    }
}
