using IndusG.Models;
using IndusG.Service;
using System;
using System.Web.Mvc;

namespace IndusG.Web.Controllers
{
    public class ServiceManagementController : Controller
    {
        [HttpPost]
        public ActionResult CheckServiceRunning()
        {
            var plcService = new PlcService();
            var isRunning = plcService.GetCurrentServiceStatus();

            var monitoringModel = plcService.GetPLCMonitoringModel();
            var latestMeasure = (new MeasurementService()).GetLatestMeasurement();
            return Json(new ServiceStatusModel
            {
                IsServiceRunning = true,
                IsPLCAvailable = monitoringModel.PLCLiveBit,
                PLCMonitoring = monitoringModel,
                Qminflow = Math.Round(latestMeasure?.Qminflow ?? 0, 2) 
            });
        }
    }
}