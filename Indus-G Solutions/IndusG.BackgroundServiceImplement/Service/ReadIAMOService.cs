using IndusG.DataAccess;
using IndusG.Models;
using IndusG.Service;
using IndusG.ServiceFrameWork;
using Newtonsoft.Json;
using System;
using System.IO.Ports;
using System.Linq;
using System.ServiceProcess;
using System.Timers;

namespace IndusG.BackgroundServiceImplement.Service
{
    /// <summary>
    /// The actual result service implementation goes here...
    /// </summary>
    [ServiceAttribute("IndusGReadIAMOService",
        DisplayName = "IndusGReadIAMOService",
        Description = "Service to read value from IAMO PLC and insert to SQL database",
        StartMode = ServiceStartMode.Automatic)]
    public class ReadIAMOService : LiteServiceBase
    {
        private modbus mb = new modbus();
        private IAMOConfigurationModel configurationModel { get; set; }
        private IAMOSettingService iamoSettingService;
        Timer timer;
        /// <summary>
        /// Default constructor
        /// </summary>
        public ReadIAMOService()
        {
            ServiceName = "IndusG - Read IAMO Service";
            iamoSettingService = new IAMOSettingService();
            configurationModel = iamoSettingService.GetIAMOSettingModel();
        }

        /// <summary>
        /// Start the result service for pushing data to database
        /// </summary>
        public override void Start()
        {
            try
            {
                LoggerHelper.Info("Start Read IAMO PLC Service!");
                StartPoll();
            }
            catch (Exception ex)
            {
                LoggerHelper.Error(ex.Message);
            }
        }

        public override void Stop()
        {
            LoggerHelper.Info("Stop Read IAMO PLC Service!");
            try
            {
                StopPoll();
            }
            catch (Exception ex)
            {
                LoggerHelper.Error($"Error while notify service stopped. {ex.Message}");
            }
        }

        private decimal GetValue(int slaveID, ushort address)
        {
            //Create array to accept read values:
            short[] values = new short[20];
            ushort pollLength = 20;

            //Read registers and display data in desired format:
            try
            {
                var success = false;
                success = mb.SendFc3(Convert.ToByte(slaveID), address, pollLength, ref values);
                if (!success)
                {
                    System.Threading.Thread.Sleep(200);
                }
            }
            catch (Exception err)
            {
                LoggerHelper.Error("Error in modbus read: " + err.Message);
                throw;
            }

            int intValue = (int)values[0];
            intValue <<= 16;
            intValue += (int)values[0];

            return (decimal)BitConverter.ToSingle(BitConverter.GetBytes(intValue), 0);
        }

        private void StartPoll()
        {
            //Open COM port using provided settings:
            if (mb.Open(configurationModel.Portname, configurationModel.Baudrate, 8, Parity.None, StopBits.One))
            {
                LoggerHelper.Info($"Open connection. Portname {configurationModel.Portname} - BaudRate {configurationModel.Baudrate}");
                //Start timer using provided values:
                timer = new Timer();
                timer.AutoReset = true;
                timer.Interval = 1000;
                timer.Elapsed += new ElapsedEventHandler(GetMeasurementData);
                timer.Start();
            }
        }

        private void StopPoll()
        {
            timer.Stop();
            mb.Close();
            mb = null;
        }

        private void RestartModbus()
        {
            mb.Close();
            mb = null;
            OpenModbus();
        }

        private bool OpenModbus()
        {
            mb = new modbus();
            return mb.Open(configurationModel.Portname, configurationModel.Baudrate, 8, Parity.None, StopBits.One);
        }

