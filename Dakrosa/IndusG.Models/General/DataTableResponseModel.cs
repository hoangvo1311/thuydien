using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndusG.Models
{
    public class DataTableResponseModel
    {
        public string draw { get; set; }
        public int recordsFiltered { get; set; }    
        public int recordsTotal { get; set; }
        public object data { get; set; }
    }
}
