using IndusG.Models.User;
using IndusG.Service;
using System.Web.Mvc;

namespace IndusG.Web.Controllers
{
    public class UserController : Controller
    {
        [HttpPost]
        public ActionResult Login(UserLoginModel model)
        {
            var userService = new UserService();
            var loginResult = userService.Login(model);
            return Json(loginResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            var userService = new UserService();
            return Json(userService.Logout());
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}