        #region Timer Elapsed Event Handler
        void GetMeasurementData(object sender, ElapsedEventArgs e)
        {
            try
            {
                var latestConfigurationModel = iamoSettingService.GetIAMOSettingModel();
                if (!configurationModel.Timestamp.SequenceEqual(latestConfigurationModel.Timestamp))
                {
                    if (configurationModel.Portname != latestConfigurationModel.Portname
                        || configurationModel.Baudrate != latestConfigurationModel.Baudrate)
                    {
                        configurationModel = latestConfigurationModel;
                        RestartModbus();
                    }
                    else
                    {
                        configurationModel = latestConfigurationModel;
                    }
                }
                var measurement = new DB_IAMO_Measurement();
                try
                {
                    if (!mb.IsOpen() || mb.StartTime < DateTime.Now.AddMinutes(-15))
                    {
                        RestartModbus();
                    }   
                    
                    var realActivePower = GetValue(configurationModel.SlaveID1, (ushort)configurationModel.AddressP1);
                    var activePower = TinhCongSuatP(realActivePower, configurationModel.TiSoP1);
                    LoggerHelper.Info($"Cong suat tac dung H1 do duoc {realActivePower}");
                    LoggerHelper.Info($"Cong suat tac dung H1 {activePower}. Ti so {configurationModel.TiSoP1}");

                    var realReactivePower = GetValue(configurationModel.SlaveID1, (ushort)configurationModel.AddressQ1);
                    var reactivePower = TinhCongSuatQ(realReactivePower, configurationModel.TiSoQ1);
                    LoggerHelper.Info($"Cong suat phan khang H1 do duoc {realReactivePower}");
                    LoggerHelper.Info($"Cong suat phan khang H1 {reactivePower}. Ti so {configurationModel.TiSoQ1}");

                    var luuLuong = TinhLuuLuong(activePower);
                    LoggerHelper.Info($"Luu luong qua H1 {luuLuong}");

                    var realActivePower2 = GetValue(configurationModel.SlaveID2, (ushort)configurationModel.AddressP2);
                    var activePower2 = TinhCongSuatP(realActivePower2, configurationModel.TiSoP2);
                    LoggerHelper.Info($"Cong suat tac dung H2 do duoc {realActivePower2}");
                    LoggerHelper.Info($"Cong suat tac dung H2 {activePower2}. Ti so {configurationModel.TiSoP2}");

                    var realReactivePower2 = GetValue(configurationModel.SlaveID2, (ushort)configurationModel.AddressQ2);
                    var reactivePower2 = TinhCongSuatQ(realReactivePower2, configurationModel.TiSoQ2);
                    LoggerHelper.Info($"Cong suat phan khang H2 do duoc {realReactivePower2}");
                    LoggerHelper.Info($"Cong suat phan khang H2 {reactivePower2}. Ti so {configurationModel.TiSoQ2}");

                    var luuLuong2 = TinhLuuLuong(activePower2);
                    LoggerHelper.Info($"Luu luong qua H2 {luuLuong2}");


                    measurement.Date = DateTime.Now;
                    measurement.H1_ActivePower = activePower;
                    measurement.H2_ActivePower = activePower2;
                    measurement.H1_ReactivePower = reactivePower;
                    measurement.H2_ReactivePower = reactivePower2;
                    measurement.QcmH1 = luuLuong;
                    measurement.QcmH2 = luuLuong2;
                }
                catch
                {
                    RestartModbus();
                }
                LoggerHelper.Info($"Insert data {JsonConvert.SerializeObject(measurement)}");

                InsertMeasurement(measurement);
            }
            catch (Exception ex)
            {
                LoggerHelper.Error($"Error {ex.Message} \n {ex.StackTrace}");
            }

        }


        #endregion

        private decimal TinhLuuLuong(decimal activePower)
        {
            return activePower / ((decimal)9.81 *
                configurationModel.HieuSuatMayPhat * configurationModel.HieuSuatCoKhi *
                configurationModel.HieuSuatTurbine * configurationModel.CotAp);
        }

        private decimal TinhCongSuatP(decimal activePower, decimal tiSoP)
        {
            return activePower * (configurationModel.BasedOnTiSo ? tiSoP : 1);
        }

        private decimal TinhCongSuatQ(decimal reActivePower, decimal tiSoQ)
        {
            return reActivePower * (configurationModel.BasedOnTiSo ? tiSoQ : 1);
        }

        private void InsertMeasurement(DB_IAMO_Measurement measurement)
        {
            var measurementService = new IAMOMeasurementService();
            measurementService.InsertMeasurement(measurement);
            LoggerHelper.Info($"Insert data {JsonConvert.SerializeObject(measurement)}");
        }

    }
}
