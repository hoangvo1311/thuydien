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
    public class IAMODashboardController : Controller
    {
        private IAMOMeasurementService _iamoMeasurementService = new IAMOMeasurementService();

        // GET: Dashboard
        [IaMoUser]
        public ActionResult Index()
        {
            var measurement = _iamoMeasurementService.GetLatestMeasurement();
            return View(measurement);
        }

        [IaMoUser]
        public ActionResult GetLatestMeasurement()
        {
            return Json(_iamoMeasurementService.GetLatestMeasurement());
        }
    }
}