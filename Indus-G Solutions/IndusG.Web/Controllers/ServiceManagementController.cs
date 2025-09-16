using IndusG.Models;
using IndusG.Service;
using IndusG.Web.Filters;
using System.Web.Mvc;

namespace IndusG.Web.Controllers
{
    [IndusGAuthorize]
    public class ServiceManagementController : Controller
    {
        [HttpPost]
        [DakHnolUser]
        public ActionResult CheckServiceRunning()
        {
            var plcService = new PlcService();
            var isRunning = plcService.GetCurrentServiceStatus();
            if (!isRunning)
            {
                return Json(new ServiceStatusModel());
            }

            var isPLCAvailable = plcService.GetCurrentPLCStatus();

            return Json(new ServiceStatusModel
            {
                IsServiceRunning = true,
                IsPLCAvailable = isPLCAvailable
            });
        }
    }
}