using IndusG.BackgroundServiceImplement.Service;
using IndusG.Models;
using IndusG.Models.Setting;
using IndusG.Service;
using IndusG.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IndusG.Web.Controllers
{
    [IndusGAuthorize]
    public class ServiceManagementController : Controller
    {
        [HttpPost]
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