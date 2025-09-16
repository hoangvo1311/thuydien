using DataAccess;
using Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using Services.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Services
{
    public class MeasurementServices
    {
        private static List<int> GetMinutes(int interval)
        {
            List<int> minutes = new List<int>();

            for (int i = 0; i < 60; i += interval)
            {
                minutes.Add(i);
            }

            return minutes;
        }

        public void PushDataToTNMT(bool usingAPI = true)
        {
            Logger.LogInfo($"Start pushing data to TNMT");
            int[] minutes = new[] { 0, 15, 30, 45 };
            try
            {
                using (var db = new PLCEntities())
                {
                    var fromDate = DateTime.Now.AddDays(-30);
                    int minuteNow = DateTime.Now.Minute;

                    // Check if the current minute is one of the specified values
                    if (minuteNow == 14 || minuteNow == 29 || minuteNow == 44 || minuteNow == 59)
                    {
                        // Sleep for 2 minutes (120,000 milliseconds)
                        System.Threading.Thread.Sleep(120000);
                    }

                    var measurements = db.DB_Sesan_2PLC
                           .Where(x =>
                               (usingAPI && x.IsPushedToCucTNMT != true) ||
                               (!usingAPI && x.IsPushedToSoTNMT != true))
                           .Where(x => x.Date >= fromDate)
                           .OrderBy(x => x.Date)
                           .ToList();

                    //var measurements = db.Measurements.OrderBy(x => x.Date).Where(x => x.IsPushedToTNMT != true).ToList();
                    Logger.LogInfo($"Count = {measurements.Count}");

                    foreach (var measurement in measurements)
                    {
                        int currentMinute = measurement.Date.Minute;

                        // Check if the current measurement's minute is not in the desired list
                        if (!minutes.Contains(currentMinute))
                        { 
                            if (currentMinute != 14 && currentMinute != 29 && currentMinute != 44 && currentMinute != 59)
                            {
                                continue; // Skip this measurement
                            }

                            var date = measurement.Date;
                                var nextTime = new DateTime(
                                    date.Year, date.Month, date.Day,
                                    date.Hour, date.Minute, 59, 999
                                ).AddMinutes(1);
                            var nextMinuteRecord = db.DB_Sesan_2PLC
                                      .FirstOrDefault(x =>
                                          x.Date > measurement.Date &&
                                          x.Date <= nextTime);

                            // If there is no record for the next minute, take the current record
                            if (nextMinuteRecord != null)
                            {
                                continue;
                            }
                        }


                        var measurementDate = measurement.Date.ToString("yyyyMMddHHmmss");
                        var content = new List<NoiDung>();

                        #region Nha may 4A xai modbus

                        //content.Add(new NoiDung(Constants.DongChayToiThieu,
                        //   Constants.LuuLuong, 195,
                        //   "m3/s", measurementDate));
                        //content.Add(new NoiDung(Constants.LuuLuongQuaNhaMay,
                        //    Constants.LuuLuong,
                        //    measurement.LuuLuong.HasValue ? (int)measurement.LuuLuong.Value / 10 : 0,
                        //    "m3/s", measurementDate));
                        //content.Add(new NoiDung(Constants.MucNuocHaLuu,
                        //    Constants.MucNuoc,
                        //    measurement.MucNuoc.HasValue ? Math.Round(measurement.MucNuoc.Value / 100, 2) : 0,
                        //    "m", measurementDate));

                        #endregion


                        content.Add(new NoiDung(Constants.DongChayToiThieu,
                            Constants.LuuLuong,
                            measurement.Qminflow ?? 0,
                            "m3/s", measurementDate));
                        content.Add(new NoiDung(Constants.LuuLuongQuaNhaMay,
                            Constants.LuuLuong,
                            measurement.QcmH1H2H3 ?? 0,
                            "m3/s", measurementDate));
                        content.Add(new NoiDung(Constants.QuaTran,
                            Constants.LuuLuong,
                            measurement.Qoverflow ?? 0,
                            "m3/s", measurementDate));
                        content.Add(new NoiDung(Constants.MucNuocThuongLuu,
                            Constants.MucNuoc,
                            measurement.UpstreamWaterLevel_m ?? 0,
                            "m", measurementDate));

                        // DAKROSA
                        //content.Add(new NoiDung(Constants.MucNuocThuongLuu,
                        //    Constants.MucNuoc,
                        //    measurement.UpstreamWaterLevel_Cal.HasValue ? measurement.UpstreamWaterLevel_Cal.Value : 0,
                        //    "m", measurementDate));

                        content.Add(new NoiDung(Constants.MucNuocHaLuu,
                            Constants.MucNuoc,
                            measurement.DownstreamWaterLevel_m ?? 0,
                            "m", measurementDate));

                        if (usingAPI)
                        {
                            var model = new TNMTModel
                            {
                                KyHieuCongTrinh = Settings.KyHieuCongTrinh,
                                ThoiGianGui = DateTime.Now.ToString("yyyyMMddHHmmss"),
                                NoiDung = content
                            };

                            JsonSerializerSettings settings = new JsonSerializerSettings();
                            settings.Converters.Add(new ContentConverter());

                            string json = JsonConvert.SerializeObject(model, settings);
                            SendTNMTJsonData(measurement, json);
                            db.SaveChanges();
                        }
                        else
                        {
                            var ftpRecords = content.Select(x =>
                                $"{x.KyHieuTram}\t{x.ThongSoDo}\t{x.GiaTriDo}\t{x.DonViTinh}\t{x.ThoiGianDo}\t00").ToList();
                            
                            var sendFTP2Success = SendTNMT_FTP(measurement, ftpRecords, Settings.TNMT_FTP_IP_2, Settings.TNMT_FTP_Username_2, Settings.TNMT_FTP_Password_2);

                            if (!sendFTP2Success)
                            {
                                Logger.LogError($"Error while pushing data to TNMT via FTP 2. Measurement ID: {measurement.ID}");
                                SendTNMT_FTP(measurement, ftpRecords, Settings.TNMT_FTP_IP, Settings.TNMT_FTP_Username, Settings.TNMT_FTP_Password);
                            } else
                            {
                                Logger.LogInfo($"Pushed data to TNMT via FTP 2 successfully. Measurement ID: {measurement.ID}");
                            }
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error while pushing data to TNMT. {ex.Message}. {ex.InnerException}");

            }
            Logger.LogInfo($"Finish pushing data to TNMT");

        }

        /// <summary>
        /// Day data len cuc xai API
        /// </summary>
        /// <param name="measurement"></param>
        /// <param name="json"></param>
        /// <exception cref="Exception"></exception>
        public void SendTNMTJsonData(DB_Sesan_2PLC measurement, string json)
        {
            try
            {
                RestClient client = new RestClient(Settings.TNMT_Url);
                var request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;

                request.AddJsonBody(json);
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

                client.Authenticator = new HttpBasicAuthenticator(Settings.TNMT_Username, Settings.TNMT_Password);
                Logger.LogInfo($"Send Data to TNMT. Data: {json}");
                var response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK && response.ErrorException == null)
                {
                    Logger.LogInfo($"Send Data to TNMT successfully");
                    // TODO: UPDATE
                    measurement.IsPushedToCucTNMT = true;
                    //measurement.IsPushedToTNMT = true;
                }
                else
                {
                    Logger.LogInfo($"Send Data to TNMT failed. Status Code: {response.StatusCode}. Exception: {response.ErrorException}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"ID: {measurement.ID}. Message: {ex.Message}");
            }

        }

        /// <summary>
        /// Day data len so xai FTP
        /// </summary>
        /// <param name="measurement"></param>
        /// <param name="records"></param>
        public bool SendTNMT_FTP(DB_Sesan_2PLC measurement, List<string> records, string ip, string user, string password)
        {
            var success = false;
            var thoiGianGui = DateTime.Now.ToString("yyyyMMddHHmmss");
            if (!Directory.Exists(Constants.ftpFilesFolder))
            {
                Directory.CreateDirectory(Constants.ftpFilesFolder);
            }
            var fileName = $"GL_{Settings.KyHieuCongTrinh}_{thoiGianGui}.txt";
            var filePath = Path.Combine(Constants.ftpFilesFolder, fileName);

            using (var file = new StreamWriter(filePath))
            {
                foreach (var record in records)
                {
                    file.WriteLine(record.ToString());
                }
                file.Close();
            }

            try
            {
                using (var client = new WebClient())
                {
                    // Get the object used to communicate with the server.
                    var url = $"ftp://{ip}/{fileName}";
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.UseBinary = true;
                    request.UsePassive = true;
                    request.KeepAlive = true;
                    request.Credentials = new NetworkCredential(user, password);

                    // Copy the contents of the file to the request stream.
                    using (var fs = File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);
                        fs.Close();
                        Stream requestStream = request.GetRequestStream();
                        requestStream.Write(buffer, 0, buffer.Length);
                        requestStream.Flush();
                        requestStream.Close();
                    }
                }

                measurement.IsPushedToSoTNMT = true;
                // TODO: UPDATE PLANT
                //measurement.IsPushedToTNMT = true;

                success = true;
                Logger.LogInfo($"Send data to TNMT via FTP {ip} successfully. File {fileName}. Data: {JsonConvert.SerializeObject(records)}");
            }
            catch (Exception ex)
            {
                success = false;
                Logger.LogError($"Error while send data to TNMT via FTP {ip}. Message: {ex.Message}");
            }
            finally
            {
                File.Delete(filePath);
            }
            return success;

        }

        public void PushDataToBitexco()
        {
            //Logger.LogInfo($"Start pushing data to Bitexco");

            //try
            //{
            //    using (var db = new PLCEntities())
            //    {
            //        var measurements = db.LocalData.Where(x => x.IsPushedToBitexco != true).ToList();
            //        foreach (var measurement in measurements)
            //        {
            //            var model = new BitexcoModel()
            //            {
            //                MN_TL = measurement.UpstreamWaterLevel_m.HasValue ? measurement.UpstreamWaterLevel_m.Value : 0,
            //                MN_HL = measurement.DownstreamWaterLevel_m.HasValue ? measurement.DownstreamWaterLevel_m.Value : 0,
            //                DT_HO = measurement.Reserve_Water.HasValue ? measurement.Reserve_Water.Value : 0,
            //                LL_TUABIN = measurement.QcmH1H2H3.HasValue ? measurement.QcmH1H2H3.Value : 0,
            //                LL_VE = measurement.Qve_Ho.HasValue ? measurement.Qve_Ho.Value : 0,
            //                LL_DUYTRI = measurement.Qminflow.HasValue ? measurement.Qminflow.Value : 0,
            //                LL_XATRAN = measurement.Qoverflow.HasValue ? measurement.Qoverflow.Value : 0,
            //                //LUONG_MUA = measurement.Luongmua.HasValue ? measurement.Luongmua.Value : 0,
            //            };
            //            var json = JsonConvert.SerializeObject(model);
            //            SendBitexcoJsonData(measurement, json);
            //            db.SaveChanges();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logger.LogError($"Error while pushing data to Bitexco. {ex.Message}");
            //}

            //Logger.LogInfo($"Finish pushing data to Bitexco");

        }

        public void SendBitexcoJsonData(DB_Sesan_2PLC measurement, string json)
        {
            //try
            //{
            //    var requestUrl = $"{Settings.Bitexco_Url}?id={Settings.Bitexco_PlcId}&pass={Settings.Bitexco_Password}&ver=1&time={measurement.Date.ToString("dd-MM-yyyy HH:mm:ss")}&mode=B";
            //    RestClient client = new RestClient(requestUrl);
            //    client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            //    var request = new RestRequest(Method.POST);
            //    request.RequestFormat = DataFormat.Json;

            //    request.AddJsonBody(json);
            //    request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            //    Logger.LogInfo($"Send Data to Bitexco. Data: {json}");
            //    var response = client.Execute(request);
            //    if (response.StatusCode == HttpStatusCode.OK && response.ErrorException == null && response.Content == "OK")
            //    {
            //        Logger.LogInfo($"Send Data to Bitexco successfully. PLCID {Settings.Bitexco_PlcId}");
            //        measurement.IsPushedToBitexco = true;
            //    }
            //    else
            //    {
            //        Logger.LogError($"Send Data to Bitexco failed. PLCID {Settings.Bitexco_PlcId}. StatusCode: {response.StatusCode}. Content: {response.Content}");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception($"ID: {measurement.ID}. Message: {ex.Message}");
            //}
        }

        public void SyncDataToWeb(int plantID)
        {
            //Logger.LogInfo($"Start pushing data to Web");
            //try
            //{
            //    using (var db = new PLCEntities())
            //    {
            //        var measurements = db.Me.Where(x => x.IsSync != true).ToList();

            //        using (var webDB = new WebTayNguyen())
            //        {
            //            foreach (var measurement in measurements)
            //            {
            //                webDB.Measurements.Add(new Measurement()
            //                {
            //                    Date = measurement.Date,
            //                    UpstreamWaterLevel_m = measurement.UpstreamWaterLevel_m,
            //                    DownstreamWaterLevel_m = measurement.DownstreamWaterLevel_m,
            //                    DeltaQsb = measurement.DeltaQsb,
            //                    H1_MW = measurement.H1_MW,
            //                    H2_MW = measurement.H2_MW,
            //                    H3_MW = measurement.H3_MW,
            //                    QcmH1 = measurement.QcmH1,
            //                    QcmH2 = measurement.QcmH2,
            //                    QcmH3 = measurement.QcmH3,
            //                    QcmH1H2H3 = measurement.QcmH1H2H3,
            //                    Qminflow_TT = measurement.Qminflow_TT,
            //                    Qminflow = measurement.Qminflow,
            //                    Qoverflow = measurement.Qoverflow,
            //                    Qve_Ho = measurement.Qve_Ho,
            //                    //Luongmua = measurement.Luongmua,
            //                    Time = measurement.Time,
            //                    Qve_Hadu = measurement.Qve_Hadu,
            //                    Qve_HoDB = measurement.Qve_HoDB,
            //                    Reserve_Water = measurement.Reserve_Water,
            //                    //Drain_Level = measurement.Drain_Level,
            //                    Minutes = measurement.Minutes,
            //                    PlantID = plantID
            //                });
            //                measurement.IsSync = true;
            //            }
            //            webDB.SaveChanges();
            //            db.SaveChanges();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logger.LogError($"Error while pushing data to Web Tay Nguyen. {ex.Message}");
            //}
            //Logger.LogInfo($"Finish pushing data to Web");

        }
    }
}
