using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HR.WebApi.ActionFilters
{
    public class CustomActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // before the action executes  
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // after the action executes  
        }
    }
}
