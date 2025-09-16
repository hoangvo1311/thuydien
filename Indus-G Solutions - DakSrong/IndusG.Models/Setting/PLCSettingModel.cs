using System;
using System.ComponentModel;
using static IndusG.Models.Enums;

namespace IndusG.Models.Setting
{
    public class PLCSettingModel
    {
        [DisplayName("CPU Type")]
        public CPUType CPUType { get; set; }

        public int Rack { get; set; }

        public int Slot { get; set; }

        [DisplayName("IP Address")]
        public string IPAddress { get; set; }
        public bool ScheduleBit { get; set; }

        [DisplayName("Schedule Switch")]
        public bool ScheduleSwitch { get; set; }
        public int ScheduleDayOn { get; set; }
        public int ScheduleDayOff { get; set; }
        public TimeSpan ScheduleTimeOn { get; set; }
        public TimeSpan ScheduleTimeOff { get; set; }

    }

    public class  PLCMonitoringModel
    {
        public double DrainLevel1 { get; set; }
        public double DrainLevel2 { get; set; }
        public bool RemoteBit { get; set; }
        public bool ManBit { get; set; }
        public bool SimulationBit { get; set; }
        public bool CV1Bottom { get; set; }
        public bool CV1Overload { get; set; }
        public bool CV1Running { get; set; }
        public bool CV2Bottom { get; set; }
        public bool CV2Overload { get; set; }
        public bool CV2Running { get; set; }
        public bool KU1 { get; set; }
        public bool KD1 { get; set; }
        public bool KU2 { get; set; }
        public bool KD2 { get; set; }

        public bool KU1_SIM { get; set; }
        public bool KD1_SIM { get; set; }
        public bool KU2_SIM { get; set; }
        public bool KD2_SIM { get; set; }

        public bool PLCLiveBit { get; set; }
        public bool ScheduleBit { get; set; }

    }
}
