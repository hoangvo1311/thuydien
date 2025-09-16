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
    [ServiceAttribute("IAMO_ThuyLoiService",
        DisplayName = "IAMO_ThuyLoiService",
        Description = "Service to push Ia Mor data to Thuy Loi",
        StartMode = ServiceStartMode.Automatic)]
    public class IAMO_ThuyLoiService : LiteServiceBase
    {
        private modbus mb = new modbus();
        Timer timer;
        /// <summary>
        /// Default constructor
        /// </summary>
        public IAMO_ThuyLoiService()
        {
            ServiceName = "IndusG - IaMor Thuy Loi Service";
        }

        /// <summary>
        /// Start the result service for pushing data to database
        /// </summary>
        public override void Start()
        {
            try
            {
                LoggerHelper.Info("Start IaMor Thuy Loi Service!");

                //Start timer using provided values:
                timer = new Timer();
                timer.AutoReset = true;
                timer.Interval = 10000;
                timer.Elapsed += new ElapsedEventHandler(PushDataToThuyLoi);
                timer.Start();
            }
            catch (Exception ex)
            {
                LoggerHelper.Error(ex.Message);
            }
        }

        public override void Stop()
        {
            LoggerHelper.Info("Stop IaMor Thuy Loi Service!");
        }

      
        #region Timer Elapsed Event Handler
        void PushDataToThuyLoi(object sender, ElapsedEventArgs e)
        {
            try
            {
                var measurementService = new IAMOMeasurementService();
                measurementService.PushDataToThuyLoi();
            }
            catch (Exception ex)
            {
                LoggerHelper.Error($"Error {ex.Message} \n {ex.StackTrace}");
            }

        }


        #endregion

    }
}
