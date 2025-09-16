using IndusG.Models.Setting;

namespace IndusG.Models
{
    public class ServiceStatusModel
    {
        public bool IsServiceRunning { get; set; }
        public bool IsPLCAvailable { get; set; }
        public PLCMonitoringModel PLCMonitoring { get; set; }
        public double Qminflow { get; set; }

    }
}
