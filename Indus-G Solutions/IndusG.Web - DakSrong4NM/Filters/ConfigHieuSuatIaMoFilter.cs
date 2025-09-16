using IndusG.Service;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace IndusG.Web.Filters
{
    public class ConfigHieuSuatIaMoFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (!WorkContext.CanConfigHieuSuatIamo())
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