using Services;
using System;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;

namespace BackgroundService_TNMT
{
    public partial class TNMTBackgroundService : ServiceBase
    {
        bool isProcessing;

        public TNMTBackgroundService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Logger.LogInfo("Start service to push data to TNMT");
            Task.Run(() => PushData());
            // Set up a timer that triggers every minute.
            Timer timer = new Timer();
            timer.Interval = 60000;
            timer.Elapsed += new ElapsedEventHandler(this.PushDataEventHandler);
            timer.Start();
        }

        public void PushDataEventHandler(object sender, ElapsedEventArgs args)
        {
            PushData();
        }

        public void PushData()
        {
            int currentMinute = DateTime.Now.Minute;

            if (currentMinute == 0 || currentMinute == 15 || currentMinute == 30 || currentMinute == 45
               || currentMinute == 59 || currentMinute == 14 || currentMinute == 29 || currentMinute == 44)
            {
                return;
            }

            while (true)
            {
                if (!isProcessing)
                {
                    isProcessing = true;
                    var measurementService = new MeasurementServices();
                    measurementService.PushDataToTNMT(false);
                    isProcessing = false;
                    return;
                }
                System.Threading.Thread.Sleep(5000);
            }

        }


        protected override void OnStop()
        {
            Logger.LogInfo("Stop service to push data to TNMT");
        }
    }
}
