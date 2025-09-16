using System;
using System.ComponentModel;

namespace IndusG.Models
{
    public class IAMO_MeasurementModel
    {
        [DisplayName("Công suất tác dụng H1")]
        public Nullable<decimal> H1_ActivePower { get; set; }
        [DisplayName("Công suất tác dụng H2")]
        public Nullable<decimal> H2_ActivePower { get; set; }
        [DisplayName("Công suất phản kháng H1")]
        public Nullable<decimal> H1_ReactivePower { get; set; }
        [DisplayName("Công suất phản kháng H2")]
        public Nullable<decimal> H2_ReactivePower { get; set; }
        [DisplayName("Lưu lượng qua H1")]
        public Nullable<decimal> QcmH1 { get; set; }
        [DisplayName("Lưu lượng qua H2")]
        public Nullable<decimal> QcmH2 { get; set; }
        [DisplayName("Lưu lượng qua hạ lưu nhà máy Ia Mơr")]
        public Nullable<decimal> QcmH1H2
        {
            get
            {
                return QcmH1 + QcmH2;
            }
        }
        [DisplayName("Cột áp nhà máy")]
        public Nullable<decimal> CotAp { get; set; }

        public Nullable<System.DateTime> Date { get; set; }
    }
}