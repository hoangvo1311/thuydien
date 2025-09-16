using System;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.ServiceProcess;

namespace IndusG.ServiceFrameWork
{
    public class ServiceLauncher
    {
        private readonly LiteServiceBase _service;

        public ServiceLauncher(LiteServiceBase service)
        {
            _service = service;
        }

        public void Run(string[] args)
        {
            //Run as console application
            if (Environment.UserInteractive)
            {
                ConsoleService.Run(args, _service);
            }
            else//Run as windows service 
            {            
                ServiceBase[] services = new ServiceBase[] { _service };
                ServiceBase.Run(services);
            }
        }

        /// <summary>
        /// Set the directory path for service otherwise it will be default as "C:\\Windows\\System32"
        /// </summary>
        public static void SetBase()
        {
            Directory.SetCurrentDirectory(new FileInfo(Assembly.GetEntryAssembly().Location).Directory.FullName);
        }
    }
}