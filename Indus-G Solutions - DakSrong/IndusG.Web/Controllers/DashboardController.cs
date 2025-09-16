using IndusG.Service;
using IndusG.Web.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IndusG.Web.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            var measurementService = new MeasurementService();
            var measurement = measurementService.GetLatestMeasurement();

            return View(measurement);
        }

        [HttpPost]
        public ActionResult GetMeasurementChartData(int selectedHour, string selectedDate)
        {
            var measurementService = new MeasurementService();
            var chartData = measurementService.GetMeasurementChartData(selectedHour, selectedDate);
            return Json(chartData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetLatestMeasurement()
        {
            var measurementService = new MeasurementService();
            var data = measurementService.GetLatestMeasurement();
            return Json(data);
        }
    }
}