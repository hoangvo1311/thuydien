using IndusG.DataAccess;
using IndusG.Models;
using IndusG.Models.Setting;
using S7.Net;
using System;
using System.Linq;

namespace IndusG.Service
{
    public class SettingService
    {
       

        public ResponseData SaveConnection(PLCSettingModel plcSettingModel)
        {
            try
            {
                using (var context = new QuantracEntities())
                {
                    var plcConfig = context.DB_SesanPLCConfiguration.FirstOrDefault();
                    var plcService = new PlcService();
                    //var testConnection = plcService.TestConnection(plcSettingModel);
                    if (plcConfig == null)
                    {
                        context.DB_SesanPLCConfiguration.Add(new DB_SesanPLCConfiguration
                        {
                            CPUType = (int)plcSettingModel.CPUType,
                            IPAddress = plcSettingModel.IPAddress,
                            Rack = plcSettingModel.Rack,
                            Slot = plcSettingModel.Slot,
                            Status = true,
                            ScheduleDayOn = plcSettingModel.ScheduleDayOn,
                            ScheduleDayOff = plcSettingModel.ScheduleDayOff,
                            ScheduleTimeOn = plcSettingModel.ScheduleTimeOn,
                            ScheduleTimeOff = plcSettingModel.ScheduleTimeOff,
                            ScheduleSwitch = plcSettingModel.ScheduleSwitch
                        });
                    }
                    else
                    {
                        plcConfig.CPUType = (int)plcSettingModel.CPUType;
                        plcConfig.IPAddress = plcSettingModel.IPAddress;
                        plcConfig.Rack = plcSettingModel.Rack;
                        plcConfig.Slot = plcSettingModel.Slot;
                        plcConfig.ScheduleDayOn = plcSettingModel.ScheduleDayOn;
                        plcConfig.ScheduleDayOff = plcSettingModel.ScheduleDayOff;
                        plcConfig.ScheduleTimeOn = plcSettingModel.ScheduleTimeOn;
                        plcConfig.ScheduleTimeOff = plcSettingModel.ScheduleTimeOff;
                        plcConfig.ScheduleSwitch = plcSettingModel.ScheduleSwitch;
                        //plcConfig.Status = testConnection.Result;
                    }
                    context.SaveChanges();
                    PlcService.ResetPLCDriver();
                    //plcService.UpdatePLCStatus(testConnection.Result);
                }

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
                Message = "Lưu cấu hình PLC thành công!"
            };
        }

        public ResponseData SaveParameters(SettingModel settingModel)
        {
            try
            {
                using (var context = new QuantracEntities())
                {
                    var parameter = context.DB_SesanParameter.FirstOrDefault();
                    if (parameter == null)
                    {
                        context.DB_SesanParameter.Add(MapperHelper.Mapper.Map<SettingModel, DB_SesanParameter>(settingModel));
                    }
                    else
                    {
                        if (WorkContext.IsAdmin())
                        {
                            parameter.K_ChuaCoHep = settingModel.K_ChuaCoHep;
                            parameter.K_CoHepNgang = settingModel.K_CoHepNgang;
                            parameter.K_CoHepDung = settingModel.K_CoHepDung;
                            parameter.K_LuuLuong = settingModel.K_LuuLuong;
                            parameter.H_MayPhat = settingModel.H_MayPhat;
                            parameter.H_MayPhat2 = settingModel.H_MayPhat2;
                            parameter.H_MayPhat3 = settingModel.H_MayPhat3;
                            parameter.H_CoKhi = settingModel.H_CoKhi;
                            parameter.H_Turbine = settingModel.H_Turbine;
                            parameter.CaoTrinhNguongTran = settingModel.CaoTrinhNguongTran;
                            parameter.ChieuDaiDapTran = settingModel.ChieuDaiDapTran;
                            parameter.CaoTrinhNguongKenhXa = settingModel.CaoTrinhNguongKenhXa;
                            parameter.ChieuRongKenhXa = settingModel.ChieuRongKenhXa;
                            parameter.DungTichHuuIch = settingModel.DungTichHuuIch;
                            parameter.MucNuocChet = settingModel.MucNuocChet;
                            parameter.DungTichHoMNC = settingModel.DungTichHoMNC;
                            parameter.DCTT_QuyDinh = settingModel.DCTT_QuyDinh;
                            parameter.LuuLuongKhongTaiH1 = settingModel.LuuLuongKhongTaiH1;
                            parameter.LuuLuongKhongTaiH2 = settingModel.LuuLuongKhongTaiH2;
                            parameter.LuuLuongKhongTaiH3 = settingModel.LuuLuongKhongTaiH3;
                            parameter.DCTT_TrungGian = settingModel.DCTT_TrungGian;
                            parameter.Nhamay = settingModel.Nhamay;
                        }
                        parameter.DCTT_Toggle = settingModel.DCTT_Toggle;
                        parameter.K_DCTT = settingModel.K_DCTT;
                    }
                    context.SaveChanges();
                }
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
                Message = "Lưu thông số cài đặt thành công!"
            };
        }

