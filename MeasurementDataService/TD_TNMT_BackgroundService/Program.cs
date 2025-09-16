using System.Collections.Generic;
using Models;
using Services;
using System.Threading;
using System.Threading.Tasks;

namespace TD_TNMT_BackgroundService
{
    class Program
    {
        static void Main(string[] args)
        {
            var measurementService = new MeasurementServices();

            while (true)
            {
                Task.Run(() => measurementService.PushDataToTNMT());
                Thread.Sleep(Settings.TNMT_SendingInterval);
            }
        }
    }
}
