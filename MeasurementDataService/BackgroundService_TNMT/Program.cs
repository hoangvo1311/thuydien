using LicenseHelper;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundService_TNMT
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //var hardwareId = License.GetHardwareId();
            //Logger.LogInfo($"hardwareId {hardwareId}");
            //if (!License.MatchAnyToken(hardwareId, AddressConstants.Dakrosa1Address))
            //{
            //    Logger.LogError("Invalid license!");
            //    return;
            //}


            //var service = new TNMTBackgroundService();
            //service.PushData();

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new TNMTBackgroundService()
            };
            ServiceBase.Run(ServicesToRun);
        }

    }
}
