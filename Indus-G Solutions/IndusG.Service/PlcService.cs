using IndusG.DataAccess;
using IndusG.Models.Setting;
using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndusG.Service
{
    public class PlcService
    {
        public ResponseData TestConnection(PLCSettingModel plcSettingModel)
        {
            try
            {
                var plc = new Plc(GetS7CPUType(plcSettingModel.CPUType),
                        plcSettingModel.IPAddress,
                        (short)plcSettingModel.Rack,
                        (short)plcSettingModel.Slot);
                if (!plc.IsAvailable)
                {
                    return new ResponseData
                    {
                        Result = false,
                        Message = "PLC not available!"
                    };
                }

                plc.Open();
                plc.Close();
            }
            catch (Exception ex)
            {
                return new ResponseData
                {
                    Result = false,
                    Message = ex.Message
                };
            }

            return new ResponseData
            {
                Result = true,
                Message = "Kết nối với PLC thành công!"
            };
        }

        public Plc InitPLCDriver()
        {
            try
            {
                LoggerHelper.Info("InitPLCDriver");
                using (var context = new DakSrong4NMEntities())
                {
                    var plcSetting = context.DB_SesanPLCConfiguration.FirstOrDefault();
                    var plc = new Plc(GetS7CPUType((Models.Enums.CPUType)plcSetting.CPUType),
                        plcSetting.IPAddress,
                        (short)plcSetting.Rack,
                        (short)plcSetting.Slot);
                    return plc;
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.Error($"Error while init PLC driver. {ex.Message}");
                throw;
            }
        }

        public CpuType GetS7CPUType(IndusG.Models.Enums.CPUType cpuType)
        {
            switch (cpuType)
            {
                case Models.Enums.CPUType.S71200:
                    return CpuType.S71200;
                case Models.Enums.CPUType.S71500:
                    return CpuType.S71500;
                case Models.Enums.CPUType.S7200:
                    return CpuType.S7200;
                case Models.Enums.CPUType.S7300:
                    return CpuType.S7300;
                case Models.Enums.CPUType.S7400:
                    return CpuType.S7400;
                default:
                    return CpuType.S7200;
            }
        }

        public bool CheckPLCAvailable()
        {
            using (var context = new DakSrong4NMEntities())
            {
                var plcSetting = context.DB_SesanPLCConfiguration.FirstOrDefault();
                try
                {
                    var plc = new Plc(GetS7CPUType((Models.Enums.CPUType)plcSetting.CPUType),
                        plcSetting.IPAddress,
                        (short)plcSetting.Rack,
                        (short)plcSetting.Slot);
                    if (!plc.IsAvailable)
                    {
                        return false;
                    }

                    plc.Open();
                    plc.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool GetCurrentPLCStatus()
        {
            using (var context = new DakSrong4NMEntities())
            {
                var plcSetting = context.DB_SesanPLCConfiguration.FirstOrDefault();
                return plcSetting.Status.HasValue ? plcSetting.Status.Value : false;
            }
        }

        public void UpdatePLCStatus(bool isAvailable)
        {
            using (var context = new DakSrong4NMEntities())
            {
                var plcSetting = context.DB_SesanPLCConfiguration.FirstOrDefault();
                plcSetting.Status = isAvailable;
                context.SaveChanges();
            }
        }

        public bool GetCurrentServiceStatus()
        {
            using (var context = new DakSrong4NMEntities())
            {
                var service = context.DB_SesanService.First();
                return service.ServiceStatus.HasValue ? service.ServiceStatus.Value : false;
            }
        }

        public void SetServiceStatus(bool status)
        {
            using (var context = new DakSrong4NMEntities())
            {
                var service = context.DB_SesanService.First();
                service.ServiceStatus = status;
                context.SaveChanges();
            }
        }
    }
}
