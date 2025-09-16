using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndusG.Models
{
    public class MeasurementChartModel
    {
        public string Date { get; set; }
        public double Qve_HaDu { get; set; }
        public double Qve_Ho { get; set; }
        public double H1_MW { get; set; }
        public double H2_MW { get; set; }
        public double H3_MW { get; set; }
        public double UpstreamWaterLevel_m { get; set; }
        public double DownstreamWaterLevel_m { get; set; }
        public double Qoverflow { get; set; }
        public double QcmH1H2H3 { get; set; }
        public double Qminflow { get; set; }
    }
}
