using IndusG.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace IndusG.Web.Filters
{
    public class IndusGAuthorizeAttribute : AuthorizeAttribute
    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return WorkContext.CurrentUser == null ? false : true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            RedirectToRouteResult routeData = null;

            if (WorkContext.CurrentUser == null)
            {
                routeData = new RedirectToRouteResult
                    (new System.Web.Routing.RouteValueDictionary
                    (new
                    {
                        controller = "User",
                        action = "Login",
                    }
                    ));
            }

            filterContext.Result = routeData;
        }

    }
}