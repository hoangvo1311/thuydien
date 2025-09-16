using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomServices.Model
{
    public class Measurement
    {
        public string Date { get; set; }
        public string UpstreamWaterLevel_m { get; set; }
        public string DownstreamWaterLevel_m { get; set; }
        public string QcmH1 { get; set; }
        public string QcmH2 { get; set; }
        public string QcmH3 { get; set; }
        public string H1_MW { get; set; }
        public string H2_MW { get; set; }
        public string H3_MW { get; set; }
        public string Qoverflow { get; set; }
        public string QminFlow { get; set; }
        public string LuongMua { get; set; }
        public string LuuLuongHo { get; set; }
        public string Qve_HoDB { get; set; }
    }
}
