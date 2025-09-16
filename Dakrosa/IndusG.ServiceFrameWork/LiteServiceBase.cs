using System;
using System.ServiceProcess;
using System.Linq;
using System.Threading;

namespace IndusG.ServiceFrameWork
{
    public abstract class LiteServiceBase : IndusGService, IIndusGService
    {

        protected LiteServiceBase()
        {
        }

        public abstract void Start();
        public abstract void Stop();


        /// <summary>
        /// Returns the specified service status
        /// </summary>
        /// <param name="serviceName">
        /// Name of the service.
        /// </param>
        public virtual string GetServiceStatus()
        {
            try
            {
                ServiceController scm = new ServiceController(this.ServiceName);
                return scm.Status.ToString();
            }
            catch
            {
                return "No Service";
            }
        }

        /// <summary>
        /// Determines whether the specified service is running.
        /// </summary>
        /// <param name="serviceName">
        /// Name of the service.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified service is running; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsServiceRunning()
        {
            try
            {
                ServiceController scm = new ServiceController(this.ServiceName);

                return (scm.Status == ServiceControllerStatus.Running);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Check if service installed already
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public virtual bool IsServiceInstalled()
        {
            try
            {
                var service = ServiceController.GetServices().AsParallel().Where(t => t.ServiceName == this.ServiceName).FirstOrDefault();

                if (service != null)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }


        protected override void OnStart(string[] args)
        {
            Thread startThread = new Thread(new ThreadStart(Start));
            startThread.IsBackground = true;
            startThread.Start();
        }

        /// <summary>
        /// This method is called when a service gets a request to pause,
        /// but not stop completely.
        /// </summary>
        public virtual void Pause()
        {
            System.Diagnostics.Debug.Write(" Service Paused.");
        }

        /// <summary>
        /// This method is called when a service gets a request to resume
        /// after a pause is issued.
        /// </summary>
        public virtual void Continue()
        {
            System.Diagnostics.Debug.Write(" Service started Running.");
        }

        /// <summary>
        /// This method is called when the machine the service is running on
        /// is being shutdown.
        /// </summary>
        //public virtual void Shutdown()

        protected override void OnShutdown()
        {
            System.Diagnostics.Debug.Write(" Service Shutdown.");
        }

        protected override void OnStop()
        {
            Stop();
        }
    }
}