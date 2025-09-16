using IndusG.DataAccess;
using IndusG.Service;
using IndusG.Service.Helpers;
using IndusG.ServiceFrameWork;
using Newtonsoft.Json;
using S7.Net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using RainService;

namespace IndusG.BackgroundServiceImplement.Service
{
    /// <summary>
    /// The actual result service implementation goes here...
    /// </summary>
    [ServiceAttribute("IndusGReadPLCDaksrongService",
        DisplayName = "IndusGReadPLCDaksrongService",
        Description = "Service to read measurement value from Siemens and insert to SQL database",
        StartMode = ServiceStartMode.Automatic)]
    public class ReadPLC_DakSrong_4NM_Service : LiteServiceBase
    {
        private Plc _plcDriver1;
        private Plc _plcDriver2;
        private PlcService _plcService;
        const int firstWaitTime = 5000;
        const int secondWaitTime = 60000;
        private string webAppUrl = ConfigurationManager.AppSettings["WebAppUrl"] != null ?
            ConfigurationManager.AppSettings["WebAppUrl"].ToString() : string.Empty;

        private string nhaMay = ConfigurationManager.AppSettings["NhaMay"] != null ?
            ConfigurationManager.AppSettings["NhaMay"].ToString() : string.Empty;

        public byte[] ParametersTimestamp;

        // Data show on DataGridView
        public static double Luongmua, Upstream, Downstream, Qve_Ho, Qoverflow, QcmH1H2H3, Qminflow, Qve_HaDu, Drainlevel, Qve_HoDB, Reserve_Water, Qminflow_TT, H1_MW, H2_MW, H3_MW, QcmH1, QcmH2, QcmH3, DeltaQsb, Qve_TT, Qve_TB;
        public static double Upstream_Prev = 0, Qve_Ho_Prev = 2.05, Upstream_15Prev = 0, Qve_Ho_15Prev = 2.05, Qve_HoDB_15Prev = 2.09, Qve_Ho_Temp = 2.05, Qve_Ho_Temp_15Prev = 57.49;
        public static double[] Qve_TT_Arr = new double[100], QcmH1H2H3_Prev = new double[5];// DeltaQsb_Prev = new double[5];
        // Setting Constant on "SiemensSettingUC"
        public static double K_ChuaCoHep = 0.953, K_CoHepNgang = 0.5114, K_CoHepDung = 0.622, K_LuuLuong = 0.9, H_MayPhat = 0.98, H_CoKhi = 0.995, H_Turbine = 0.94, CaoTrinhNguongTran = 146.5, ChieuDaiDapTran = 600, CaoTrinhNguongKenhXa = 134.55, ChieuRongKenhXa = 1, DungTichHuuIch = 2030000, MucNuocChet = 145.5, DungTichHoMNC = 3870000, K_DCTT = 0.52, DCTT_QuyDinh = 4.2;
        public static double DungTich1, DungTich2, DungTich3, DungTich4;
        public static double SampleSize = 96;
        public static bool DCTT_Toggle;
        public static bool ErrorQminflow_TT = false;

