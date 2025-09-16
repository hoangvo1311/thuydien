using System;

namespace IndusG.ServiceFrameWork
{
    /// <summary>
    /// The interface that any windows service should implement to be used
    /// with the GenericWindowsService executable.
    /// </summary>
    public interface IIndusGService
    {
        /// <summary>
        /// Service name
        /// </summary>
        string ServiceName { get; }

        /// <summary>
        /// Check if service installed already
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        bool IsServiceInstalled();

        /// <summary>
        /// Determines whether the specified service is running.
        /// </summary>
        /// <param name="serviceName">
        /// Name of the service.
        /// </param>
        bool IsServiceRunning();

        /// <summary>
        /// Returns the specified service status
        /// </summary>
        string GetServiceStatus();

        /// <summary>
        ///  This method is called when the service gets a request to Start.
        /// </summary>
        void Start();

        /// <summary>
        /// This method is called when the service gets a request to stop.
        /// </summary>
        void Stop();

        /// <summary>
        /// This method is called when a service gets a request to pause,
        /// but not stop completely.
        /// </summary>
        void Pause();

        /// <summary>
        /// This method is called when a service gets a request to resume
        /// after a pause is issued.
        /// </summary>
        void Continue();
    }
}