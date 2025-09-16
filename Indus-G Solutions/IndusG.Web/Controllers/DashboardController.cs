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
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            //if (!WorkContext.IsDakhnolUser())
            //{
            //    return RedirectToAction("Index", "IAMODashboard");
            //}
            var measurementService = new MeasurementService();
            var measurement = measurementService.GetLatestMeasurement();
            return View(measurement);
        }

        [HttpPost]
        [DakHnolUser]
        public ActionResult GetMeasurementChartData(int selectedHour)
        {
            var measurementService = new MeasurementService();
            var chartData = measurementService.GetMeasurementChartData(selectedHour);
            return Json(chartData);
        }

        [HttpPost]
        [DakHnolUser]
        public ActionResult GetLatestMeasurement()
        {
            var measurementService = new MeasurementService();
            var data = measurementService.GetLatestMeasurement();
            return Json(data);
        }
    }
}