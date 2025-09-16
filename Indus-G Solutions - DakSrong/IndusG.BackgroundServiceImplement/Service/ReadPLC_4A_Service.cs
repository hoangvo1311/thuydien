using EasyModbus;
using IndusG.DataAccess;
using IndusG.Service;
using IndusG.ServiceFrameWork;
using Newtonsoft.Json;
using System;
using System.ServiceProcess;
using System.Threading;

namespace IndusG.BackgroundServiceImplement.Service
{
    /// <summary>
    /// The actual result service implementation goes here...
    /// </summary>
    [ServiceAttribute("IndusGReadPLC_4A_Service",
        DisplayName = "IndusGReadPLC_4A_Service",
        Description = "Service to read measurement value from Siemens and insert to SQL database",
        StartMode = ServiceStartMode.Automatic)]
    public class ReadPLC_4A_Service : LiteServiceBase
    {
        internal ModbusClient ModbusClient { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ReadPLC_4A_Service()
        {
            ServiceName = "IndusG - Read PLC Service";
        }

        /// <summary>
        /// Start the result service for pushing data to database
        /// </summary>
        public override void Start()
        {
            try
            {
                LoggerHelper.Info("Start Read PLC Service!");

                var thread = new Thread(new ThreadStart(() =>
                {
                    while (true)
                    {
                        ModbusClient = new ModbusClient("192.168.8.26", 502);
                        try
                        {
                            int[] readHoldingRegisters_436 = ModbusClient.ReadHoldingRegisters(436, 1);
                            int[] readHoldingRegisters_720 = ModbusClient.ReadHoldingRegisters(720, 1);

                            InsertMeasurement(readHoldingRegisters_720[0], readHoldingRegisters_436[0]);
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

                        ModbusClient.Disconnect();
                        ModbusClient = null;

                        Thread.Sleep(300000);
                    }
                }));

                thread.IsBackground = true;
                thread.Start();

                //string ValueData = "<436>: " + readHoldingRegisters_436[0] + Environment.NewLine + "<710>: " + readHoldingRegistersFloat_710;

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
                if (ModbusClient != null && ModbusClient.Connected)
                {
                    ModbusClient.Disconnect();
                }

            }
            catch (Exception ex)
            {
                LoggerHelper.Error($"Error while notify service stopped. {ex.Message}");
            }

        }

        private void InsertMeasurement(int mucNuoc, int luuLuong)
        {
            var measurementService = new MeasurementService();
            var now = DateTime.Now;
            var measurement = new Measurement
            {
                Date = now,
                MucNuoc = mucNuoc,
                LuuLuong = luuLuong
            };
            LoggerHelper.Info($"Insert data {JsonConvert.SerializeObject(measurement)}");

            measurementService.InsertMeasurement_4A(measurement);
            LoggerHelper.Info("Inserted new measurement data!");
        }
    }
}
