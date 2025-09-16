using System;
using System.ServiceProcess;

namespace IndusG.ServiceFrameWork
{
    /// <summary>
    /// A generic Windows Service that can handle any assembly that
    /// implements IWindowsService (including AbstractWindowsService)
    /// </summary>
    public abstract class IndusGService : ServiceBase
    {
        /// <summary>
        /// Constructor a generic windows service from the given class
        /// </summary>
        /// <param name="serviceImplementation">Service implementation.</param>
        protected IndusGService()
        {
            // configure the service
            ConfigureServiceFromAttributes();
        }

        /// <summary>
        /// </summary>
        /// Set configuration data
        /// </summary>
        /// <param name="serviceImplementation">The service with configuration settings.</param>
        private void ConfigureServiceFromAttributes()
        {
            var attribute = this.GetType().GetAttribute<ServiceAttribute>();

            if (attribute != null)
            {
                if (!string.IsNullOrWhiteSpace(attribute.EventLogSource))
                {
                    EventLog.Source = attribute.EventLogSource;
                }

                CanStop = attribute.CanStop;
                CanPauseAndContinue = attribute.CanPauseAndContinue;
                CanShutdown = attribute.CanShutdown;
                AutoLog = true;
            }
        }
    }
}