        internal Plc PLCDriver1
        {
            get
            {
                if (_plcDriver1 != null) return _plcDriver1;
                _plcDriver1 = _plcService.InitPLCDriver();

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
        public ReadPLC_DakSrong_4NM_Service()
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
                LoggerHelper.Info("Start Read PLC Service!");
                LoggerHelper.Info($"Luong mua {Math.Round(RainFallHelper.GetRainFall(Plant.DakSrong2), 2)}");
                if (string.IsNullOrEmpty(nhaMay))
                {
                    throw new Exception("Chua cau hinh Nha May!");
                }

                #region Start-up values for decrease error
                if (nhaMay.TrimEnd() == "2")
                {
                    Upstream_15Prev = 242.26;
                    Upstream_Prev = 242.27;
                }
                else if (nhaMay.TrimEnd() == "2A")
                {
                    Upstream_15Prev = 201.35;
                    Upstream_Prev = 201.35;
                }
                else if (nhaMay.TrimEnd() == "3A")
                {
                    Upstream_15Prev = 146.11;
                    Upstream_Prev = 146.11;
                }
                else if (nhaMay.TrimEnd() == "3B")
                {
                    Upstream_15Prev = 133.95;
                    Upstream_Prev = 133.95;
                }
                #endregion


                _plcDriver1 = _plcService.InitPLCDriver();
                _plcDriver2 = _plcService.InitPLCDriver();

                try
                {
                    _plcDriver1.Open();
                    _plcDriver2.Open();
                }
                catch
                {
                    // ignored
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
                            catch
                            {
                                // ignored
                            }
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
                            DoSecondBusiness();
                        }
                        catch (Exception ex)
                        {
                            LoggerHelper.Error(ex.Message);
                            try
                            {
                                LoggerHelper.Error(ex.InnerException.InnerException.Message);
                            }
                            catch
                            {
                                // ignored
                            }
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
            using (var context = new DakSrong4NMEntities())
            {
                var plcSetting = context.DB_SesanPLCConfiguration.FirstOrDefault();
                if (plcSetting != null && (plcSetting.Rack != PLCDriver2.Rack || plcSetting.Slot != PLCDriver2.Slot
                                                                              || plcSetting.IPAddress != PLCDriver2.IP
                                                                              || _plcService.GetS7CPUType((Models.Enums.CPUType)plcSetting.CPUType) != PLCDriver2.CPU))
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
                Qve_Ho_15Prev = Qve_Ho;
                Qve_Ho_Temp_15Prev = Qve_Ho_Temp;
                Qve_HoDB_15Prev = Qve_HoDB;
            }
            else
            {
                ReadAndCalculateData_1m(PLCDriver2);
                // Move Upstream to Upstream_Prev for calculate DeltaQsb
                Upstream_Prev = Upstream;
                Qve_Ho_Prev = Qve_Ho;
            }
            InsertMeasurement();
            UpdateLatestMeasurement();
        }

        private void DoFirstBusiness()
        {
            using (var context = new DakSrong4NMEntities())
            {

                var plcSetting = context.DB_SesanPLCConfiguration.FirstOrDefault();
                if (plcSetting?.CPUType != null && (plcSetting.Rack != PLCDriver1.Rack || plcSetting.Slot != PLCDriver1.Slot
                        || plcSetting.IPAddress != PLCDriver1.IP
                        || _plcService.GetS7CPUType((Models.Enums.CPUType)plcSetting.CPUType) != PLCDriver1.CPU))
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

                if (plcSetting != null && plcSetting.Status != true)
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
            H1_MW = Math.Round(((uint)plcDriver.Read("DB5.DBD8")).ConvertToFloat(), 2);
            H2_MW = Math.Round(((uint)plcDriver.Read("DB5.DBD12")).ConvertToFloat(), 2);
            H3_MW = Math.Round(((uint)plcDriver.Read("DB5.DBD16")).ConvertToFloat(), 2);

            #region DrainLevel
            //Get from AI channel of PLC or input directly on the screen   
            // --- Update change request 09Apr21        
            if (DCTT_Toggle)
            {
                Drainlevel = Math.Round(((uint)plcDriver.Read("DB5.DBD380")).ConvertToFloat(), 2);
            }
            else
            {
                Drainlevel = DCTT_QuyDinh; // Update 09Apr21 DCTT_QuyDinh -> DrainLevel 

            }

            #endregion
            var now = DateTime.Now;
            // Get data from API Vrain
            if (now.Minute.Equals(30))
            {
                try
                {
                    if (nhaMay.TrimEnd() == "2A" || nhaMay.TrimEnd() == "2")
                    {
                        Luongmua = Math.Round(RainFallHelper.GetRainFall(Plant.DakSrong2), 2);
                    }
                    else if (nhaMay.TrimEnd() == "3A" || nhaMay.TrimEnd() == "3B")
                    {
                        Luongmua = Math.Round(RainFallHelper.GetRainFall(Plant.DakSrong3A), 2);
                    }
                }
                catch (Exception ex)
                {
                    LoggerHelper.Error($"Exception when read data from API Vrain: \n {ex.Message}");
                }
            }
            //Luongmua = 1;

            using (var context = new DakSrong4NMEntities())
            {
                // Calculate base on Excel file    
                #region QcmH1
                //Update on 06Jan22 - Change formular of Qcm H1 _ H2 _H3 (2+2A) (3A+3B)
                //QcmH1
                if (nhaMay.TrimEnd() == "2")
                {
                    QcmH1 = Math.Round((H1_MW * 2.499 / H_MayPhat), 2);
                }
                if (nhaMay.TrimEnd() == "2A")
                {
                    QcmH1 = Math.Round((H1_MW * 4.465 / H_MayPhat), 2);
                }
                else if (nhaMay.TrimEnd() == "3A" || nhaMay.TrimEnd() == "3B")
                {
                    if (H1_MW > 0)
                    {
                        QcmH1 = Math.Round((H1_MW * 1000 / (9.81 * H_MayPhat * H_Turbine * H_CoKhi * (Upstream - Downstream))), 2);
                    }
                    else
                    {
                        QcmH1 = 0;
                    }
                }

                #endregion

                #region QcmH2
                //QcmH2
                if (nhaMay.TrimEnd() == "2")
                {
                    QcmH2 = Math.Round((H2_MW * 2.499 / H_MayPhat), 2);
                }
                if (nhaMay.TrimEnd() == "2A")
                {
                    QcmH2 = Math.Round((H2_MW * 4.465 / H_MayPhat), 2);
                }
                else if (nhaMay.TrimEnd() == "3A" || nhaMay.TrimEnd() == "3B")
                {
                    if (H2_MW > 0)
                    {
                        QcmH2 = Math.Round((H2_MW * 1000 / (9.81 * H_MayPhat * H_Turbine * H_CoKhi * (Upstream - Downstream))), 2);
                    }
                    else
                    {
                        QcmH2 = 0;
                    }
                }
                #endregion

                #region QcmH3
                //QcmH3
                if (nhaMay.TrimEnd() == "2")
                {
                    QcmH3 = Math.Round((H3_MW * 2.499 / H_MayPhat), 2);
                }
                if (nhaMay.TrimEnd() == "2A")
                {
                    QcmH3 = Math.Round((H3_MW * 4.465 / H_MayPhat), 2);
                }
                else if (nhaMay.TrimEnd() == "3A" || nhaMay.TrimEnd() == "3B")
                {
                    if (H3_MW > 0)
                    {
                        QcmH3 = Math.Round((H3_MW * 1000 / (9.81 * H_MayPhat * H_Turbine * H_CoKhi * (Upstream - Downstream))), 2);
                    }
                    else
                    {
                        QcmH3 = 0;
                    }
                }
                #endregion

                #region QcmH1H2H3
                //QcmH1H2H3 _ Qua nha may
                QcmH1H2H3 = Math.Round(((QcmH1 + QcmH2 + QcmH3)), 2);
                #endregion

                #region Qoverflow
                //Qoverflow
                if (Upstream > CaoTrinhNguongTran)
                {
                    Qoverflow = Math.Round(K_ChuaCoHep * K_CoHepNgang * ChieuDaiDapTran * Math.Sqrt(2 * 9.81) * Math.Pow((Upstream - CaoTrinhNguongTran), 1.5), 2);
                }
                else
                {
                    Qoverflow = 0;
                }
                #endregion

                #region Qminflow_TT
                //Qminflow_TT

                if (Drainlevel > 0)
                {
                    Qminflow_TT = Math.Round(K_LuuLuong * K_CoHepDung * ChieuRongKenhXa * Drainlevel * Math.Sqrt(2 * 9.81 * ((Upstream - CaoTrinhNguongKenhXa) - K_CoHepDung * Drainlevel)), 2);
                }
                else
                {
                    Qminflow_TT = 0;

                }


                if (double.IsNaN(Qminflow_TT) || double.IsInfinity(Qminflow_TT))
                {
                    throw new Exception("Check input of Do mo cam bien (DrainLevel)");
                }

                #endregion


                #region Qminflow
                //Qminflow  --- Update change request 09Apr21

                Qminflow = Qminflow_TT;


                #endregion

                #region DeltaQsb
                // Update 30Now21 (2A 3B)
                if (nhaMay.TrimEnd() == "3A")
                {
                    if (Upstream_15Prev > 144.5 && Upstream > 145 && Upstream <= 146)
                    {
                        DeltaQsb = Math.Round((Upstream - Upstream_15Prev) *100 * 18.89, 2);
                    }
                    else if (Upstream > 146)
                    {
                        DeltaQsb = Math.Round((Upstream - Upstream_15Prev) * 100 * 26.67, 2);
                    }
                    else
                    {
                        DeltaQsb = 0;
                    }
                }
                else if (nhaMay.TrimEnd() == "3B")
                {
                    if (Upstream_15Prev > 130 && Upstream > 132 && Upstream <= 133)
                    {
                        DeltaQsb = Math.Round((Upstream - Upstream_15Prev) * DungTich1 / 900, 2);
                    }
                    else if (Upstream_15Prev > 131 && Upstream > 133 && Upstream <= 134)
                    {
                        DeltaQsb = Math.Round((Upstream - Upstream_15Prev) * DungTich2 / 900, 2);
                    }
                    else if (Upstream_15Prev > 131 && Upstream > 134 && Upstream <= 135)
                    {
                        DeltaQsb = Math.Round((Upstream - Upstream_15Prev) * DungTich3 / 900, 2);
                    }
                    else if (Upstream > 135)
                    {
                        DeltaQsb = Math.Round((Upstream - Upstream_15Prev) * DungTich4 / 900, 2);
                    }
                    else
                    {
                        DeltaQsb = 0;
                    }
                }
                else if (nhaMay.TrimEnd() == "2")
                {
                    if (Upstream_15Prev > 241)
                    {
                        DeltaQsb = Math.Round(((Upstream - Upstream_15Prev) * (DungTichHuuIch / 900)), 2);
                    }
                    else
                    {
                        DeltaQsb = 0;
                    }
                }
                else if (nhaMay.TrimEnd() == "2A")
                {
                    if (Upstream_15Prev > 199)
                    {
                        DeltaQsb = Math.Round(((Upstream - Upstream_15Prev) * (DungTichHuuIch / 900)), 2);
                    }
                    else
                    {
                        DeltaQsb = 0;
                    }
                }

                #endregion

                #region Modify Qve for NM3A vs NM2
                if (nhaMay.TrimEnd() == "3A" || nhaMay.TrimEnd() == "2" || nhaMay.TrimEnd() == "3B" || nhaMay.TrimEnd() == "2A")
                {
                    #region Qve_TT
                    // Qve_TT
                    Qve_TT = Math.Round(QcmH1H2H3 + Qoverflow + Qminflow + DeltaQsb, 2);
                    #endregion

                    #region Qve_TB
                    double Qve_TB_Temp = 0;

                    for (int i = 1; i <= SampleSize - 1; i++)
                    {
                        var timeAfter = now.AddMinutes(0.5 - i * 15);
                        var timeBefore = now.AddMinutes(-0.5 - i * 15);

                        var measurementValues = context.DB_Sesan.Where(x => x.Date <= timeAfter && x.Date >= timeBefore);
                        foreach (var measurement in measurementValues)
                        {
                            Qve_TT_Arr[i] = measurement.Qve_TT ?? 0;
                            if (i < 5)
                            {
                                QcmH1H2H3_Prev[i] = measurement.QcmH1H2H3 ?? 0;
                            }
                        }

                        Qve_TB_Temp += Qve_TT_Arr[i];
                    }

                    LoggerHelper.Info($"Qve_TT_Arr {JsonConvert.SerializeObject(Qve_TT_Arr)}");
                    Qve_TB = Math.Round((Qve_TB_Temp + Qve_TT) / SampleSize, 2);
                    LoggerHelper.Info($"Qve_TB_Temp {Qve_TB_Temp} - Qve_TB {Qve_TB}");

                    #endregion
                }
                #endregion

                #region Qve_Ho
                //Qve_Ho
                // Update 30Nov21 - Don't get data from Upper Dam (2->2A, 3A-> 3B) 
                // Update 06Jan22 - Change formular for 3B + 2A
                if (nhaMay.TrimEnd() == "3A" || nhaMay.TrimEnd() == "2")
                {
                    if (Qve_TB == 0)
                    {
                        Qve_Ho = Qve_TT;
                    }
                    else if (Qve_TB > 0)
                    {
                        Qve_Ho = Qve_TB;
                    }
                }
                else if (nhaMay.TrimEnd() == "3B" || nhaMay.TrimEnd() == "2A")
                {
                    var pre15m = (now.AddMinutes(-15).Minute).ToString();
                    var dataPre15m = context.DB_Sesan.FirstOrDefault(x => x.Minutes == pre15m);
                    var QcmH1H2H3_Prev15 = dataPre15m != null ? dataPre15m.QcmH1H2H3 : 0;
                    if (QcmH1H2H3 == 0 & QcmH1H2H3_Prev15 == 0)
                    {
                        Qve_Ho = Qve_TT;
                    }
                    else if (Qve_TB > 0)
                    {
                        Qve_Ho = Qve_TB;
                    }
                }
                /*else if (Nhamay.TrimEnd() == "3B" || Nhamay.TrimEnd() == "2A")
                {
                    int plantID = 0;
                    if (Nhamay.TrimEnd() == "2A")
                    {
                        plantID = 1;
                    }
                    else if (Nhamay.TrimEnd() == "3B")
                    {
                        plantID = 3;
                    }

                    SqlConnection cnnServer = new SqlConnection(cnnString_Server);
                    SqlCommand cmdMoveQve;

                    String sqlMoveQve = "SELECT [Qve_HaDu],[Qve_HoDB]  FROM [thu89357_Quantrac].[thu89357_thuydienadmin].[Measurements] " +
                                   "WHERE [Date]<= @timeafter AND [Date]>=@timebefore AND [PlantID]=@plantID";

                    cmdMoveQve = new SqlCommand(sqlMoveQve, cnnServer);
                    cmdMoveQve.Parameters.AddWithValue("@timeafter", DateTime.Now.AddMinutes(0.5 - 1));
                    cmdMoveQve.Parameters.AddWithValue("@timebefore", DateTime.Now.AddMinutes(-0.5 - 1));
                    cmdMoveQve.Parameters.AddWithValue("@plantID", plantID);
                    // Open Connection String
                    cnnServer.Open();
                    SqlDataReader reader1 = cmdMoveQve.ExecuteReader();
                    try
                    {
                        while (reader1.Read())
                        {
                            Qve_Ho = reader1.GetDouble(0);
                            Qve_HoDB = reader1.GetDouble(1);
                        }
                    }
                    finally
                    {
                        // Always call Close when done reading.
                        reader1.Close();
                    }
                    cnnServer.Close();
                }
                */
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
                // Update on 02Dec21
                if (nhaMay.TrimEnd() == "3A" || nhaMay.TrimEnd() == "2" || nhaMay.TrimEnd() == "3B" || nhaMay.TrimEnd() == "2A")
                {
                    if ((QcmH1H2H3_Prev[4] * QcmH1H2H3_Prev[1] * QcmH1H2H3_Prev[2] * QcmH1H2H3_Prev[3]) > 0 && Qve_TT > Qve_TT_Arr[1] && Qve_TT_Arr[1] > Qve_TT_Arr[2] && Qve_TT_Arr[2] > Qve_TT_Arr[3])
                    {
                        Qve_HoDB = Math.Round((Qve_TT + (Qve_TT + Qve_TT_Arr[2]) / 2), 2);
                    }
                    else
                    {
                        Qve_HoDB = Qve_Ho;
                    }
                }
                /*
                // UPDATE 30NOV21
                ELSE IF (NHAMAY.TRIMEND() == "2A")
                {
                    //QVE_HODB
                    QVE_HODB = QVE_HO*2 - QVE_HO_15PREV;
                }
                ELSE IF (NHAMAY.TRIMEND() == "3B" )
                {
                    //QVE_HODB
                    QVE_HODB = QVE_HO * 1.2;
                }
                */
                if (Qve_HoDB <= 0)
                {
                    Qve_HoDB = 0;
                }

                #endregion

                #region Reserve_Water
                // Update 25Jul20
                if (nhaMay.TrimEnd() == "3A")
                {
                    if (Upstream > 145 && Upstream <= 146)
                    {
                        Reserve_Water = Math.Round(((Upstream - MucNuocChet) * 1740000 + DungTichHoMNC) / 1000000, 2);
                    }
                    else if (Upstream > 146)
                    {
                        Reserve_Water = Math.Round(((Upstream - 146) * 2320000 + 4730000) / 1000000, 2);
                    }
                    else
                    {
                        Reserve_Water = 0;
                    }
                }
                else if (nhaMay.TrimEnd() == "3B")
                {
                    if (Upstream > 132 && Upstream <= 134)
                    {
                        Reserve_Water = Math.Round(((Upstream - MucNuocChet) * 760000 + DungTichHoMNC) / 1000000, 2);
                    }
                    else if (Upstream > 134)
                    {
                        Reserve_Water = Math.Round(((Upstream - 134) * 890000 + 3000000) / 1000000, 2);
                    }
                    else
                    {
                        Reserve_Water = 0;
                    }
                }
                else
                {
                    if (Upstream > 0)
                    {
                        Reserve_Water = Math.Round(((Upstream - MucNuocChet) * DungTichHuuIch + DungTichHoMNC) / 1000000, 2);
                    }
                    else
                    {
                        Reserve_Water = 0;
                    }
                }

                #endregion
            }

        }

        private void ReadAndCalculateData_1m(Plc plcDriver)
        {
            // Data from PLC
            Upstream = Math.Round(((uint)plcDriver.Read("DB5.DBD0")).ConvertToFloat(), 2);
            Downstream = Math.Round(((uint)plcDriver.Read("DB5.DBD4")).ConvertToFloat(), 2);
            H1_MW = Math.Round(((uint)plcDriver.Read("DB5.DBD8")).ConvertToFloat(), 2);
            H2_MW = Math.Round(((uint)plcDriver.Read("DB5.DBD12")).ConvertToFloat(), 2);
            H3_MW = Math.Round(((uint)plcDriver.Read("DB5.DBD16")).ConvertToFloat(), 2);

            #region DrainLevel
            //Get from AI channel of PLC or input directly on the screen            
            // --- Update change request 09Apr21
            
            if (DCTT_Toggle)
            {
                Drainlevel = Math.Round(((uint)plcDriver.Read("DB5.DBD380")).ConvertToFloat(), 2);
            }
            else
            {
                Drainlevel = DCTT_QuyDinh; // Update 09Apr21 DCTT_QuyDinh -> DrainLevel 

            }

            #endregion


            using (var context = new DakSrong4NMEntities())
            {
                // Calculate base on Excel file    
                #region QcmH1
                //Update on 06Jan22 - Change formular of Qcm H1 _ H2 _H3 (2+2A) (3A+3B)
                //QcmH1
                if (nhaMay.TrimEnd() == "2")
                {
                    QcmH1 = Math.Round((H1_MW * 2.499 / H_MayPhat), 2);
                }
                if (nhaMay.TrimEnd() == "2A")
                {
                    QcmH1 = Math.Round((H1_MW * 4.465 / H_MayPhat), 2);
                }
                else if (nhaMay.TrimEnd() == "3A" || nhaMay.TrimEnd() == "3B")
                {
                    if (H1_MW > 0)
                    {
                        QcmH1 = Math.Round((H1_MW * 1000 / (9.81 * H_MayPhat * H_Turbine * H_CoKhi * (Upstream - Downstream))), 2);
                    }
                    else
                    {
                        QcmH1 = 0;
                    }
                }

                #endregion

                #region QcmH2
                //QcmH2
                if (nhaMay.TrimEnd() == "2")
                {
                    QcmH2 = Math.Round((H2_MW * 2.499 / H_MayPhat), 2);
                }
                if (nhaMay.TrimEnd() == "2A")
                {
                    QcmH2 = Math.Round((H2_MW * 4.465 / H_MayPhat), 2);
                }
                else if (nhaMay.TrimEnd() == "3A" || nhaMay.TrimEnd() == "3B")
                {
                    if (H2_MW > 0)
                    {
                        QcmH2 = Math.Round((H2_MW * 1000 / (9.81 * H_MayPhat * H_Turbine * H_CoKhi * (Upstream - Downstream))), 2);
                    }
                    else
                    {
                        QcmH2 = 0;
                    }
                }
                #endregion

                #region QcmH3
                //QcmH3
                if (nhaMay.TrimEnd() == "2")
                {
                    QcmH3 = Math.Round((H3_MW * 2.499 / H_MayPhat), 2);
                }
                if (nhaMay.TrimEnd() == "2A")
                {
                    QcmH3 = Math.Round((H3_MW * 4.465 / H_MayPhat), 2);
                }
                else if (nhaMay.TrimEnd() == "3A" || nhaMay.TrimEnd() == "3B")
                {
                    if (H3_MW > 0)
                    {
                        QcmH3 = Math.Round((H3_MW * 1000 / (9.81 * H_MayPhat * H_Turbine * H_CoKhi * (Upstream - Downstream))), 2);
                    }
                    else
                    {
                        QcmH3 = 0;
                    }
                }
                #endregion

                #region QcmH1H2H3
                //QcmH1H2H3 _ Qua nha may
                QcmH1H2H3 = (QcmH1 + QcmH2 + QcmH3);
                #endregion

                #region Qoverflow
                //Qoverflow
                if (Upstream > CaoTrinhNguongTran)
                {
                    Qoverflow = Math.Round(K_ChuaCoHep * K_CoHepNgang * ChieuDaiDapTran * Math.Sqrt(2 * 9.81) * Math.Pow((Upstream - CaoTrinhNguongTran), 1.5), 2);
                }
                else
                {
                    Qoverflow = 0;
                }
                #endregion


                #region Qminflow_TT
                //Qminflow_TT
                Qminflow_TT = Math.Round(K_LuuLuong * K_CoHepDung * ChieuRongKenhXa * Drainlevel * Math.Sqrt(2 * 9.81 * ((Upstream - CaoTrinhNguongKenhXa) - K_CoHepDung * Drainlevel)), 2);

                //if (Drainlevel > 0)
                //{
                //    Qminflow_TT = Math.Round(K_LuuLuong * K_CoHepDung * ChieuRongKenhXa * Drainlevel * Math.Sqrt(2 * 9.81 * ((Upstream - CaoTrinhNguongKenhXa) - K_CoHepDung * Drainlevel)), 2);
                //}
                //else
                //{
                //    Qminflow_TT = 0;
                //}

                #endregion


                #region Qminflow
                //Qminflow
                //Update 09Apr21
                Qminflow = Qminflow_TT;

                //if (toggle_DCTT.Checked)
                //{

                //    Qminflow = Qminflow_TT;
                //}
                //else
                //{
                //    Qminflow = DCTT_QuyDinh;

                //}
                #endregion

                #region DeltaQsb
                // Update 30Nov21
                if (nhaMay.TrimEnd() == "3A")
                {
                    if (Upstream_15Prev > 144.5 && Upstream > 145 && Upstream <= 146)
                    {
                        DeltaQsb = Math.Round((Upstream - Upstream_15Prev) * 100 * 18.89, 2);
                    }
                    else if (Upstream > 146)
                    {
                        DeltaQsb = Math.Round((Upstream - Upstream_15Prev) * 100 * 26.67, 2);
                    }
                    else
                    {
                        DeltaQsb = 0;
                    }
                }
                else if (nhaMay.TrimEnd() == "3B")
                {
                    if (Upstream_15Prev > 131 && Upstream > 132 && Upstream <= 133)
                    {
                        DeltaQsb = Math.Round((Upstream - Upstream_15Prev) * 844.44, 2);
                    }
                    else if (Upstream > 134)
                    {
                        DeltaQsb = Math.Round((Upstream - Upstream_15Prev) * 988.89, 2);
                    }
                    else
                    {
                        DeltaQsb = 0;
                    }
                }
                else if (nhaMay.TrimEnd() == "2")
                {
                    if (Upstream_15Prev > 241)
                    {
                        DeltaQsb = Math.Round(((Upstream - Upstream_15Prev) * (DungTichHuuIch / 900)), 2);
                    }
                    else
                    {
                        DeltaQsb = 0;
                    }
                }
                else if (nhaMay.TrimEnd() == "2A")
                {
                    if (Upstream_15Prev > 199)
                    {
                        DeltaQsb = Math.Round(((Upstream - Upstream_15Prev) * (DungTichHuuIch / 900)), 2);
                    }
                    else
                    {
                        DeltaQsb = 0;
                    }
                }

                #endregion

                #region Qve_HaDu
                //Qve_DB
                Qve_HaDu = QcmH1H2H3 + Qoverflow + Qminflow;
                #endregion

                #region Qve_Ho, Qve_HoDB

                //Qve_Ho
                // Update 30Nov21 - Don't get data from Upper Dam (2->2A, 3A-> 3B) 
                if (nhaMay.TrimEnd() == "3A" || nhaMay.TrimEnd() == "2" || nhaMay.TrimEnd() == "3B" || nhaMay.TrimEnd() == "2A")
                {
                    Qve_Ho = Qve_Ho_15Prev;
                    Qve_HoDB = Qve_HoDB_15Prev;
                }
                /*
                else if (Nhamay.TrimEnd() == "3B" || Nhamay.TrimEnd() == "2A")
                {
                    int plantID = 0;
                    if (Nhamay.TrimEnd() == "2A")
                    {
                        plantID = 1;
                    }
                    else if (Nhamay.TrimEnd() == "3B")
                    {
                        plantID = 3;
                    }

                    SqlConnection cnnServer = new SqlConnection(cnnString_Server);
                    SqlCommand cmdMoveQve;

                    String sqlMoveQve = "SELECT [Qve_HaDu],[Qve_HoDB]  FROM [thu89357_Quantrac].[thu89357_thuydienadmin].[Measurements] " +
                                   "WHERE [Date]<= @timeafter AND [Date]>=@timebefore AND [PlantID]=@plantID";
                //C9t6pj1?
                    cmdMoveQve = new SqlCommand(sqlMoveQve, cnnServer);
                    cmdMoveQve.Parameters.AddWithValue("@timeafter", DateTime.Now.AddMinutes(0.5 - 1));
                    cmdMoveQve.Parameters.AddWithValue("@timebefore", DateTime.Now.AddMinutes(-0.5 - 1));
                    cmdMoveQve.Parameters.AddWithValue("@plantID",plantID);
                    // Open Connection String
                    cnnServer.Open();
                    SqlDataReader reader = cmdMoveQve.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            Qve_Ho = reader.GetDouble(0);
                            Qve_HoDB = reader.GetDouble(1);                        
                        }
                    }
                    finally
                    {
                        // Always call Close when done reading.
                        reader.Close();
                    }
                    cnnServer.Close();
                }
                */
                if (Qve_Ho <= 0)
                {
                    Qve_Ho = 0;
                }
                #endregion


                #region Reserve_Water
                if (Upstream > 0)
                {
                    Reserve_Water = Math.Round(((Upstream - MucNuocChet) * DungTichHuuIch + DungTichHoMNC) / 1000000, 2);
                }
                else
                {
                    Reserve_Water = 0;
                }
                #endregion
            }

        }
        
        private void InsertMeasurement()
        {
            var measurementService = new MeasurementService();
            var now = DateTime.Now;
            var measurement = new DB_Sesan_2PLC
            {
                Time = new TimeSpan(now.Hour, now.Minute, now.Second),
                Date = now,
                UpstreamWaterLevel_m = Math.Round(Upstream, 2),
                DownstreamWaterLevel_m = Math.Round(Downstream, 2),
                Luongmua = Math.Round(Luongmua, 2),
                Qve_Ho = Math.Round(Qve_Ho, 2),
                Qoverflow = Math.Round(Qoverflow, 2),
                QcmH1H2H3 = Math.Round(QcmH1H2H3, 2),
                Qminflow = Math.Round(Qminflow, 2),
                Qve_Hadu = Math.Round(Qve_HaDu, 2),
                Drain_Level = Math.Round(Drainlevel, 2),
                Qve_HoDB = Math.Round(Qve_HoDB, 2),
                Reserve_Water = Math.Round(Reserve_Water, 3),
                Qminflow_TT = Math.Round(Qminflow_TT, 2),
                H1_MW = Math.Round(H1_MW, 2),
                H2_MW = Math.Round(H2_MW, 2),
                H3_MW = Math.Round(H3_MW, 2),
                QcmH1 = Math.Round(QcmH1, 2),
                QcmH2 = Math.Round(QcmH2, 2),
                QcmH3 = Math.Round(QcmH3, 2),
                DeltaQsb = Math.Round(DeltaQsb, 2),
                Minutes = now.Minute.ToString(),
                Qve_TT = Math.Round(Qve_TT, 2),
                Qve_TB = Math.Round(Qve_TB, 2)
            };
            LoggerHelper.Info($"Insert data {JsonConvert.SerializeObject(measurement)}");

            measurementService.InsertMeasurement(measurement);
            LoggerHelper.Info("Inserted new measurement data!");
        }

        private void GetParametersValue()
        {
            using (var context = new DakSrong4NMEntities())
            {
                var parameter = context.DB_SesanParameter.First();
                if (ParametersTimestamp == null || parameter.Timestamp != ParametersTimestamp)
                {
                    K_ChuaCoHep = parameter.K_ChuaCoHep;
                    K_CoHepNgang = parameter.K_CoHepNgang;
                    K_CoHepDung = parameter.K_CoHepDung;
                    K_LuuLuong = parameter.K_LuuLuong;
                    H_MayPhat = parameter.H_MayPhat;
                    H_CoKhi = parameter.H_CoKhi;
                    H_Turbine = parameter.H_Turbine;
                    CaoTrinhNguongTran = parameter.CaoTrinhNguongTran;
                    ChieuDaiDapTran = parameter.ChieuDaiDapTran;
                    CaoTrinhNguongKenhXa = parameter.CaoTrinhNguongKenhXa;
                    ChieuRongKenhXa = parameter.ChieuRongKenhXa;
                    DungTichHuuIch = parameter.DungTichHuuIch;
                    MucNuocChet = parameter.MucNuocChet;
                    DungTichHoMNC = parameter.DungTichHoMNC;
                    DCTT_QuyDinh = parameter.DCTT_QuyDinh;
                    SampleSize = parameter.K_DCTT;
                    ParametersTimestamp = parameter.Timestamp;
                    DCTT_Toggle = parameter.DCTT_Toggle.Value;
                    DungTich1 = parameter.DungTich1.HasValue ? parameter.DungTich1.Value : 0;
                    DungTich2 = parameter.DungTich2.HasValue ? parameter.DungTich2.Value : 0;
                    DungTich3 = parameter.DungTich3.HasValue ? parameter.DungTich3.Value : 0;
                    DungTich4 = parameter.DungTich4.HasValue ? parameter.DungTich4.Value : 0;
                }
            }
        }

        private void UpdateLatestMeasurement()
        {
            var measurementService = new MeasurementService();

            measurementService.UpdateLatestMeasurement(new DB_SesanMeasurement
            {
                Date = DateTime.Now,
                UpstreamWaterLevel_m = Upstream,
                DownstreamWaterLevel_m = Downstream,
                DeltaQsb = DeltaQsb,
                H1_MW = H1_MW,
                H2_MW = H2_MW,
                H3_MW = H3_MW,
                QcmH1 = QcmH1,
                QcmH2 = QcmH2,
                QcmH3 = QcmH3,
                QcmH1H2H3 = QcmH1H2H3,
                Qve_Hadu = Qve_HaDu,
                Drain_Level = Drainlevel,
                Luongmua = Luongmua,
                Qve_HoDB = Qve_HoDB,
                Qve_Ho = Qve_Ho,
                Qoverflow = Qoverflow,
                Qminflow = Qminflow,
                Reserve_Water = Reserve_Water
            });
        }
    }
}
