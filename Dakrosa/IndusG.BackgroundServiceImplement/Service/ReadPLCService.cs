using IndusG.DataAccess;
using IndusG.Service;
using IndusG.Service.Helpers;
using IndusG.ServiceFrameWork;
using Newtonsoft.Json;
using S7.Net;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Threading;

namespace IndusG.BackgroundServiceImplement.Service
{
    /// <summary>
    /// The actual result service implementation goes here...
    /// </summary>
    [ServiceAttribute("IndusGReadPLCService",
        DisplayName = "IndusGReadPLCService",
        Description = "Service to read value from PLC and insert to SQL database",
        StartMode = ServiceStartMode.Automatic)]
    public class ReadPLCService : LiteServiceBase
    {
        private Plc _plcDriver1;
        private Plc _plcDriver2;
        private PlcService _plcService;
        const int firstWaitTime = 5000;
        const int secondWaitTime = 60000;
        private string webAppUrl = ConfigurationManager.AppSettings["WebAppUrl"] != null ?
            ConfigurationManager.AppSettings["WebAppUrl"].ToString() : string.Empty;
        public byte[] ParametersTimestamp;

        public static double Upstream = 734.00, Upstream_Cal = 734.00, Downstream, Qve_Ho, Qoverflow, QcmH1H2H3, Qminflow, Qve_HaDu, Drainlevel1, Drainlevel2, Qve_HoDB, Reserve_Water, Qminflow_TT, H1_MW, H2_MW, H3_MW, QcmH1, QcmH2, QcmH3, DeltaQsb, Qve_TT, Qve_TB;
        public static double Upstream_Prev = 734.00, Upstream_Cal_Prev = 734.00, Qve_Ho_Prev = 135.99, Upstream_15Prev = 734.00, Upstream_Cal_15Prev = 734.00, Qve_Ho_15Prev = 135.99, Qve_HoDB_15Prev = 4.69, Qve_Ho_Temp = 2.05, Qve_Ho_Temp_15Prev = 57.49;

        public static double[] Qve_TT_Arr = new double[100], QcmH1H2H3_Prev = new double[5];// DeltaQsb_Prev = new double[5];

        public static string Nhamay;

        public static double K_ChuaCoHep = 0.953, K_CoHepNgang = 0.5875, K_CoHepDung = 0.622, K_LuuLuong = 0.9, H_MayPhat = 0.87,  H_CoKhi = 1, H_Turbine = 1, CaoTrinhNguongTran = 734, ChieuDaiDapTran = 40, CaoTrinhNguongKenhXa = 725, ChieuRongKenhXa = 2.5, MucNuocChet = 732, DungTichHoMNC = 40200, K_DCTT = 0.17, DCTT_QuyDinh = 0.95, DungTichHuuIch1 = 27000, DungTichHuuIch2 = 67000, DungTichHuuIch3 = 156000, DungTichHuuIch4 = 319000;

        public static double SampleSize = 4;
        public static bool Upstream_Cal_Toggle;
        public static bool DCTT_Toggle;
        internal Plc PLCDriver1
        {
            get
            {
                if (_plcDriver1 == null)
                {
                    _plcDriver1 = _plcService.InitPLCDriver();
                }

                return _plcDriver1;
            }
        }

