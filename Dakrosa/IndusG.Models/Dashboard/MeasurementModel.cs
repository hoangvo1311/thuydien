using System;
using System.ComponentModel;

namespace IndusG.Models
{
    public class MeasurementModel
    {
        public System.DateTime Date { get; set; }

        [DisplayName("Mực nước thượng lưu")]
        public Nullable<double> UpstreamWaterLevel_m { get; set; }

        [DisplayName("Mực nước hạ lưu")]
        public Nullable<double> DownstreamWaterLevel_m { get; set; }

        [DisplayName("Tính toán Lưu lượng về hồ")]
        public Nullable<double> Qve_Ho { get; set; }


        [DisplayName("Lưu lượng qua xả tràn")]
        public Nullable<double> Qoverflow { get; set; }

        [DisplayName("Lưu lượng qua nhà máy")]
        public Nullable<double> QcmH1H2H3 { get; set; }

        [DisplayName("Lưu lượng xả tối thiểu")]
        public Nullable<double> Qminflow { get; set; }

        [DisplayName("Lưu lượng về hạ du")]
        public Nullable<double> Qve_HaDu { get; set; }
        public Nullable<double> Drain_Level1 { get; set; }
        public Nullable<double> Drain_Level2 { get; set; }

        [DisplayName("Dự báo lưu lượng về hồ 1h tới")]
        public Nullable<double> Qve_HoDB { get; set; }


        [DisplayName("Dung tích hữu ích")]
        public Nullable<double> Reserve_Water { get; set; }

        public Nullable<double> Qminflow_TT { get; set; }

        [DisplayName("Công suất H1")]
        public Nullable<double> H1_MW { get; set; }

        [DisplayName("Công suất H2")]
        public Nullable<double> H2_MW { get; set; }

        [DisplayName("Công suất H3")]
        public Nullable<double> H3_MW { get; set; }

        [DisplayName("Lưu lượng H1")]
        public Nullable<double> QcmH1 { get; set; }

        [DisplayName("Lưu lượng H2")]
        public Nullable<double> QcmH2 { get; set; }

        [DisplayName("Lưu lượng H3")]
        public Nullable<double> QcmH3 { get; set; }

        public Nullable<double> DeltaQsb { get; set; }
        public Nullable<double> Qve_TT { get; set; }
        public Nullable<double> Qve_TB { get; set; }
    }
}