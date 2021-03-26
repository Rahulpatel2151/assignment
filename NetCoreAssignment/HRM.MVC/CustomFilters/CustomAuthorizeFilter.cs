using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.MVC.CustomFilters
{
    public class CustomAuthorizeFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string isAuthenticated = filterContext.HttpContext.Session.GetString("isAuthenticated");
            if (isAuthenticated==null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary{{ "controller", "Admin" },
                                          { "action", "Login" }

                                         });
            }
            
            base.OnActionExecuting(filterContext);
        }
    }
}
