using IndusG.Service;
using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestS7
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggerHelper.Info("Start Read PLC Service!");

            try
            {
                var plcDriver = new Plc(CpuType.S71200,
                         "192.168.1.200",
                         (short)0,
                         (short)1);
                plcDriver.Open();

                if (plcDriver.IsConnected)
                {
                    LoggerHelper.Info("Connected!");
                    var Upstream = Math.Round(((uint)plcDriver.Read("DB5.DBD0")).ConvertToFloat(), 2);
                    var Downstream = Math.Round(((uint)plcDriver.Read("DB5.DBD4")).ConvertToFloat(), 2);
                    var H1_MW = Math.Round(((uint)plcDriver.Read("DB5.DBD12")).ConvertToFloat(), 2);
                    var H2_MW = Math.Round(((uint)plcDriver.Read("DB5.DBD16")).ConvertToFloat(), 2);
                    var H3_MW = Math.Round(((uint)plcDriver.Read("DB5.DBD20")).ConvertToFloat(), 2);
                    LoggerHelper.Info($"Upstream: {Upstream}  -  Downstream: {Downstream}  -  H1_MW: {H1_MW}");
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.Info($"error: {ex.Message}");
            }
            LoggerHelper.Info("End Read PLC Service!");
        }

        public CpuType GetS7CPUType(IndusG.Models.Enums.CPUType cpuType)
        {
            switch (cpuType)
            {
                case IndusG.Models.Enums.CPUType.S71200:
                    return CpuType.S71200;
                case IndusG.Models.Enums.CPUType.S71500:
                    return CpuType.S71500;
                case IndusG.Models.Enums.CPUType.S7200:
                    return CpuType.S7200;
                case IndusG.Models.Enums.CPUType.S7300:
                    return CpuType.S7300;
                case IndusG.Models.Enums.CPUType.S7400:
                    return CpuType.S7400;
                default:
                    return CpuType.S7200;
            }
        }

    }
}
