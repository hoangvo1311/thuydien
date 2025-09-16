using IndusG.Models;
using IndusG.Service;
using IndusG.Web.Filters;
using System.Web.Mvc;

namespace IndusG.Web.Controllers
{
    [IndusGAuthorize]
    public class DashboardController : Controller
    {
        private bool IsUser()
        {
            return WorkContext.CurrentUser != null && (WorkContext.CurrentUser.UserType == Enums.UserType.Admin
                || WorkContext.CurrentUser.UserType == Enums.UserType.Operator);
        }
        // GET: Dashboard
        public ActionResult Index()
        {
            if (!IsUser())
            {
                return RedirectToAction("Index", "IAMODashboard");
            }
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
            return Json(chartData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [DakHnolUser]
        public ActionResult GetLatestMeasurement()
        {
            var measurementService = new MeasurementService();
            var data = measurementService.GetLatestMeasurement();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}