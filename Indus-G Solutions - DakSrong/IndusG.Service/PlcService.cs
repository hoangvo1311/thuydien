using IndusG.DataAccess;
using IndusG.Models.Setting;
using S7.Net;
using System;
using System.Linq;

namespace IndusG.Service
{
    public class PlcService : IDisposable
    {
        private static Plc _plcDriver;
        private static readonly object _lock = new object();
        public static bool IsWritingToPLC = false;

        internal static Plc PLCDriver
        {
            get
            {
                if (_plcDriver == null)
                {
                    _plcDriver = InitPLCDriver();
                }
                return _plcDriver;
            }
        }

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

        private static Plc InitPLCDriver()
        {
            try
            {
                using (var context = new QuantracEntities())
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

        public static CpuType GetS7CPUType(IndusG.Models.Enums.CPUType cpuType)
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
            using (var context = new QuantracEntities())
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

        public void SavePLCConfiguration(PLCSettingModel plcSettingModel)
        {
            using (var context = new QuantracEntities())
            {
                var plcSetting = context.DB_SesanPLCConfiguration.FirstOrDefault();
                plcSetting.CPUType = (int)plcSettingModel.CPUType;
                plcSetting.IPAddress = plcSettingModel.IPAddress;
                plcSetting.Rack = plcSettingModel.Rack;
                plcSetting.Slot = plcSettingModel.Slot;
                plcSetting.ScheduleSwitch = plcSettingModel.ScheduleSwitch;
                plcSetting.ScheduleDayOn = plcSettingModel.ScheduleDayOn;
                plcSetting.ScheduleDayOff = plcSettingModel.ScheduleDayOff;
                plcSetting.ScheduleTimeOn = plcSettingModel.ScheduleTimeOn;
                plcSetting.ScheduleTimeOff = plcSettingModel.ScheduleTimeOff;
                context.SaveChanges();
            }
        }

        public bool GetCurrentPLCStatus()
        {
            using (var context = new QuantracEntities())
            {
                var plcSetting = context.DB_SesanPLCConfiguration.FirstOrDefault();
                return plcSetting.Status.HasValue ? plcSetting.Status.Value : false;
            }
        }

        public DB_SesanPLCConfiguration GetPLCMonitoring()
        {
            using (var context = new QuantracEntities())
            {
                return context.DB_SesanPLCConfiguration.FirstOrDefault();
            }
        }

        public void UpdatePLCStatus(bool isAvailable)
        {
            using (var context = new QuantracEntities())
            {
                var plcSetting = context.DB_SesanPLCConfiguration.FirstOrDefault();
                plcSetting.Status = isAvailable;
                context.SaveChanges();
            }
        }

        public bool GetCurrentServiceStatus()
        {
            using (var context = new QuantracEntities())
            {
                var service = context.DB_SesanService.First();
                return service.ServiceStatus.HasValue ? service.ServiceStatus.Value : false;
            }
        }

        public void SetServiceStatus(bool status)
        {
            using (var context = new QuantracEntities())
            {
                var service = context.DB_SesanService.First();
                service.ServiceStatus = status;
                context.SaveChanges();
            }
        }

        public PLCMonitoringModel GetPLCMonitoringModel()
        {
            using (var context = new QuantracEntities())
            {
                var plcMonitor = context.DB_SesanPLCConfiguration.First();
                return new PLCMonitoringModel
                {
                    CV1Bottom = plcMonitor.CV1Bottom.HasValue ? plcMonitor.CV1Bottom.Value : false,
                    CV1Overload = plcMonitor.CV1Overload.HasValue ? plcMonitor.CV1Overload.Value : false,
                    CV1Running = plcMonitor.CV1Running.HasValue ? plcMonitor.CV1Running.Value : false,
                    CV2Bottom = plcMonitor.CV2Bottom.HasValue ? plcMonitor.CV2Bottom.Value : false,
                    CV2Overload = plcMonitor.CV2Overload.HasValue ? plcMonitor.CV2Overload.Value : false,
                    CV2Running = plcMonitor.CV2Running.HasValue ? plcMonitor.CV2Running.Value : false,
                    DrainLevel1 = plcMonitor.DrainLevel1.HasValue ? Math.Round(plcMonitor.DrainLevel1.Value * 100, 1) : Math.Round(0.0,2),
                    DrainLevel2 = plcMonitor.DrainLevel2.HasValue ? Math.Round(plcMonitor.DrainLevel2.Value * 100, 1) : Math.Round(0.0, 2),
                    RemoteBit = plcMonitor.RemoteBit.HasValue ? plcMonitor.RemoteBit.Value : false,
                    SimulationBit = plcMonitor.SimulationBit.HasValue ? plcMonitor.SimulationBit.Value : false,
                    KU1 = plcMonitor.KU1.HasValue ? plcMonitor.KU1.Value : false,
                    KD1 = plcMonitor.KD1.HasValue ? plcMonitor.KD1.Value : false,
                    KU2 = plcMonitor.KU2.HasValue ? plcMonitor.KU2.Value : false,
                    KD2 = plcMonitor.KD2.HasValue ? plcMonitor.KD2.Value : false,
                    KU1_SIM = plcMonitor.KU1_SIM.HasValue ? plcMonitor.KU1_SIM.Value : false,
                    KD1_SIM = plcMonitor.KD1_SIM.HasValue ? plcMonitor.KD1_SIM.Value : false,
                    KU2_SIM = plcMonitor.KU2_SIM.HasValue ? plcMonitor.KU2_SIM.Value : false,
                    KD2_SIM = plcMonitor.KD2_SIM.HasValue ? plcMonitor.KD2_SIM.Value : false,
                    PLCLiveBit = plcMonitor.Status.HasValue ? plcMonitor.Status.Value : false,
                    ScheduleBit = plcMonitor.ScheduleBit.HasValue ? plcMonitor.ScheduleBit.Value : false,
                };
            }

        }

        public void UpdatePLCMonitoring(PLCMonitoringModel model)
        {
            using (var context = new QuantracEntities())
            {
                var plcMonitor = context.DB_SesanPLCConfiguration.First();

                plcMonitor.CV1Bottom = model.CV1Bottom;
                plcMonitor.CV1Overload = model.CV1Overload;
                plcMonitor.CV1Running = model.CV1Running;
                plcMonitor.CV2Bottom = model.CV2Bottom;
                plcMonitor.CV2Overload = model.CV2Overload;
                plcMonitor.CV2Running = model.CV2Running;

                plcMonitor.DrainLevel1 = model.DrainLevel1;
                plcMonitor.DrainLevel2 = model.DrainLevel2;

                plcMonitor.RemoteBit = model.RemoteBit;
                plcMonitor.ManBit = model.ManBit;
                plcMonitor.SimulationBit = model.SimulationBit;
                plcMonitor.ScheduleBit = model.ScheduleBit;
                plcMonitor.Status = model.PLCLiveBit;

                plcMonitor.KU1 = model.KU1;
                plcMonitor.KD1 = model.KD1;
                plcMonitor.KU2 = model.KU2;
                plcMonitor.KD2 = model.KD2;

                plcMonitor.KU1_SIM = model.KU1_SIM;
                plcMonitor.KD1_SIM = model.KD1_SIM;
                plcMonitor.KU2_SIM = model.KU2_SIM;
                plcMonitor.KD2_SIM = model.KD2_SIM;

                context.SaveChanges();
            }
        }

        public static void ResetPLCDriver()
        {
            if (_plcDriver != null)
            {
                if (_plcDriver.IsConnected)
                    _plcDriver.Close();
                _plcDriver = null;
            }
        }

        public static ResponseData WriteToPLC(string address, object value)
        {
            IsWritingToPLC = true;

            lock (_lock)
            {
                try
                {
                    EnsureConnection();

                    if (!PLCDriver.IsConnected)
                    {
                        return new ResponseData
                        {
                            Result = false,
                            Message = "Không thể kết nối PLC."
                        };
                    }

                    if (value is bool)
                    {
                        PLCDriver.Write(address, (bool)value);
                    }
                    else if (value is float || value is double || value is decimal)
                    {
                        PLCDriver.Write(address, Convert.ToSingle(value));
                    }
                    else
                    {
                        return new ResponseData
                        {
                            Result = false,
                            Message = "Unsupported data type. Only bool and real (float) are supported."
                        };
                    }

                    return new ResponseData
                    {
                        Result = true,
                        Message = $"Successfully wrote {value} to {address}."
                    };
                }
                catch (Exception ex)
                {
                    return new ResponseData
                    {
                        Result = false,
                        Message = $"Lỗi ghi xuống PLC: {ex.Message}"
                    };
                }
                finally
                {
                    IsWritingToPLC = false;
                }
            }
        }


        public static double ReadReal(string address)
        {
            lock (_lock)
            {
                EnsureConnection();

                var raw = (uint)PLCDriver.Read(address);
                return Math.Round(raw.ConvertToFloat(), 2);
            }
        }

        public static bool ReadBool(string address)
        {
            lock (_lock)
            {
                EnsureConnection();

                var value = PLCDriver.Read(address);
                return Convert.ToBoolean(value);
            }
        }

        private static void EnsureConnection()
        {
            lock (_lock)
            {
                if (_plcDriver == null)
                {
                    _plcDriver = InitPLCDriver();
                }

                if (!_plcDriver.IsConnected)
                {
                    try
                    {
                        _plcDriver.Open();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Unable to connect to PLC.", ex);
                    }
                }
            }
        }

        public void Dispose()
        {
            ResetPLCDriver();

            GC.SuppressFinalize(this);
        }
    }
}
