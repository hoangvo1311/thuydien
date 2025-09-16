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
                    else if (args[0].Contains("-InstallReadPLC_DakSrongService"))
                    {
                        InstallReadPLC_DakSrongService();
                    }
                    else if (args[0].Contains("-InstallReadPLC_DakSrong_4NM_Service"))
                    {
                        InstallReadPLC_DakSrong_4NM_Service();
                    }
                    else if (args[0].Contains("-ReadPLC_DakSrongService"))
                    {
                        StartReadPLC_DakSrongService();
                    }
                    else if (args[0].Contains("-ReadPLC_DakSrong_4NM_Service"))
                    {
                        StartReadPLC_DakSrong_4NM_Service();
                    }
                    else if (args[0].Contains("-InstallReadPLC_4AService"))
                    {
                        InstallReadPLC_4AService();
                    }
                    else if (args[0].Contains("-ReadPLC_4AService"))
                    {
                        StartReadPLC_4AService();
                    }
                    else if (args[0].Contains("-InstallReadIAMOService"))
                    {
                        InstallReadIAMOService();
                    }
                    else if (args[0].Contains("-ReadIAMOService"))
                    {
                        StartReadIAMOService();
                    }
                    else if (args[0].Contains("-InstallIAMO_ThuyLoiService"))
                    {
                        InstallIAMO_ThuyLoiService();
                    }
                    else if (args[0].Contains("-RunIAMO_ThuyLoiService"))
                    {
                        StartIAMO_ThuyLoiService();
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

        private static void InstallReadPLC_DakSrongService()
        {
            var readPLCService = new ReadPLC_DakSrongService();
            Assembly service = Assembly.GetEntryAssembly();
            ReadPLCDakSrongServiceInstaller<ReadPLC_DakSrongService> installer = new ReadPLCDakSrongServiceInstaller<ReadPLC_DakSrongService>(new ReadPLC_DakSrongService());
            installer.AssemblyPath = service.Location;
            List<string> existingServiceNames = LiteServiceAssemblyInstaller.GetNameOfAllServices("IndusG");
            installer.OptionalInstall(existingServiceNames);

            if (!readPLCService.IsServiceRunning())
            {
                LiteServiceAssemblyInstaller.StartService(readPLCService.ServiceName);
            }
        }

        private static void InstallReadPLC_DakSrong_4NM_Service()
        {
            //var readPLCService = new ReadPLC_DakSrong_4NM_Service();
            //Assembly service = Assembly.GetEntryAssembly();
            //ReadPLCDakSrong_4NM_ServiceInstaller<ReadPLC_DakSrong_4NM_Service> installer = 
            //    new ReadPLCDakSrong_4NM_ServiceInstaller<ReadPLC_DakSrong_4NM_Service>(new ReadPLC_DakSrong_4NM_Service());
            //installer.AssemblyPath = service.Location;
            //List<string> existingServiceNames = LiteServiceAssemblyInstaller.GetNameOfAllServices("IndusG");
            //installer.OptionalInstall(existingServiceNames);

            //if (!readPLCService.IsServiceRunning())
            //{
            //    LiteServiceAssemblyInstaller.StartService(readPLCService.ServiceName);
            //}
        }

        private static void InstallReadPLC_4AService()
        {
            var readPLCService = new ReadPLC_4A_Service();
            Assembly service = Assembly.GetEntryAssembly();
            ReadPLC_4A_ServiceInstaller<ReadPLC_4A_Service> installer = new ReadPLC_4A_ServiceInstaller<ReadPLC_4A_Service>(new ReadPLC_4A_Service());
            installer.AssemblyPath = service.Location;
            List<string> existingServiceNames = LiteServiceAssemblyInstaller.GetNameOfAllServices("IndusG");
            installer.OptionalInstall(existingServiceNames);

            if (!readPLCService.IsServiceRunning())
            {
                LiteServiceAssemblyInstaller.StartService(readPLCService.ServiceName);
            }
        }

        private static void InstallIAMO_ThuyLoiService()
        {
            LoggerHelper.Info("InstallIAMO_ThuyLoiService");
            try
            {
                var thuyLoiService = new IAMO_ThuyLoiService();
                Assembly service = Assembly.GetEntryAssembly();
                IaMO_ThuyLoiServiceInstaller<IAMO_ThuyLoiService> installer =
                    new IaMO_ThuyLoiServiceInstaller<IAMO_ThuyLoiService>(new IAMO_ThuyLoiService());
                installer.AssemblyPath = service.Location;
                List<string> existingServiceNames = LiteServiceAssemblyInstaller.GetNameOfAllServices("IndusG");
                installer.OptionalInstall(existingServiceNames);

                if (!thuyLoiService.IsServiceRunning())
                {
                    LiteServiceAssemblyInstaller.StartService(thuyLoiService.ServiceName);
                }
            } catch (Exception ex)
            {
                LoggerHelper.Error($"InstallIAMO_ThuyLoiService. Error: {ex.Message}");
            }
        }

        private static void InstallReadIAMOService()
        {
            var readPLCService = new ReadPLCService();
            Assembly service = Assembly.GetEntryAssembly();
            ReadIAMOServiceInstaller<ReadIAMOService> installer =
                new ReadIAMOServiceInstaller<ReadIAMOService>(new ReadIAMOService());
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

        private static void StartReadPLC_DakSrongService()
        {
            ServiceLauncher launcher = new ServiceLauncher(new ReadPLC_DakSrongService());
            launcher.Run(null);
        }

        private static void StartReadPLC_DakSrong_4NM_Service()
        {
            //ServiceLauncher launcher = new ServiceLauncher(new ReadPLC_DakSrong_4NM_Service());
            //launcher.Run(null);
        }


        private static void StartReadPLC_4AService()
        {
            ServiceLauncher launcher = new ServiceLauncher(new ReadPLC_4A_Service());
            launcher.Run(null);
        }


        private static void StartReadIAMOService()
        {
            ServiceLauncher launcher = new ServiceLauncher(new ReadIAMOService());
            launcher.Run(null);
        }

        private static void StartIAMO_ThuyLoiService()
        {
            ServiceLauncher launcher = new ServiceLauncher(new IAMO_ThuyLoiService());
            launcher.Run(null);
        }
    }
}
