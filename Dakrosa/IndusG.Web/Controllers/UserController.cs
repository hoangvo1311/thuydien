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
            if (loginResult.Result)
                return RedirectToAction("Index", "Dashboard");
            return View();
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