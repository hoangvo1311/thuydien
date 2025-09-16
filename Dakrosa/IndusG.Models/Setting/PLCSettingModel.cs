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
    }
}
