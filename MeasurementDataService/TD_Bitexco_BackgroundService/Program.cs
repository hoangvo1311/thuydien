using Services;
using System.Threading;

namespace TD_Bitexco_BackgroundService
{
    class Program
    {
        static void Main(string[] args)
        {
            var measurementService = new MeasurementServices();

            while (true)
            {
                measurementService.PushDataToBitexco();
                Thread.Sleep(Settings.Bitexco_SendingInterval);
            }
        }
    }
}
