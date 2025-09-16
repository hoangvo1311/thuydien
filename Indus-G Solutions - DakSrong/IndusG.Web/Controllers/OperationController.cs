using IndusG.Service;
using IndusG.Web.Filters;
using System;
using System.Web.Mvc;

namespace IndusG.Web.Controllers
{

    [IndusGAuthorize]

    public class OperationController : Controller
    {
        private OperationService operationService = new OperationService();
        // GET: Setting
        [DakHnolUser]
        public ActionResult Index()
        {
            var settingModel = operationService.GetOperationModel();
            return View(settingModel);
        }

        [HttpPost]
        [DakHnolUser]
        public ActionResult SaveDrainLevel1(double drainLevel)
        {
            var result = operationService.SaveDrainLevel1(drainLevel);
            return Json(result);
        }


        [HttpPost]
        [DakHnolUser]
        public ActionResult SaveDrainLevel2(double drainLevel)
        {
            var result = operationService.SaveDrainLevel2(drainLevel);
            return Json(result);
        }

        [HttpPost]
        [DakHnolUser]
        public ActionResult SaveRemoteMode(string mode)
        {
            var result = operationService.SaveRemoteMode(mode);
            return Json(result);
        }

        [HttpPost]
        [DakHnolUser]
        public ActionResult SaveSimulationBit(bool simulationBit)
        {
            var result = operationService.SaveSimulationBit(simulationBit);
            return Json(result);
        }

        [HttpPost]
        [DakHnolUser]
        public ActionResult SaveKUKDState(string kc, bool state)
        {
            object result;

            try
            {
                switch (kc.ToUpper())
                {
                    case "KC1":
                        result = operationService.SaveKU1(state);
                        break;

                    case "KC2":
                        result = operationService.SaveKD1(state);
                        break;

                    case "KC3":
                        result = operationService.SaveKU2(state);
                        break;

                    case "KC4":
                        result = operationService.SaveKD2(state);
                        break;

                    default:
                        return Json(new { success = false, message = "Invalid KC code" });
                }

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [DakHnolUser]
        public ActionResult SaveKU1(bool ku1)
        {
            var result = operationService.SaveKU1(ku1);
            return Json(result);
        }


        [HttpPost]
        [DakHnolUser]
        public ActionResult SaveKD1(bool kd1)
        {
            var result = operationService.SaveKD1(kd1);
            return Json(result);
        }

        [HttpPost]
        [DakHnolUser]
        public ActionResult SaveKU2(bool ku2)
        {
            var result = operationService.SaveKU2(ku2);
            return Json(result);
        }

        [HttpPost]
        [DakHnolUser]
        public ActionResult SaveKD2(bool kd2)
        {
            var result = operationService.SaveKD2(kd2);
            return Json(result);
        }

        [HttpPost]
        [DakHnolUser]
        public ActionResult SetK1Up()
        {
            var result = operationService.SetK1Up();
            return Json(result);
        }

        [HttpPost]
        [DakHnolUser]
        public ActionResult SetK1Down()
        {
            var result = operationService.SetK1Down();
            return Json(result);
        }

        [HttpPost]
        [DakHnolUser]
        public ActionResult SetK2Up()
        {
            var result = operationService.SetK2Up();
            return Json(result);
        }

        [HttpPost]
        [DakHnolUser]
        public ActionResult SetK2Down()
        {
            var result = operationService.SetK2Down();
            return Json(result);
        }
    }
}