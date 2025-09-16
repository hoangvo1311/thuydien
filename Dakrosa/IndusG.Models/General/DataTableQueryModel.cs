using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndusG.Models
{
    public class DataTableQueryModel
    {
        public string Draw { get; set; }
        public string SortColumn { get; set; }
        public string SortColumnDir { get; set; }
        public string SearchValue { get; set; }
        public int Skip { get; set; }
        public int PageSize { get; set; }
    }
}
