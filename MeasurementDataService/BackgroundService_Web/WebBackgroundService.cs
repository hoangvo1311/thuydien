using Services;
using System;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;

namespace BackgroundService_Web
{
    public partial class WebBackgroundService : ServiceBase
    {
        bool isProcessing;
        public WebBackgroundService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Logger.LogInfo("Start service to push data to Web Tay Nguyen");
            SyncData();
            // Set up a timer that triggers every minute.
            Timer timer = new Timer();
            timer.Interval = Settings.Web_SyncInterval;
            timer.Elapsed += new ElapsedEventHandler(this.SyncDataEventHandhler);
            timer.Start();
        }

        public void SyncDataEventHandhler(object sender, ElapsedEventArgs args)
        {
            SyncData();
        }

        public void SyncData()
        {
            while (true)
            {
                if (!isProcessing)
                {
                    isProcessing = true;

                    var measurementService = new MeasurementServices();
                    var PlantID = Convert.ToInt32(Settings.PlantID);
                    measurementService.SyncDataToWeb(PlantID);
                    isProcessing = false;
                    return;
                }
                System.Threading.Thread.Sleep(5000);
            }

        }

        protected override void OnStop()
        {
            Logger.LogInfo("Stop service to push data to Web Tay Nguyen");
        }
    }
}
