using IndusG.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace IndusG.Web.Filters
{
    public class DakHnolUserAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (WorkContext.CurrentUser == null || 
                (!WorkContext.IsAdmin() && !WorkContext.IsOperator()
                //&& WorkContext.CurrentUser.UserType != Models.Enums.UserType.Viewer
                ))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                
            }
        }
    }
}