using IndusG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IndusG.Service
{
    public class DataTableHelper
    {
        public static DataTableQueryModel GetQueryModel()
        {
            var request = HttpContext.Current.Request;

            var draw = request.Form.GetValues("draw").FirstOrDefault();
            var start = request.Form.GetValues("start").FirstOrDefault();
            var length = request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = request.Form.GetValues("columns[" + request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = request.Form.GetValues("search[value]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            return new DataTableQueryModel
            {
                Draw = draw,
                SortColumn = sortColumn,
                SortColumnDir = sortColumnDir,
                SearchValue = searchValue,
                PageSize = pageSize,
                Skip = skip
            };
        }
    }
}
