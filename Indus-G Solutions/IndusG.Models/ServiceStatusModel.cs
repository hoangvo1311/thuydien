using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndusG.Models
{
    public class ServiceStatusModel
    {
        public bool IsServiceRunning { get; set; }
        public bool IsPLCAvailable { get; set; }
    }
}
