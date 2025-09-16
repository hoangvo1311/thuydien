using System;

namespace IndusG.Models
{
    public class DataModel
    {
        public System.DateTime Date { get; set; }
        public string DateString
        {
            get
            {
                return Date.ToString("dd/MM/yyyy");
            }
        }

        public TimeSpan Time
        {
            get
            {
                return new TimeSpan(0, Date.Hour, Date.Minute, Date.Second);
            }
        }

        public string TimeString
        {
            get
            {
                return Date.ToString("HH:mm");
            }
        }

        //public string TimeString
        //{
        //    get
        //    {
        //        return Time.ToString("dd/MM/yyyy");
        //    }
        //}
        public Nullable<double> UpstreamWaterLevel_Cal { get; set; }
        public Nullable<double> DownstreamWaterLevel_m { get; set; }
        public Nullable<double> Qve_Ho { get; set; }
        public Nullable<double> Qoverflow { get; set; }
        public Nullable<double> QcmH1H2H3 { get; set; }
        public Nullable<double> Qminflow { get; set; }
        public Nullable<double> Qve_Hadu { get; set; }
        public Nullable<double> Qve_HoDB { get; set; }
        public Nullable<double> Reserve_Water { get; set; }
    }
}