        public SettingModel GetSettingModel()
        {
            using (var context = new QuantracEntities())
            {
                var settingModel = new SettingModel();

                var plcConfiguration = context.DB_SesanPLCConfiguration.FirstOrDefault();
                if (plcConfiguration != null)
                {
                    settingModel.PLCSetting = MapperHelper.Mapper.Map<DB_SesanPLCConfiguration, PLCSettingModel>(plcConfiguration);
                    settingModel.PLCLiveBit = plcConfiguration?.Status ?? false;

                }
                else
                {
                    settingModel.PLCSetting = new PLCSettingModel();
                }

                var parameters = context.DB_SesanParameter.FirstOrDefault();
                if (parameters != null)
                {
                    settingModel.K_ChuaCoHep = parameters.K_ChuaCoHep.HasValue ? parameters.K_ChuaCoHep.Value : default;
                    settingModel.K_CoHepNgang = parameters.K_CoHepNgang.HasValue ? parameters.K_CoHepNgang.Value : default;
                    settingModel.K_CoHepDung = parameters.K_CoHepDung.HasValue ? parameters.K_CoHepDung.Value : default;
                    settingModel.K_LuuLuong = parameters.K_LuuLuong.HasValue ? parameters.K_LuuLuong.Value : default;
                    settingModel.H_MayPhat = parameters.H_MayPhat.HasValue ? parameters.H_MayPhat.Value : default;
                    settingModel.H_MayPhat2 = parameters.H_MayPhat2.HasValue ? parameters.H_MayPhat2.Value : default;
                    settingModel.H_MayPhat3 = parameters.H_MayPhat3.HasValue ? parameters.H_MayPhat3.Value : default;
                    settingModel.H_CoKhi = parameters.H_CoKhi.HasValue ? parameters.H_CoKhi.Value : default;
                    settingModel.H_Turbine = parameters.H_Turbine.HasValue ? parameters.H_Turbine.Value : default;
                    settingModel.CaoTrinhNguongTran = parameters.CaoTrinhNguongTran.HasValue ? parameters.CaoTrinhNguongTran.Value : default;
                    settingModel.ChieuDaiDapTran = parameters.ChieuDaiDapTran.HasValue ? parameters.ChieuDaiDapTran.Value : default;
                    settingModel.CaoTrinhNguongKenhXa = parameters.CaoTrinhNguongKenhXa.HasValue ? parameters.CaoTrinhNguongKenhXa.Value : default;
                    settingModel.ChieuRongKenhXa = parameters.ChieuRongKenhXa.HasValue ? parameters.ChieuRongKenhXa.Value : default;
                    settingModel.DungTichHuuIch = parameters.DungTichHuuIch.HasValue ? parameters.DungTichHuuIch.Value : default;
                    settingModel.MucNuocChet = parameters.MucNuocChet.HasValue ? parameters.MucNuocChet.Value : default;
                    settingModel.DungTichHoMNC = parameters.DungTichHoMNC.HasValue ? parameters.DungTichHoMNC.Value : default;
                    settingModel.K_DCTT = parameters.K_DCTT.HasValue ? parameters.K_DCTT.Value : default;
                    settingModel.DCTT_QuyDinh = parameters.DCTT_QuyDinh.HasValue ? parameters.DCTT_QuyDinh.Value : default;
                    settingModel.Nhamay = parameters.Nhamay;
                    settingModel.DCTT_Toggle = parameters.DCTT_Toggle.HasValue ? parameters.DCTT_Toggle.Value : false;
                    settingModel.LuuLuongKhongTaiH1 = parameters.LuuLuongKhongTaiH1.HasValue ? parameters.LuuLuongKhongTaiH1.Value : default;
                    settingModel.LuuLuongKhongTaiH2 = parameters.LuuLuongKhongTaiH2.HasValue ? parameters.LuuLuongKhongTaiH2.Value : default;
                    settingModel.LuuLuongKhongTaiH3 = parameters.LuuLuongKhongTaiH3.HasValue ? parameters.LuuLuongKhongTaiH3.Value : default;
                    settingModel.DCTT_TrungGian = parameters.DCTT_TrungGian.HasValue ? parameters.DCTT_TrungGian.Value : default;
                }

                return settingModel;
            }
        }

    }
}
