using Services;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;

namespace BackgroundService_Bitexco
{
    public partial class BitexcoBackgroundService : ServiceBase
    {
        bool isProcessing;
        public BitexcoBackgroundService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Logger.LogInfo("Start service to push data to Bitexco");
            Task.Run(() => PushData());
            // Set up a timer that triggers every minute.
            Timer timer = new Timer();
            timer.Interval = Settings.Bitexco_SendingInterval;
            timer.Elapsed += new ElapsedEventHandler(this.PushDataEventHandhler);
            timer.Start();
        }

        public void PushDataEventHandhler(object sender, ElapsedEventArgs args)
        {
            PushData();
        }

        public void PushData()
        {
            while (true)
            {
                if (!isProcessing)
                {
                    isProcessing = true;
                    var measurementService = new MeasurementServices();
                    measurementService.PushDataToBitexco();
                    isProcessing = false;
                    return;
                }
                System.Threading.Thread.Sleep(5000);
            }
        }

        protected override void OnStop()
        {
            Logger.LogInfo("Stop service to push data to Bitexco");
        }
    }
}
