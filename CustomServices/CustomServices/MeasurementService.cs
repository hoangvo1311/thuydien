using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using CustomServices.Model;

namespace CustomServices
{
    public class MeasurementService : CustomDBObject
    {
        public MeasurementService(string connectionString)
             : base(connectionString)
        {
        }

        public Measurement GetLatestMeasurementValue(int plantID)
        {
            DataSet dataSet = new DataSet();
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
              {
                new SqlParameter("@PlantID", SqlDbType.Int)
              };
            sqlParameterArray[0].Value = (object)plantID;
            DataTable measurements = this.RunProcedure("sp_Measurement_GetLatestMeasurement", 
                (IDataParameter[])sqlParameterArray, "Measurement").Tables["Measurement"];

            if (measurements.Rows.Count > 0)
            {
                return new Measurement
                {
                    Date = (Convert.ToDateTime(measurements.Rows[0]["Date"].ToString())).ToString("HH:mm dd/MM/yyyy"),
                    DownstreamWaterLevel_m = measurements.Rows[0]["UpstreamWaterLevel_m"] != null ? (Convert.ToDouble(measurements.Rows[0]["DownstreamWaterLevel_m"].ToString())).ToString() : string.Empty,
                    UpstreamWaterLevel_m = measurements.Rows[0]["UpstreamWaterLevel_m"] != null ? (Convert.ToDouble(measurements.Rows[0]["UpstreamWaterLevel_m"].ToString())).ToString() : string.Empty,
                    QcmH1 = measurements.Rows[0]["QcmH1"] != null ? (Convert.ToDouble(measurements.Rows[0]["QcmH1"].ToString())).ToString()  : string.Empty,
                    QcmH2 = measurements.Rows[0]["QcmH2"] != null ? (Convert.ToDouble(measurements.Rows[0]["QcmH2"].ToString())).ToString()  : string.Empty,
                    QcmH3 = measurements.Rows[0]["QcmH3"] != null ? (Convert.ToDouble(measurements.Rows[0]["QcmH3"].ToString())).ToString()  : string.Empty,
                    H1_MW = measurements.Rows[0]["H1_MW"] != null ? (Convert.ToDouble(measurements.Rows[0]["H1_MW"].ToString())).ToString()  : string.Empty,
                    H2_MW = measurements.Rows[0]["H2_MW"] != null ? (Convert.ToDouble(measurements.Rows[0]["H2_MW"].ToString())).ToString()  : string.Empty,
                    H3_MW = measurements.Rows[0]["H3_MW"] != null ? (Convert.ToDouble(measurements.Rows[0]["H3_MW"].ToString())).ToString()  : string.Empty,
                    Qoverflow = measurements.Rows[0]["Qoverflow"] != null ? (Convert.ToDouble(measurements.Rows[0]["Qoverflow"].ToString())).ToString() : string.Empty,
                    QminFlow = measurements.Rows[0]["QminFlow"] != null ? (Convert.ToDouble(measurements.Rows[0]["QminFlow"].ToString())).ToString() : string.Empty,
                    LuongMua = measurements.Rows[0]["Luongmua"] != null ? (Convert.ToDouble(measurements.Rows[0]["Luongmua"].ToString())).ToString() : string.Empty,
                    LuuLuongHo = measurements.Rows[0]["Qve_Ho"] != null ? (Convert.ToDouble(measurements.Rows[0]["Qve_Ho"].ToString())).ToString() : string.Empty,
                    Qve_HoDB = measurements.Rows[0]["Qve_HoDB"] != null ? (Convert.ToDouble(measurements.Rows[0]["Qve_HoDB"].ToString())).ToString() : string.Empty,
                };
            }
            return null;
        }
    }
}
