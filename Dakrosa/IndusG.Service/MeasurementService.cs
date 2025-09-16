using IndusG.DataAccess;
using IndusG.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IndusG.Service
{
    public class MeasurementService
    {
        public MeasurementModel GetLatestMeasurement()
        {
            using (var context = new QuantracEntities())
            {
                var measurement = context.DB_Measurement.FirstOrDefault();
                if (measurement == null)
                    return new MeasurementModel();
                return MapperHelper.Mapper.Map<DB_Measurement, MeasurementModel>(measurement);
            }

        }

        public void InsertMeasurement(DB_Data measurement)
        {
            try
            {
                using (var context = new QuantracEntities())
                {
                    context.DB_Data.Add(measurement);
                    context.SaveChanges();
                }
                LoggerHelper.Info("Inserted new measurement data in server!");
            }
            catch (Exception ex)
            {
                LoggerHelper.Error($"Error while insert measurement in server. Message: {ex}");
            }

        }

        public void InsertLocalMeasurement(DB_Data measurement)
        {
            try
            {
                using (var context = new QuantracEntities())
                {
                    context.DB_Data.Add(measurement);
                    context.SaveChanges();
                }
                LoggerHelper.Info("Inserted new measurement data in local!");
            }
            catch (Exception ex)
            {
                LoggerHelper.Error($"Error while insert measurement in local. Message: {ex}");
            }

        }

        public List<MeasurementChartModel> GetMeasurementChartData(int selectedHour)
        {
            var measurementChartModelList = new List<MeasurementChartModel>();
            using (var context = new QuantracEntities())
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

                var measurements = context.DB_Data.OrderBy(x => x.Date);
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

        public void UpdateLatestMeasurement(DB_Measurement measurement)
        {
            try
            {
                using (var context = new QuantracEntities())
                {
                    var latestMeasurement = context.DB_Measurement.FirstOrDefault();
                    try
                    {
                        if (latestMeasurement == null)
                        {
                            context.DB_Measurement.Add(measurement);
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
                            latestMeasurement.Qve_Hadu = measurement.Qve_Hadu;
                            latestMeasurement.Qve_HoDB = measurement.Qve_HoDB;
                            latestMeasurement.Qve_Ho = measurement.Qve_Ho;
                            latestMeasurement.Qoverflow = measurement.Qoverflow;
                            latestMeasurement.Qminflow = measurement.Qminflow;
                        }


                        context.SaveChanges();

                    }
                    catch (Exception e)
                    {
                        LoggerHelper.Error($"Measurement. {JsonConvert.SerializeObject(latestMeasurement)}");
                        throw;
                    }

                }
            }
            catch (Exception e) { }
        }
    }
}
