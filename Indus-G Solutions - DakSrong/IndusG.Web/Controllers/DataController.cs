using IndusG.Models;
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
    public class DataController : Controller
    {
        // GET: Data
        [DakHnolUser]
        public ActionResult Index()
        {
            return View();
        }

        [DakHnolUser]
        public ActionResult Load(string fromDate, string toDate)
        {
            try
            {
                var dataService = new DataService();
                var queryModel = DataTableHelper.GetQueryModel();
                var dataTableModel = dataService.LoadData(queryModel, fromDate, toDate);
                var jsonResult = Json(dataTableModel, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                LoggerHelper.Error($"{ex.Message} \n {ex.StackTrace}");
                throw;
            }

        }

        [DakHnolUser]
        public ActionResult Export()
        {
            var dataService = new DataService();
            var fileStream = dataService.Export();
            return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SoLieuQuanTrac.xlsx");
        }
    }
}