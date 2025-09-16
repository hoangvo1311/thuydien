using IndusG.Models;
using IndusG.Models.Setting;
using IndusG.Service;
using IndusG.Web.Filters;
using System.Web.Mvc;

namespace IndusG.Web.Controllers
{

    [IndusGAuthorize]

    public class SettingController : Controller
    {
        private SettingService settingService = new SettingService();
        // GET: Setting
        [DakHnolUser]
        public ActionResult Index()
        {
            var settingModel = settingService.GetSettingModel();
            return View(settingModel);
        }

        [HttpPost]
        [DakHnolUser]
        public ActionResult TestConnection(PLCSettingModel model)
        {
            var plcService = new PlcService();
            var result = plcService.TestConnection(model);
            return Json(result);
        }

        [HttpPost]
        [AdminUser]
        public ActionResult SavePLCConnection(PLCSettingModel model)
        {
            var result = settingService.SaveConnection(model);
            return Json(result);
        }

        [HttpPost]
        [DakHnolUser]
        public ActionResult SaveParameters(SettingModel model)
        {
            var result = settingService.SaveParameters(model);
            return Json(result);
        }
    }
}