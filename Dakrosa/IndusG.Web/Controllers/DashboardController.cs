using IndusG.Service;
using IndusG.Web.Filters;
using System.Web.Mvc;

namespace IndusG.Web.Controllers
{
    [IndusGAuthorize]
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
        public ActionResult GetMeasurementChartData(int selectedHour)
        {
            var measurementService = new MeasurementService();
            var chartData = measurementService.GetMeasurementChartData(selectedHour);
            return Json(chartData);
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