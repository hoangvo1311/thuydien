using IndusG.DataAccess;
using IndusG.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace IndusG.Service
{
    public class IAMOMeasurementService
    {
        public IAMO_MeasurementModel GetLatestMeasurement()
        {
            using (var context = new QuantracEntities())
            {
                var measurement = context.DB_IAMO_Measurement.OrderByDescending(x => x.Date).FirstOrDefault();
                if (measurement == null)
                    return new IAMO_MeasurementModel();
                var measurementModel = MapperHelper.Mapper.Map<DB_IAMO_Measurement, IAMO_MeasurementModel>(measurement);
                var configuration = context.DB_IAMO_Configuration.FirstOrDefault();
                if (configuration != null)
                {
                    measurementModel.CotAp = configuration.CotAp;
                }

                return measurementModel;
            }
        }

        public void InsertMeasurement(DB_IAMO_Measurement measurement)
        {
            using (var context = new QuantracEntities())
            {
                foreach(var oldMeasurement in context.DB_IAMO_Measurement)
                {
                    context.DB_IAMO_Measurement.Remove(oldMeasurement);
                }
                context.DB_IAMO_Measurement.Add(measurement);
                context.SaveChanges();
            }
        }

        public void PushDataToThuyLoi()
        {
            using (var context = new QuantracEntities())
            {
                var measurement = context.DB_IAMO_Measurement.Where(x => x.IsPushedToThuyLoi != true)
                    .OrderByDescending(x => x.Date).FirstOrDefault();
                try
                {
                    var sentMeasurement = context.DB_IAMO_Measurement.Where(x => x.IsPushedToThuyLoi != true);
                    RestClient client = new RestClient("https://apiv2.thuyloivietnam.vn/Api/CapNhatDuLieuQuanTrac");
                    var request = new RestRequest(Method.POST);
                    request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                    var luuLuong = measurement.QcmH1 + measurement.QcmH2;
                    request.AddParameter("UserName", "nhatminh");
                    request.AddParameter("PassWord", "cndltd2021");
                    request.AddParameter("MaQuanTrac", "18146");
                    request.AddParameter("ThoiGian", measurement.Date.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    request.AddParameter("GiaTri", luuLuong);
                    LoggerHelper.Info($"Send Data to Thuy Loi. Value: {luuLuong}. ThoiGian: {measurement.Date}");
                    var response = client.Execute(request);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        LoggerHelper.Info($"{response.Content}");
                    }
                    else
                    {
                        LoggerHelper.Error($"Send Data to Thuy Loi failed. Status Code: {response.StatusCode}. Exception: {response.ErrorException}");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"ID: {measurement.Id}. Message: {ex.Message}");
                }
            }
        }
    }
}
