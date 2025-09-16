using IndusG.DataAccess;
using IndusG.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IndusG.Service
{
    public class MeasurementService
    {
        public MeasurementModel GetLatestMeasurement()
        {
            using (var context = new DakSrong4NMEntities())
            {
                var measurement = context.DB_SesanMeasurement.FirstOrDefault();
                if (measurement == null)
                    return new MeasurementModel();
                return MapperHelper.Mapper.Map<DB_SesanMeasurement, MeasurementModel>(measurement);
            }

        }

        public void InsertMeasurement(DB_Sesan_2PLC measurement)
        {
            using (var context = new DaksrongDBEntities())
            {
                context.DB_Sesan_2PLC.Add(measurement);
                context.SaveChanges();
            }
        }

        public void InsertMeasurement_4A(Measurement measurement)
        {
            using (var context = new NM4ADBEntities())
            {
                context.Measurements.Add(measurement);
                context.SaveChanges();
            }
        }

        public List<MeasurementChartModel> GetMeasurementChartData(int selectedHour)
        {
            var measurementChartModelList = new List<MeasurementChartModel>();
            using (var context = new DakSrong4NMEntities())
            {
                var now = System.DateTime.Now;
                var lastDate = now.Date + new TimeSpan(now.Hour, now.Minute, 0);

                if (lastDate.Minute >= 45)
                {
                    lastDate = lastDate.AddMinutes(45 - lastDate.Minute);
                }
                else if (lastDate.Minute >= 30)
                {
                    lastDate = lastDate.AddMinutes(30 - lastDate.Minute);
                }
                else if (lastDate.Minute >= 15)
                {
                    lastDate = lastDate.AddMinutes(15 - lastDate.Minute);
                }
                else
                {
                    lastDate = lastDate.AddMinutes(0 - lastDate.Minute);
                }

                var measurements = context.DB_Sesan.OrderBy(x => x.Date);
                var selectedMinute = selectedHour * 4;
                for (var indexMinute = selectedMinute; indexMinute >= 0; indexMinute--)
                {
                    var date = lastDate.AddMinutes(-indexMinute * 15);
                    var end = date.AddMinutes(15);
                    var measurement = measurements.FirstOrDefault(x => x.Date >= date && x.Date < end);
                    if (measurement != null)
                    {
                        measurementChartModelList.Add(new MeasurementChartModel
                        {
                            Date = measurement.Date.ToString("HH:mm dd/MM/yyyy"),
                            DownstreamWaterLevel_m = measurement.DownstreamWaterLevel_m.HasValue ? measurement.DownstreamWaterLevel_m.Value : 0,
                            H1_MW = measurement.H1_MW.HasValue ? measurement.H1_MW.Value : 0,
                            H2_MW = measurement.H2_MW.Value,
                            H3_MW = measurement.H3_MW.Value,
                            QcmH1H2H3 = measurement.QcmH1H2H3.Value,
                            Qminflow = measurement.Qminflow.Value,
                            Qoverflow = measurement.Qoverflow.Value,
                            Qve_HaDu = measurement.Qve_Hadu.Value,
                            Qve_Ho = measurement.Qve_Ho.Value,
                            UpstreamWaterLevel_m = measurement.UpstreamWaterLevel_m.Value
                        });
                    }
                    else
                    {
                        measurementChartModelList.Add(new MeasurementChartModel
                        {
                            Date = date.ToString("HH:mm dd/MM/yyyy")
                        });
                    }
                }

            }
            return measurementChartModelList;
        }

        public void UpdateLatestMeasurement(DB_SesanMeasurement measurement)
        {
            try
            {
                using (var context = new DakSrong4NMEntities())
                {
                    var latestMeasurement = context.DB_SesanMeasurement.FirstOrDefault();
                    if (latestMeasurement == null)
                    {
                        context.DB_SesanMeasurement.Add(measurement);
                    }
                    else
                    {
                        latestMeasurement.Date = measurement.Date;
                        latestMeasurement.UpstreamWaterLevel_m = measurement.UpstreamWaterLevel_m;
                        latestMeasurement.DownstreamWaterLevel_m = measurement.DownstreamWaterLevel_m;
                        latestMeasurement.DeltaQsb = measurement.DeltaQsb;
                        latestMeasurement.H1_MW = measurement.H1_MW;
                        latestMeasurement.H2_MW = measurement.H2_MW;
                        latestMeasurement.H3_MW = measurement.H3_MW;
                        latestMeasurement.QcmH1 = measurement.QcmH1;
                        latestMeasurement.QcmH2 = measurement.QcmH2;
                        latestMeasurement.QcmH3 = measurement.QcmH3;
                        latestMeasurement.QcmH1H2H3 = measurement.QcmH1H2H3;
                        //latestMeasurement.Drain_Level = measurement.Drain_Level;
                        //latestMeasurement.Luongmua = measurement.Luongmua;
                        latestMeasurement.Qve_Hadu = measurement.Qve_Hadu;
                        latestMeasurement.Qve_HoDB = measurement.Qve_HoDB;
                        latestMeasurement.Qve_Ho = measurement.Qve_Ho;
                        latestMeasurement.Qoverflow = measurement.Qoverflow;
                        latestMeasurement.Qminflow = measurement.Qminflow;
                        latestMeasurement.Reserve_Water = measurement.Reserve_Water;
                    }
                    context.SaveChanges();

                }
            } catch (Exception ex)
            {
                LoggerHelper.Error(ex.InnerException.ToString());
            }
        }
    }
}
