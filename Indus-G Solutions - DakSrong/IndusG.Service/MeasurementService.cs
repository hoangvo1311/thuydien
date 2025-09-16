using IndusG.DataAccess;
using IndusG.Models;
using Newtonsoft.Json;
using S7.Net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace IndusG.Service
{
    public class MeasurementService
    {
        
        public MeasurementModel GetLatestMeasurement()
        {
            using (var context = new QuantracEntities())
            {
                var measurement = context.DB_SesanMeasurement.FirstOrDefault();
                if (measurement == null)
                    return new MeasurementModel();

                return MapperHelper.Mapper.Map<DB_SesanMeasurement, MeasurementModel>(measurement);
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

        public List<MeasurementChartModel> GetMeasurementChartData(int selectedHour, string selectedDate)
        {
            var measurementChartModelList = new List<MeasurementChartModel>();
            using (var context = new QuantracEntities())
            {
                // Nếu có selectedDate thì dùng ngày đó, ngược lại dùng DateTime.Now
                DateTime baseDate;

                if (string.IsNullOrEmpty(selectedDate)
                    || (DateTime.TryParseExact(selectedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                                               DateTimeStyles.None, out var parsedDate)
                        && parsedDate.Date == DateTime.Today))
                {
                    // giữ nguyên ngày hiện tại + giờ hiện tại
                    baseDate = DateTime.Today + DateTime.Now.TimeOfDay;
                    baseDate = baseDate.AddMinutes(-1);
                }
                else if (DateTime.TryParseExact(selectedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                                                DateTimeStyles.None, out parsedDate))
                {
                    // dùng ngày chọn + 00:00:00
                    baseDate = parsedDate.Date.AddDays(1);
                }
                else
                {
                    // fallback: dùng ngày hiện tại + giờ hiện tại
                    baseDate = DateTime.Today + DateTime.Now.TimeOfDay;
                }

                var lastDate = baseDate.Date + new TimeSpan(baseDate.Hour, baseDate.Minute, 0);

                // Round down về mốc 5 phút gần nhất
                lastDate = lastDate.AddMinutes(-(lastDate.Minute % 5))
                                   .AddSeconds(-lastDate.Second)
                                   .AddMilliseconds(-lastDate.Millisecond);

                var selectedMinute = selectedHour * 12; // 12 intervals/h
                var startDate = lastDate.AddMinutes(-selectedMinute * 5);

                // Query toàn bộ khoảng thời gian
                var endDate = lastDate.AddMinutes(2); // để lấy đủ dữ liệu cho khoảng cuối cùng
                var allMeasurements = context.DB_Sesan_2PLC
                    .Where(x => x.Date >= startDate && x.Date <= endDate)
                    .OrderBy(x => x.Date)
                    .ToList();

                for (var indexMinute = selectedMinute; indexMinute >= 0; indexMinute--)
                {
                    var date = lastDate.AddMinutes(-indexMinute * 5);
                    var end = date.AddMinutes(5);

                    var measurement = allMeasurements
                        .FirstOrDefault(x => x.Date >= date && x.Date < end);

                    if (measurement != null)
                    {
                        measurementChartModelList.Add(new MeasurementChartModel
                        {
                            Date = measurement.Date.ToString("yyyy-MM-ddTHH:mm:ss"), // ISO format cho chart.js
                            DownstreamWaterLevel_m = measurement.DownstreamWaterLevel_m ?? 0,
                            H1_MW = measurement.H1_MW ?? 0,
                            H2_MW = measurement.H2_MW ?? 0,
                            H3_MW = measurement.H3_MW ?? 0,
                            QcmH1H2H3 = measurement.QcmH1H2H3 ?? 0,
                            Qminflow = measurement.Qminflow ?? 0,
                            Qoverflow = measurement.Qoverflow ?? 0,
                            Qve_HaDu = measurement.Qve_Hadu ?? 0,
                            Qve_Ho = measurement.Qve_Ho ?? 0,
                            UpstreamWaterLevel_m = measurement.UpstreamWaterLevel_m ?? 0
                        });
                    }
                    else
                    {
                        measurementChartModelList.Add(new MeasurementChartModel
                        {
                            Date = date.ToString("yyyy-MM-ddTHH:mm:ss")
                        });
                    }
                }
            }
            return measurementChartModelList;
        }

        //public void UpdateLatestMeasurement(DB_Sesan_2PLC measurement)
        //{
        //    using (var context = new QuantracEntities())
        //    {
        //        var latesMeasurement = context.DB_Sesan_2PLC.FirstOrDefault();
        //        if (latesMeasurement == null)
        //        {
        //            context.DB_Sesan_2PLC.Add(measurement);
        //        }
        //        else
        //        {
        //            latesMeasurement.Date = measurement.Date;
        //            latesMeasurement.UpstreamWaterLevel_m = measurement.UpstreamWaterLevel_m;
        //            latesMeasurement.DownstreamWaterLevel_m = measurement.DownstreamWaterLevel_m;
        //            latesMeasurement.DeltaQsb = measurement.DeltaQsb;
        //            latesMeasurement.H1_MW = measurement.H1_MW;
        //            latesMeasurement.H2_MW = measurement.H2_MW;
        //            latesMeasurement.H3_MW = measurement.H3_MW;
        //            latesMeasurement.QcmH1 = measurement.QcmH1;
        //            latesMeasurement.QcmH2 = measurement.QcmH2;
        //            latesMeasurement.QcmH3 = measurement.QcmH3;
        //            latesMeasurement.QcmH1H2H3 = measurement.QcmH1H2H3;
        //            latesMeasurement.Qve_Hadu = measurement.Qve_Hadu;
        //            latesMeasurement.Qve_HoDB = measurement.Qve_HoDB;
        //            latesMeasurement.Qve_Ho = measurement.Qve_Ho;
        //            latesMeasurement.Qoverflow = measurement.Qoverflow;
        //            latesMeasurement.Qminflow = measurement.Qminflow;
        //        }
        //        context.SaveChanges();

        //    }
        //}
    }
}
