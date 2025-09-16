using System;
using System.ComponentModel;

namespace IndusG.ServiceFrameWork
{
    public static class ConsoleService
    {
        // Run a service from the console given a service implementation
        public static void Run(string[] args, IIndusGService service)
        {
            // simulate starting the windows service
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerAsync(service);

            WriteToConsole("Service {0} Started !!!!! ", service.ServiceName);
            Console.ReadKey();

            // stop and shutdown
            service.Stop();
        }

        static void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var service = (IIndusGService)e.Argument;
            service.Start();
        }

        // Helper method to write a message to the console at the given foreground color.
        internal static void WriteToConsole(string format, params object[] formatArguments)
        {
            Console.WriteLine(format, formatArguments);
            Console.Out.Flush();
        }
    }
}