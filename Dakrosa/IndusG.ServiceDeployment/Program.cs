using IndusG.BackgroundServiceImplement.Installer;
using IndusG.BackgroundServiceImplement.Service;
using IndusG.Service;
using IndusG.ServiceFrameWork;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace IndusG.ServiceDeployment
{
    class Program
    {
        // The main entry point for the windows service application.
        public static void Main(string[] args)
        {
            try
            {
                //Important ! Sets the current working directory
                ServiceLauncher.SetBase();

                if (args.Length == 1)
                {
                    if (args[0].Contains("-InstallReadPLCService"))
                    {
                        InstallReadPLCService();
                    }
                    else if (args[0].Contains("-ReadPLCService"))
                    {
                        StartReadPLCService();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.Error(ex.ToString());
                LoggerHelper.Error(ex.StackTrace);
                Console.WriteLine(ex.ToString());

                if (Environment.UserInteractive)
                    Console.ReadLine();
            }
        }

        private static void InstallReadPLCService()
        {
            var readPLCService = new ReadPLCService();
            Assembly service = Assembly.GetEntryAssembly();
            ReadPLCServiceInstaller<ReadPLCService> installer = new ReadPLCServiceInstaller<ReadPLCService>(new ReadPLCService());
            installer.AssemblyPath = service.Location;
            List<string> existingServiceNames = LiteServiceAssemblyInstaller.GetNameOfAllServices("IndusG");
            installer.OptionalInstall(existingServiceNames);

            if (!readPLCService.IsServiceRunning())
            {
                LiteServiceAssemblyInstaller.StartService(readPLCService.ServiceName);
            }
        }

        private static void StartReadPLCService()
        {
            ServiceLauncher launcher = new ServiceLauncher(new ReadPLCService());
            launcher.Run(null);
        }
    }
}