        internal Plc PLCDriver2
        {
            get
            {
                if (_plcDriver2 == null)
                {
                    _plcDriver2 = _plcService.InitPLCDriver();
                }

                return _plcDriver2;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ReadPLCService()
        {
            ServiceName = "IndusG - Read PLC Service";
            _plcService = new PlcService();
        }

        /// <summary>
        /// Start the result service for pushing data to database
        /// </summary>
        public override void Start()
        {
            try
            {
                //var syncThread = new Thread(new ThreadStart(() =>
                //{
                //    while (true)
                //    {
                //        try
                //        {
                //            LoggerHelper.Info("Sync data from server");
                //            SyncServerData();
                //            LoggerHelper.Info("Finish Sync data from server");
                //        }
                //        catch (Exception ex)
                //        {
                //            LoggerHelper.Error(ex.Message);
                //            try
                //            {
                //                LoggerHelper.Error(ex.InnerException.InnerException.Message);
                //            }
                //            catch { }
                //        }

                //        Thread.Sleep(2000);
                //    }
                //}));

                //syncThread.IsBackground = true;
                //syncThread.Start();

                LoggerHelper.Info("Start Read PLC Service!");
                _plcDriver1 = _plcService.InitPLCDriver();
                //_plcDriver2 = _plcService.InitPLCDriver();

                try
                {
                    _plcDriver1.Open();
                    //_plcDriver2.Open();
                }
                catch
                {
                    DisconnectPLC1();
                }

                LoggerHelper.Info("Finish init PLC Service!");

                try
                {
                    _plcService.SetServiceStatus(true);
                    var runningServiceNotificationUrl = Path.Combine(webAppUrl, "api/Notification/NotifyServiceRunning");
                    var response = RestAPIHelper.Post(runningServiceNotificationUrl);
                }
                catch (Exception ex)
                {
                    LoggerHelper.Error($"Error while notify service stopped. {ex.Message}");
                }

                LoggerHelper.Info("Get parameters value!");

                var firstThread = new Thread(new ThreadStart(() =>
                {
                    while (true)
                    {
                        try
                        {
                            DoFirstBusiness();
                        }
                        catch (Exception ex)
                        {
                            LoggerHelper.Error(ex.Message);
                            try
                            {
                                LoggerHelper.Error(ex.InnerException.InnerException.Message);
                            }
                            catch { }
                        }

                        Thread.Sleep(firstWaitTime);
                    }
                }));

                firstThread.IsBackground = true;
                firstThread.Start();

                var secondThread = new Thread(new ThreadStart(() =>
                {
                    while (true)
                    {
                        try
                        {
                            LoggerHelper.Info("Reading PLC Data");
                            DoSecondBusiness();
                        }
                        catch (Exception ex)
                        {
                            LoggerHelper.Error(ex.Message);
                            try
                            {
                                LoggerHelper.Error(ex.InnerException.InnerException.Message);
                            }
                            catch { }
                        }

                        Thread.Sleep(secondWaitTime);
                    }
                }));

                secondThread.IsBackground = true;
                secondThread.Start();



            }
            catch (Exception ex)
            {
                LoggerHelper.Error(ex.Message);
            }
        }

        public override void Stop()
        {
            LoggerHelper.Info("Stop Read PLC Service!");
            try
            {
                if (_plcDriver1 != null && _plcDriver1.IsConnected)
                {
                    _plcDriver1.Close();
                }
                if (_plcDriver2 != null && _plcDriver2.IsConnected)
                {
                    _plcDriver2.Close();
                }

                _plcService.SetServiceStatus(false);

                var stopServiceNotificationUrl = Path.Combine(webAppUrl, "api/Notification/NotifyServiceStopped");
                var response = RestAPIHelper.Post(stopServiceNotificationUrl);
                LoggerHelper.Info($"Notify Service Stopped");
            }
            catch (Exception ex)
            {
                LoggerHelper.Error($"Error while notify service stopped. {ex.Message}");
            }

        }

        private void DoSecondBusiness()
        {
            using (var context = new QuantracEntities())
            {
                var plcSetting = context.DB_PLCConfiguration.FirstOrDefault();
                if (plcSetting.Rack != PLCDriver2.Rack || plcSetting.Slot != PLCDriver2.Slot
                    || plcSetting.IPAddress != PLCDriver2.IP
                    || _plcService.GetS7CPUType((Models.Enums.CPUType)plcSetting.CPUType) != PLCDriver2.CPU)
                {
                    _plcDriver2 = _plcService.InitPLCDriver();
                }

                if (!PLCDriver2.IsConnected || plcSetting.Status != true)
                {
                    try
                    {
                        PLCDriver2.Open();
                        LoggerHelper.Info("Connected to PLC");
                    }
                    catch (Exception ex)
                    {
                        LoggerHelper.Error(ex.Message);
                        DisconnectPLC2();
                        return;
                    }
                }
                if (plcSetting.Status != true)
                {
                    _plcService.UpdatePLCStatus(true);
                }
            }

            GetParametersValue();

            // Every 15 minutes
            var currentMinute = DateTime.Now.Minute;
            if (currentMinute % 15 == 0)
            {
                LoggerHelper.Info($"Current Minute: {currentMinute}. ReadAndCalculateData_15m");
                ReadAndCalculateData_15m(PLCDriver2);
                // Move Upstream to Upstream_Prev for calculate DeltaQsb
                Upstream_15Prev = Upstream;
                Upstream_Cal_15Prev = Upstream_Cal;
                Qve_Ho_15Prev = Qve_Ho;
                Qve_Ho_Temp_15Prev = Qve_Ho_Temp;
                Qve_HoDB_15Prev = Qve_HoDB;
            }
            else
            {
                ReadAndCalculateData_1m(PLCDriver2);
                // Move Upstream to Upstream_Prev for calculate DeltaQsb
                Upstream_Prev = Upstream;
                Upstream_Cal_Prev = Upstream_Cal;
                Qve_Ho_Prev = Qve_Ho;
            }
            InsertMeasurement();
            //InsertLocalMeasurement();
            UpdateLatestMeasurement();
        }

        private void SyncServerData()
        {
            using (var context = new QuantracEntities())
            {
                var plcConfiguration = context.DB_PLCConfiguration.FirstOrDefault();
                if (plcConfiguration != null)
                {
                    var settingService = new SettingService();
                    //settingService.SyncConnectionToLocal(plcConfiguration);
                }

                var parameterConfiguration = context.DB_Parameter.FirstOrDefault();
                if (parameterConfiguration != null)
                {
                    var settingService = new SettingService();
                    //settingService.SyncParametersToLocal(parameterConfiguration);
                }
            }
        }

        private void DoFirstBusiness()
        {
            using (var context = new QuantracEntities())
            {
                var plcSetting = context.DB_PLCConfiguration.FirstOrDefault();
                if (plcSetting.Rack != PLCDriver1.Rack || plcSetting.Slot != PLCDriver1.Slot
                    || plcSetting.IPAddress != PLCDriver1.IP
                    || _plcService.GetS7CPUType((Models.Enums.CPUType)plcSetting.CPUType) != PLCDriver1.CPU)
                {
                    _plcDriver1 = _plcService.InitPLCDriver();
                }

                if (!PLCDriver1.IsConnected || plcSetting.Status != true)
                {
                    try
                    {
                        PLCDriver1.Open();
                        LoggerHelper.Info("Connected to PLC");
                        _plcService.UpdatePLCStatus(true);
                    }
                    catch (Exception ex)
                    {
                        LoggerHelper.Error(ex.Message);
                        DisconnectPLC1();
                        return;
                    }
                }
                if (plcSetting.Status != true)
                {
                    _plcService.UpdatePLCStatus(true);
                }

                GetParametersValue();
                ReadAndCalculateData_1m(PLCDriver1);
                UpdateLatestMeasurement();
            }
        }

        private void DisconnectPLC1()
        {
            try
            {
                if (_plcDriver1 != null && _plcDriver1.IsConnected)
                {
                    _plcDriver1.Close();
                }
                _plcDriver1 = null;

                _plcService.UpdatePLCStatus(false);

                var plcDisconnectNotificationUrl = Path.Combine(webAppUrl, "api/Notification/NotifyPLCDisconnect");
                var response = RestAPIHelper.Post(plcDisconnectNotificationUrl);
            }
            catch (Exception ex)
            {
                LoggerHelper.Error($"DisconnectPLC1. {ex.Message}");
            }
        }

        private void DisconnectPLC2()
        {

            try
            {
                if (_plcDriver2 != null && _plcDriver2.IsConnected)
                {
                    _plcDriver2.Close();
                }
                _plcDriver2 = null;

                _plcService.UpdatePLCStatus(false);

                var plcDisconnectNotificationUrl = Path.Combine(webAppUrl, "api/Notification/NotifyPLCDisconnect");
                var response = RestAPIHelper.Post(plcDisconnectNotificationUrl);
            }
            catch (Exception ex)
            {
                LoggerHelper.Error($"DisconnectPLC2. {ex.Message}");
            }
        }


        private void ReadAndCalculateData_15m(Plc plcDriver)
        {
            // Data from PLC
            Upstream = Math.Round(((uint)plcDriver.Read("DB5.DBD0")).ConvertToFloat(), 2);
            Downstream = Math.Round(((uint)plcDriver.Read("DB5.DBD4")).ConvertToFloat(), 2);
            H1_MW = Math.Round(((uint)plcDriver.Read("DB5.DBD16")).ConvertToFloat(), 2);
            H2_MW = Math.Round(((uint)plcDriver.Read("DB5.DBD20")).ConvertToFloat(), 2);
            H3_MW = Math.Round(((uint)plcDriver.Read("DB5.DBD24")).ConvertToFloat(), 2);

            if (Upstream_Cal_Toggle == true)
            {
                Upstream_Cal = Math.Round(((uint)plcDriver.Read("DB5.DBD28")).ConvertToFloat(), 2);
            }
            else
            {
                Upstream_Cal = Upstream;
            }

            #region Drainlevel           

            if (DCTT_Toggle == true)
            {
                Drainlevel1 = Math.Round(((uint)plcDriver.Read("DB5.DBD8")).ConvertToFloat(), 2);
            }
            else
            {
                Drainlevel1 = K_DCTT;
            }

            #endregion


            using (var context = new QuantracEntities())
            {
                // Calculate base on Excel file    
                #region QcmH1
                //QcmH1
                if (H1_MW > 0)
                {
                    QcmH1 = Math.Round((H1_MW * 1000 / (9.81 * H_MayPhat * H_Turbine * H_CoKhi * (Upstream_Cal - Downstream))), 2);
                }
                else
                {
                    QcmH1 = 0;
                }
                #endregion
                // Update H_MayPhat2+3 on 28Oct20
                #region QcmH2
                //QcmH2
                if (H2_MW > 0)
                {
                    QcmH2 = Math.Round((H2_MW * 1000 / (9.81 * H_MayPhat * H_Turbine * H_CoKhi * (Upstream_Cal - Downstream))), 2);
                }
                else
                {
                    QcmH2 = 0;
                }
                #endregion

                #region QcmH3
                //QcmH3
                if (H3_MW > 0)
                {
                    QcmH3 = Math.Round((H3_MW * 1000 / (9.81 * H_MayPhat * H_Turbine * H_CoKhi * (Upstream_Cal - Downstream))), 2);
                }
                else
                {
                    QcmH3 = 0;
                }
                #endregion

                #region QcmH1H2H3
                //QcmH1H2H3 _ Qua nha may
                QcmH1H2H3 = Math.Round(((QcmH1 + QcmH2 + QcmH3)), 2);
                #endregion

                #region Qoverflow
                //Qoverflow
                if (Upstream_Cal > CaoTrinhNguongTran)
                {
                    Qoverflow = Math.Round(K_ChuaCoHep * K_CoHepNgang * ChieuDaiDapTran * Math.Sqrt(2 * 9.81) * Math.Pow((Upstream_Cal - CaoTrinhNguongTran), 1.5), 2);
                }
                else
                {
                    Qoverflow = 0;
                }
                #endregion

                #region Qminflow_TT
                //Qminflow_TT
                if (Drainlevel1 > 0)
                {
                    Qminflow_TT = Math.Round((K_LuuLuong * K_CoHepDung * ChieuRongKenhXa * Drainlevel1 * Math.Sqrt(2 * 9.81 * ((Upstream_Cal - CaoTrinhNguongKenhXa) - K_CoHepDung * Drainlevel1))), 2);
                }
                else
                {
                    Qminflow_TT = 0;

                }
                #endregion


                #region Qminflow

                Qminflow = Qminflow_TT;

                #endregion

                #region DeltaQsb

                #region Dakrosa

                //if (Upstream_Cal_15Prev > 730 && Upstream_Cal > 731 && Upstream_Cal <= 732)
                //{
                //    DeltaQsb = Math.Round(((Upstream_Cal - Upstream_Cal_15Prev) * (DungTichHuuIch1 / 900)), 2);
                //}
                //else if (Upstream_Cal > 732 && Upstream_Cal <= 733)
                //{
                //    DeltaQsb = Math.Round(((Upstream_Cal - Upstream_Cal_15Prev) * (DungTichHuuIch2 / 900)), 2);
                //}
                //else if (Upstream_Cal > 733 && Upstream_Cal <= 734)
                //{
                //    DeltaQsb = Math.Round(((Upstream_Cal - Upstream_Cal_15Prev) * (DungTichHuuIch3 / 900)), 2);
                //}
                //else if (Upstream_Cal > 734 && Upstream_Cal <= 737)
                //{
                //    DeltaQsb = Math.Round(((Upstream_Cal - Upstream_Cal_15Prev) * (DungTichHuuIch4 / 900)), 2);
                //}
                //else
                //{
                //    DeltaQsb = 0;
                //}

                #endregion

                //TODO: DAkrosa 2 khac Dakrosa 1
                #region Dakrosa2

                if (Upstream_Cal_15Prev > 625 && Upstream_Cal <= 632)
                {
                    DeltaQsb = Math.Round(((Upstream_Cal - Upstream_Cal_15Prev) * (DungTichHuuIch1 / 900)), 2);
                }
                else if (Upstream_Cal > 632)
                {
                    DeltaQsb = Math.Round(((Upstream_Cal - Upstream_Cal_15Prev) * (DungTichHuuIch2 / 900)), 2);
                }
                else
                {
                    DeltaQsb = 0;
                }

                #endregion

                #endregion

                #region Modify Qve with Qve_TT & Qve_TB for sample 4 times

                #region Qve_TT
                // Qve_TT
                Qve_TT = Math.Round(QcmH1H2H3 + Qoverflow + Qminflow + DeltaQsb, 2);
                #endregion

                #region Qve_TB

                // Qve_TB
                double Qve_TB_Temp = 0;

                for (int i = 1; i <= SampleSize - 1; i++)
                {
                    var timeAfter = DateTime.Now.AddMinutes(0.5 - i * 15);
                    var timeBefore = DateTime.Now.AddMinutes(-0.5 - i * 15);

                    var measurementValues = context.DB_Data.Where(x => x.Date <= timeAfter && x.Date >= timeBefore);
                    foreach (var measurement in measurementValues)
                    {
                        Qve_TT_Arr[i] = measurement.Qve_TT.HasValue ? measurement.Qve_TT.Value : 0;
                        if (i < 5)
                        {
                            QcmH1H2H3_Prev[i] = measurement.QcmH1H2H3.HasValue ? measurement.QcmH1H2H3.Value : 0;
                        }
                    }

                    Qve_TB_Temp = Qve_TB_Temp + Qve_TT_Arr[i];
                }

                Qve_TB = Math.Round((Qve_TB_Temp + Qve_TT) / SampleSize, 2);

                LoggerHelper.Info($"Qve_TB  {Qve_TB}");

                #endregion

                #endregion

                #region Qve_Ho
                //Qve_Ho

                if (QcmH1H2H3 == 0)
                {
                    Qve_Ho = Qve_TT;
                }
                else
                {
                    Qve_Ho = Qve_TB;
                }


                if (Qve_Ho <= 0)
                {
                    Qve_Ho = 0;
                }


                #endregion

                #region Qve_HaDu
                //Qve_DB
                Qve_HaDu = QcmH1H2H3 + Qoverflow + Qminflow;
                #endregion



                #region Qve_HoDB

                if ((QcmH1H2H3_Prev[4] * QcmH1H2H3_Prev[1] * QcmH1H2H3_Prev[2] * QcmH1H2H3_Prev[3]) > 0 && Qve_TT > Qve_TT_Arr[1] && Qve_TT_Arr[1] > Qve_TT_Arr[2] && Qve_TT_Arr[2] > Qve_TT_Arr[3])
                {
                    Qve_HoDB = Math.Round((Qve_TT + Qve_TT_Arr[1] + Qve_TT_Arr[2] + Qve_TT_Arr[3]) / 4, 2);

                    LoggerHelper.Info("Qve_HoDB = Math.Round");
                    if (double.IsInfinity(Qve_HoDB))
                    {
                        LoggerHelper.Error("Qve_HoDB is infinity");
                        Qve_HoDB = 0;
                    }
                }
                else
                {
                    LoggerHelper.Info("Qve_HoDB = Qve_TB");
                    Qve_HoDB = Qve_TB;
                }


                if (Qve_HoDB <= 0)
                {
                    LoggerHelper.Info($"Qve_HoDB <= 0. {Qve_HoDB}");

                    Qve_HoDB = 0;
                }

                #endregion

                #region Reserve_Water
                if (Upstream_Cal >= 326)
                {
                    Reserve_Water = Math.Round(((Upstream_Cal - MucNuocChet) * DungTichHuuIch1) / 1000000, 3);
                }
                else
                {
                    Reserve_Water = 0;
                }
                #endregion
            }
        }

        private void ReadAndCalculateData_1m(Plc plcDriver)
        {
            // Data from PLC
            Upstream = Math.Round(((uint)plcDriver.Read("DB5.DBD0")).ConvertToFloat(), 2);
            Downstream = Math.Round(((uint)plcDriver.Read("DB5.DBD4")).ConvertToFloat(), 2);
            H1_MW = Math.Round(((uint)plcDriver.Read("DB5.DBD16")).ConvertToFloat(), 2);
            H2_MW = Math.Round(((uint)plcDriver.Read("DB5.DBD20")).ConvertToFloat(), 2);
            H3_MW = Math.Round(((uint)plcDriver.Read("DB5.DBD24")).ConvertToFloat(), 2);

            if (Upstream_Cal_Toggle == true)
            {
                Upstream_Cal = Math.Round(((uint)plcDriver.Read("DB5.DBD28")).ConvertToFloat(), 2);
            }
            else
            {
                Upstream_Cal = Upstream;
            }
            LoggerHelper.Info($"Upstream_Cal {Upstream_Cal}");

            #region Drainlevel           

            if (DCTT_Toggle == true)
            {
                Drainlevel1 = Math.Round(((uint)plcDriver.Read("DB5.DBD8")).ConvertToFloat(), 2);
                Drainlevel2 = Math.Round(((uint)plcDriver.Read("DB5.DBD12")).ConvertToFloat(), 2);
            }
            else
            {
                Drainlevel1 = K_DCTT;
                Drainlevel2 = Math.Round(((uint)plcDriver.Read("DB5.DBD12")).ConvertToFloat(), 2);
            }
            #endregion
            LoggerHelper.Info($"Upstream {Upstream} - Downstream {Downstream} - H1_MW {H1_MW} - H2_MW {H2_MW} - H3_MW {H3_MW}");

            // Calculate base on Excel file    
            #region QcmH1
            //QcmH1
            if (H1_MW > 0)
            {
                QcmH1 = Math.Round((H1_MW * 1000 / (9.81 * H_MayPhat * H_Turbine * H_CoKhi * (Upstream_Cal - Downstream))), 2);
            }
            else
            {
                QcmH1 = 0;
            }
            #endregion



            #region QcmH2
            //QcmH2
            if (H2_MW > 0)
            {
                QcmH2 = Math.Round((H2_MW * 1000 / (9.81 * H_MayPhat * H_Turbine * H_CoKhi * (Upstream_Cal - Downstream))), 2);
            }
            else
            {
                QcmH2 = 0;
            }
            #endregion

            #region QcmH3
            //QcmH3
            if (H3_MW > 0)
            {
                QcmH3 = Math.Round((H3_MW * 1000 / (9.81 * H_MayPhat * H_Turbine * H_CoKhi * (Upstream_Cal - Downstream))), 2);
            }
            else
            {
                QcmH3 = 0;
            }
            #endregion

            #region QcmH1H2H3
            //QcmH1H2H3 _ Qua nha may
            QcmH1H2H3 = (QcmH1 + QcmH2 + QcmH3);
            #endregion

            #region Qoverflow
            //Qoverflow
            //Qoverflow
            if (Upstream_Cal > CaoTrinhNguongTran)
            {
                Qoverflow = Math.Round(K_ChuaCoHep * K_CoHepNgang * ChieuDaiDapTran * Math.Sqrt(2 * 9.81) * Math.Pow((Upstream_Cal - CaoTrinhNguongTran), 1.5), 2);
            }
            else
            {
                Qoverflow = 0;
            }
            #endregion


            #region Qminflow_TT
            //Qminflow_TT
            if (Drainlevel1 > 0)
            {
                Qminflow_TT = Math.Round((K_LuuLuong * K_CoHepDung * ChieuRongKenhXa * Drainlevel1 * Math.Sqrt(2 * 9.81 * ((Upstream_Cal - CaoTrinhNguongKenhXa) - K_CoHepDung * Drainlevel1))), 2);
            }
            else
            {
                Qminflow_TT = 0;

            }
            #endregion


            #region Qminflow

            Qminflow = Qminflow_TT;


            #endregion

            #region DeltaQsb
            if (Upstream_Cal_15Prev > 730 && Upstream_Cal > 731 && Upstream_Cal <= 732)
            {
                DeltaQsb = Math.Round(((Upstream_Cal - Upstream_Cal_15Prev) * (DungTichHuuIch1 / 900)), 2);
            }
            else if (Upstream_Cal > 732 && Upstream_Cal <= 733)
            {
                DeltaQsb = Math.Round(((Upstream_Cal - Upstream_Cal_15Prev) * (DungTichHuuIch2 / 900)), 2);
            }
            else if (Upstream_Cal > 733 && Upstream_Cal <= 734)
            {
                DeltaQsb = Math.Round(((Upstream_Cal - Upstream_Cal_15Prev) * (DungTichHuuIch3 / 900)), 2);
            }
            else if (Upstream_Cal > 734 && Upstream_Cal <= 737)
            {
                DeltaQsb = Math.Round(((Upstream_Cal - Upstream_Cal_15Prev) * (DungTichHuuIch4 / 900)), 2);
            }
            else
            {
                DeltaQsb = 0;
            }
            #endregion

            #region Qve_HaDu
            //Qve_DB
            Qve_HaDu = QcmH1H2H3 + Qoverflow + Qminflow;
            #endregion


            #region Qve_Ho, Qve_HoDB

            //Qve_Ho

            Qve_Ho = Qve_Ho_15Prev;
            Qve_HoDB = Qve_HoDB_15Prev;


            #endregion



            #region Reserve_Water
            // Change to Dung tich huu ich 02Aug20
            if (Upstream_Cal >= 326)
            {
                Reserve_Water = Math.Round(((Upstream_Cal - MucNuocChet) * DungTichHuuIch1) / 1000000, 3);
            }
            else
            {
                Reserve_Water = 0;
            }
            #endregion
        }

        private void InsertMeasurement()
        {
            var measurementService = new MeasurementService();
            var now = DateTime.Now;
            var measurement = new DB_Data
            {
                Time = new TimeSpan(now.Hour, now.Minute, now.Second),
                Date = now,
                UpstreamWaterLevel_m = Math.Round(Upstream, 2),
                DownstreamWaterLevel_m = Math.Round(Downstream, 2),
                Qve_Ho = Double.IsNaN(Qve_Ho) ? 0 : Math.Round(Qve_Ho, 2),
                Qoverflow = Double.IsNaN(Qoverflow) ? 0 : Math.Round(Qoverflow, 2),
                QcmH1H2H3 = Double.IsNaN(QcmH1H2H3) ? 0 : Math.Round(QcmH1H2H3, 2),
                Qminflow = Double.IsNaN(Qminflow) ? 0 : Math.Round(Qminflow, 2),
                Qve_Hadu = Double.IsNaN(Qve_HaDu) ? 0 : Math.Round(Qve_HaDu, 2),
                Drain_Level1 = Double.IsNaN(Drainlevel1) ? 0 : Math.Round(Drainlevel1, 2),
                Qve_HoDB = Double.IsNaN(Qve_HoDB) ? 0 : Math.Round(Qve_HoDB, 2),
                Reserve_Water = Double.IsNaN(Reserve_Water) ? 0 : Math.Round(Reserve_Water, 3),
                Qminflow_TT = Double.IsNaN(Qminflow_TT) ? 0 : Math.Round(Qminflow_TT, 2),
                H1_MW = Double.IsNaN(H1_MW) ? 0 : Math.Round(H1_MW, 2),
                H2_MW = Double.IsNaN(H2_MW) ? 0 : Math.Round(H2_MW, 2),
                H3_MW = Double.IsNaN(H3_MW) ? 0 : Math.Round(H3_MW, 2),
                QcmH1 = Double.IsNaN(QcmH1) ? 0 : Math.Round(QcmH1, 2),
                QcmH2 = Double.IsNaN(QcmH2) ? 0 : Math.Round(QcmH2, 2),
                QcmH3 = Double.IsNaN(QcmH3) ? 0 : Math.Round(QcmH3, 2),
                DeltaQsb = Double.IsNaN(DeltaQsb) ? 0 : Math.Round(DeltaQsb, 2),
                Minutes = now.Minute.ToString(),
                Qve_TT = Double.IsNaN(Qve_TT) ? 0 : Math.Round(Qve_TT, 2),
                Qve_TB = Double.IsNaN(Qve_TB) ? 0 : Math.Round(Qve_TB, 2),
                UpstreamWaterLevel_Cal = Double.IsNaN(Upstream_Cal) ? 0 : Math.Round(Upstream_Cal, 2),
            };
            LoggerHelper.Info($"Insert data {JsonConvert.SerializeObject(measurement)}");

            measurementService.InsertMeasurement(measurement);
        }

        private void InsertLocalMeasurement()
        {
            var measurementService = new MeasurementService();
            var now = DateTime.Now;
            var measurement = new DB_Data
            {
                Time = new TimeSpan(now.Hour, now.Minute, now.Second),
                Date = now,
                UpstreamWaterLevel_m = Math.Round(Upstream, 2),
                DownstreamWaterLevel_m = Math.Round(Downstream, 2),
                Qve_Ho = Double.IsNaN(Qve_Ho) ? 0 : Math.Round(Qve_Ho, 2),
                Qoverflow = Double.IsNaN(Qoverflow) ? 0 : Math.Round(Qoverflow, 2),
                QcmH1H2H3 = Double.IsNaN(QcmH1H2H3) ? 0 : Math.Round(QcmH1H2H3, 2),
                Qminflow = Double.IsNaN(Qminflow) ? 0 : Math.Round(Qminflow, 2),
                Qve_Hadu = Double.IsNaN(Qve_HaDu) ? 0 : Math.Round(Qve_HaDu, 2),
                Drain_Level1 = Double.IsNaN(Drainlevel1) ? 0 : Math.Round(Drainlevel1, 2),
                Qve_HoDB = Double.IsNaN(Qve_HoDB) ? 0 : Math.Round(Qve_HoDB, 2),
                Reserve_Water = Double.IsNaN(Reserve_Water) ? 0 : Math.Round(Reserve_Water, 3),
                Qminflow_TT = Double.IsNaN(Qminflow_TT) ? 0 : Math.Round(Qminflow_TT, 2),
                H1_MW = Double.IsNaN(H1_MW) ? 0 : Math.Round(H1_MW, 2),
                H2_MW = Double.IsNaN(H2_MW) ? 0 : Math.Round(H2_MW, 2),
                H3_MW = Double.IsNaN(H3_MW) ? 0 : Math.Round(H3_MW, 2),
                QcmH1 = Double.IsNaN(QcmH1) ? 0 : Math.Round(QcmH1, 2),
                QcmH2 = Double.IsNaN(QcmH2) ? 0 : Math.Round(QcmH2, 2),
                QcmH3 = Double.IsNaN(QcmH3) ? 0 : Math.Round(QcmH3, 2),
                DeltaQsb = Double.IsNaN(DeltaQsb) ? 0 : Math.Round(DeltaQsb, 2),
                Minutes = now.Minute.ToString(),
                Qve_TT = Double.IsNaN(Qve_TT) ? 0 : Math.Round(Qve_TT, 2),
                Qve_TB = Double.IsNaN(Qve_TB) ? 0 : Math.Round(Qve_TB, 2),
                UpstreamWaterLevel_Cal = Double.IsNaN(Upstream_Cal) ? 0 : Math.Round(Upstream_Cal, 2),
            };
            LoggerHelper.Info($"Insert data {JsonConvert.SerializeObject(measurement)}");

            //measurementService.InsertLocalMeasurement(measurement);
        }

        private void GetParametersValue()
        {
            using (var context = new QuantracEntities())
            {
                var parameters = context.DB_Parameter.First();
                if (ParametersTimestamp == null || parameters.Timestamp != ParametersTimestamp)
                {
                    K_ChuaCoHep = parameters.K_ChuaCoHep.Value;
                    K_CoHepNgang = parameters.K_CoHepNgang.Value;
                    K_CoHepDung = parameters.K_CoHepDung.Value;
                    K_LuuLuong = parameters.K_LuuLuong.Value;
                    H_MayPhat = parameters.H_MayPhat.Value;
                    H_CoKhi = parameters.H_CoKhi.Value;
                    H_Turbine = parameters.H_Turbine.Value;
                    CaoTrinhNguongTran = parameters.CaoTrinhNguongTran.Value;
                    ChieuDaiDapTran = parameters.ChieuDaiDapTran.Value;
                    CaoTrinhNguongKenhXa = parameters.CaoTrinhNguongKenhXa.Value;
                    ChieuRongKenhXa = parameters.ChieuRongKenhXa.Value;
                    DungTichHuuIch1 = parameters.DungTichHuuIch1.Value;
                    DungTichHuuIch2 = parameters.DungTichHuuIch2.Value;
                    DungTichHuuIch3 = parameters.DungTichHuuIch3.Value;
                    DungTichHuuIch4 = parameters.DungTichHuuIch4.Value;
                    MucNuocChet = parameters.MucNuocChet.Value;
                    DungTichHoMNC = parameters.DungTichHoMNC.Value;
                    DCTT_QuyDinh = parameters.DCTT_QuyDinh.Value;
                    SampleSize = parameters.SampleSize.Value;
                    K_DCTT = parameters.K_DCTT.Value;
                    ParametersTimestamp = parameters.Timestamp;
                    DCTT_Toggle = parameters.DCTT_Toggle.Value;
                    Upstream_Cal_Toggle = parameters.Upstream_Cal_Toggle.Value;
                }
            }
        }

        private void UpdateLatestMeasurement()
        {
            var measurementService = new MeasurementService();

            measurementService.UpdateLatestMeasurement(new DB_Measurement
            {
                Date = DateTime.Now,
                UpstreamWaterLevel_m = Upstream,
                DownstreamWaterLevel_m = Downstream,
                Qve_Ho = Double.IsNaN(Qve_Ho) ? 0 : Math.Round(Qve_Ho, 2),
                Qoverflow = Double.IsNaN(Qoverflow) ? 0 : Math.Round(Qoverflow, 2),
                QcmH1H2H3 = Double.IsNaN(QcmH1H2H3) ? 0 : Math.Round(QcmH1H2H3, 2),
                Qminflow = Double.IsNaN(Qminflow) ? 0 : Math.Round(Qminflow, 2),
                Qve_Hadu = Double.IsNaN(Qve_HaDu) ? 0 : Math.Round(Qve_HaDu, 2),
                Drain_Level1 = Double.IsNaN(Drainlevel1) ? 0 : Math.Round(Drainlevel1, 2),
                Qve_HoDB = Double.IsNaN(Qve_HoDB) ? 0 : Math.Round(Qve_HoDB, 2),
                Reserve_Water = Double.IsNaN(Reserve_Water) ? 0 : Math.Round(Reserve_Water, 3),
                Qminflow_TT = Double.IsNaN(Qminflow_TT) ? 0 : Math.Round(Qminflow_TT, 2),
                H1_MW = Double.IsNaN(H1_MW) ? 0 : Math.Round(H1_MW, 2),
                H2_MW = Double.IsNaN(H2_MW) ? 0 : Math.Round(H2_MW, 2),
                H3_MW = Double.IsNaN(H3_MW) ? 0 : Math.Round(H3_MW, 2),
                QcmH1 = Double.IsNaN(QcmH1) ? 0 : Math.Round(QcmH1, 2),
                QcmH2 = Double.IsNaN(QcmH2) ? 0 : Math.Round(QcmH2, 2),
                QcmH3 = Double.IsNaN(QcmH3) ? 0 : Math.Round(QcmH3, 2),
                DeltaQsb = Double.IsNaN(DeltaQsb) ? 0 : Math.Round(DeltaQsb, 2)
            });
        }
    }
}
