using IndusG.Models;
using IndusG.Models.Setting;
using IndusG.Service;
using IndusG.Web.Filters;
using System.Web.Mvc;

namespace IndusG.Web.Controllers
{

    [IndusGAuthorize]
    public class IAMOSettingController : Controller
    {
        private IAMOSettingService _IAMOSettingService = new IAMOSettingService();

        // GET: Setting
        [IaMoUser]
        [ConfigHieuSuatIaMoFilter]
        public ActionResult Index()
        {
            var settingModel = _IAMOSettingService.GetIAMOSettingModel();
            return View(settingModel);
        }

        [HttpPost]
        [ConfigHieuSuatIaMoFilter]
        public ActionResult SaveConfiguration(IAMOConfigurationModel model)
        {
            var result = _IAMOSettingService.SaveIAMO_Configuration(model);
            return Json(result);
        }
    }
}