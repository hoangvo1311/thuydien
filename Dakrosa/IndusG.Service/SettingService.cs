using IndusG.DataAccess;
using IndusG.Models;
using IndusG.Models.Setting;
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
                    var plcConfig = context.DB_PLCConfiguration.FirstOrDefault();
                    var plcService = new PlcService();
                    //var testConnection = plcService.TestConnection(plcSettingModel);
                    if (plcConfig == null)
                    {
                        context.DB_PLCConfiguration.Add(new DB_PLCConfiguration
                        {
                            CPUType = (int)plcSettingModel.CPUType,
                            IPAddress = plcSettingModel.IPAddress,
                            Rack = plcSettingModel.Rack,
                            Slot = plcSettingModel.Slot,
                            Status = true
                        });
                    }
                    else
                    {
                        plcConfig.CPUType = (int)plcSettingModel.CPUType;
                        plcConfig.IPAddress = plcSettingModel.IPAddress;
                        plcConfig.Rack = plcSettingModel.Rack;
                        plcConfig.Slot = plcSettingModel.Slot;
                        //plcConfig.Status = testConnection.Result;
                    }
                    context.SaveChanges();
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
                    var parameters = context.DB_Parameter.FirstOrDefault();
                    if (parameters == null)
                    {
                        context.DB_Parameter.Add(MapperHelper.Mapper.Map<SettingModel, DB_Parameter>(settingModel));
                    }
                    else
                    {
                        if (WorkContext.CanUpdateK_ChuaCoHep())
                        {
                            parameters.K_ChuaCoHep = settingModel.K_ChuaCoHep;
                        }

                        if (WorkContext.CanUpdateK_CoHepNgang())
                        {
                            parameters.K_CoHepNgang = settingModel.K_CoHepNgang;
                        }

                        if (WorkContext.CanUpdateK_CoHepDung())
                        {
                            parameters.K_CoHepDung = settingModel.K_CoHepDung;
                        }

                        if (WorkContext.CanUpdateK_LuuLuong())
                        {
                            parameters.K_LuuLuong = settingModel.K_LuuLuong;
                        }

                        if (WorkContext.CanUpdateH_MayPhat())
                        {
                            parameters.H_MayPhat = settingModel.H_MayPhat;
                        }

                        if (WorkContext.CanUpdateH_CoKhi())
                        {
                            parameters.H_CoKhi = settingModel.H_CoKhi;
                        }

                        if (WorkContext.CanUpdateH_Turbine())
                        {
                            parameters.H_Turbine = settingModel.H_Turbine;
                        }

                        if (WorkContext.CanUpdateCaoTrinhNguongTran())
                        {
                            parameters.CaoTrinhNguongTran = settingModel.CaoTrinhNguongTran;
                        }

                        if (WorkContext.CanUpdateChieuDaiDapTran())
                        {
                            parameters.ChieuDaiDapTran = settingModel.ChieuDaiDapTran;
                        }

                        if (WorkContext.CanUpdateCaoTrinhNguongKenhXa())
                        {
                            parameters.CaoTrinhNguongKenhXa = settingModel.CaoTrinhNguongKenhXa;
                        }

                        if (WorkContext.CanUpdateChieuRongKenhXa())
                        {
                            parameters.ChieuRongKenhXa = settingModel.ChieuRongKenhXa;
                        }

                        if (WorkContext.CanUpdateDungTichHuuIch1())
                        {
                            parameters.DungTichHuuIch1 = settingModel.DungTichHuuIch1;
                        }

                        if (WorkContext.CanUpdateDungTichHuuIch2())
                        {
                            parameters.DungTichHuuIch2 = settingModel.DungTichHuuIch2;
                        }

                        if (WorkContext.CanUpdateDungTichHuuIch3())
                        {
                            parameters.DungTichHuuIch3 = settingModel.DungTichHuuIch3;
                        }

                        if (WorkContext.CanUpdateDungTichHuuIch4())
                        {
                            parameters.DungTichHuuIch4 = settingModel.DungTichHuuIch4;
                        }

                        if (WorkContext.CanUpdateMucNuocChet())
                        {
                            parameters.MucNuocChet = settingModel.MucNuocChet;
                        }

                        if (WorkContext.CanUpdateDungTichHoMNC())
                        {
                            parameters.DungTichHoMNC = settingModel.DungTichHoMNC;
                        }

                        if (WorkContext.CanUpdateK_DCTT())
                        {
                            parameters.K_DCTT = settingModel.K_DCTT;
                        }

                        if (WorkContext.CanUpdateDCTT_QuyDinh())
                        {
                            parameters.DCTT_QuyDinh = settingModel.DCTT_QuyDinh;
                        }

                        if (WorkContext.CanUpdateSampleSize())
                        {
                            parameters.SampleSize = settingModel.SampleSize;
                        }

                        if (WorkContext.CanUpdateDCTT_Toggle())
                        {
                            parameters.DCTT_Toggle = settingModel.DCTT_Toggle;
                        }

                        if (WorkContext.CanUpdateUpstream_Cal_Toggle())
                        {
                            parameters.Upstream_Cal_Toggle = settingModel.Upstream_Cal_Toggle;
                        }

                        //parameters.Nhamay = settingModel.Nhamay;


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

        public void SyncConnectionToLocal(DB_PLCConfiguration plcSetting)
        {
            try
            {
                using (var context = new QuantracEntities())
                {
                    var plcConfig = context.DB_PLCConfiguration.FirstOrDefault();
                    var plcService = new PlcService();
                    if (plcConfig == null)
                    {
                        context.DB_PLCConfiguration.Add(new DB_PLCConfiguration
                        {
                            CPUType = (int)plcSetting.CPUType,
                            IPAddress = plcSetting.IPAddress,
                            Rack = plcSetting.Rack,
                            Slot = plcSetting.Slot,
                            Status = true
                        });
                    }
                    else
                    {
                        plcConfig.CPUType = (int)plcSetting.CPUType;
                        plcConfig.IPAddress = plcSetting.IPAddress;
                        plcConfig.Rack = plcSetting.Rack;
                        plcConfig.Slot = plcSetting.Slot;
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.Error(ex.Message);
            }
        }

        public void SyncParametersToLocal(DB_Parameter settingModel)
        {
            try
            {
                using (var context = new QuantracEntities())
                {
                    var parameters = context.DB_Parameter.FirstOrDefault();
                    if (parameters == null)
                    {
                        context.DB_Parameter.Add(MapperHelper.Mapper.Map<DB_Parameter, DB_Parameter>(settingModel));
                    }
                    else
                    {
                        parameters.K_ChuaCoHep = settingModel.K_ChuaCoHep;
                        parameters.K_CoHepNgang = settingModel.K_CoHepNgang;
                        parameters.K_CoHepDung = settingModel.K_CoHepDung;
                        parameters.K_LuuLuong = settingModel.K_LuuLuong;
                        parameters.H_MayPhat = settingModel.H_MayPhat;
                        parameters.H_CoKhi = settingModel.H_CoKhi;
                        parameters.H_Turbine = settingModel.H_Turbine;
                        parameters.CaoTrinhNguongTran = settingModel.CaoTrinhNguongTran;
                        parameters.ChieuDaiDapTran = settingModel.ChieuDaiDapTran;
                        parameters.CaoTrinhNguongKenhXa = settingModel.CaoTrinhNguongKenhXa;
                        parameters.ChieuRongKenhXa = settingModel.ChieuRongKenhXa;
                        parameters.DungTichHuuIch1 = settingModel.DungTichHuuIch1;
                        parameters.DungTichHuuIch2 = settingModel.DungTichHuuIch2;
                        parameters.MucNuocChet = settingModel.MucNuocChet;
                        parameters.DungTichHoMNC = settingModel.DungTichHoMNC;
                        parameters.K_DCTT = settingModel.K_DCTT;
                        parameters.DCTT_QuyDinh = settingModel.DCTT_QuyDinh;
                        parameters.Nhamay = settingModel.Nhamay;
                        parameters.DCTT_Toggle = settingModel.DCTT_Toggle;
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.Error(ex.Message);
            }
        }

        public SettingModel GetSettingModel()
        {
            using (var context = new QuantracEntities())
            {
                var settingModel = new SettingModel();

                var plcConfiguration = context.DB_PLCConfiguration.FirstOrDefault();
                if (plcConfiguration != null)
                {
                    settingModel.PLCSetting = MapperHelper.Mapper.Map<DB_PLCConfiguration, PLCSettingModel>(plcConfiguration);
                }
                else
                {
                    settingModel.PLCSetting = new PLCSettingModel();
                }

                var parameters = context.DB_Parameter.FirstOrDefault();
                if (parameters != null)
                {
                    settingModel.K_ChuaCoHep = parameters.K_ChuaCoHep.HasValue ? parameters.K_ChuaCoHep.Value : default;
                    settingModel.K_CoHepNgang = parameters.K_CoHepNgang.HasValue ? parameters.K_CoHepNgang.Value : default;
                    settingModel.K_CoHepDung = parameters.K_CoHepDung.HasValue ? parameters.K_CoHepDung.Value : default;
                    settingModel.K_LuuLuong = parameters.K_LuuLuong.HasValue ? parameters.K_LuuLuong.Value : default;
                    settingModel.H_MayPhat = parameters.H_MayPhat.HasValue ? parameters.H_MayPhat.Value : default;
                    settingModel.H_CoKhi = parameters.H_CoKhi.HasValue ? parameters.H_CoKhi.Value : default;
                    settingModel.H_Turbine = parameters.H_Turbine.HasValue ? parameters.H_Turbine.Value : default;
                    settingModel.CaoTrinhNguongTran = parameters.CaoTrinhNguongTran.HasValue ? parameters.CaoTrinhNguongTran.Value : default;
                    settingModel.ChieuDaiDapTran = parameters.ChieuDaiDapTran.HasValue ? parameters.ChieuDaiDapTran.Value : default;
                    settingModel.CaoTrinhNguongKenhXa = parameters.CaoTrinhNguongKenhXa.HasValue ? parameters.CaoTrinhNguongKenhXa.Value : default;
                    settingModel.ChieuRongKenhXa = parameters.ChieuRongKenhXa.HasValue ? parameters.ChieuRongKenhXa.Value : default;
                    settingModel.DungTichHuuIch1 = parameters.DungTichHuuIch1.HasValue ? parameters.DungTichHuuIch1.Value : default;
                    settingModel.DungTichHuuIch2 = parameters.DungTichHuuIch2.HasValue ? parameters.DungTichHuuIch2.Value : default;
                    settingModel.DungTichHuuIch3 = parameters.DungTichHuuIch3.HasValue ? parameters.DungTichHuuIch3.Value : default;
                    settingModel.DungTichHuuIch4 = parameters.DungTichHuuIch4.HasValue ? parameters.DungTichHuuIch4.Value : default;
                    settingModel.SampleSize = parameters.SampleSize.HasValue ? parameters.SampleSize.Value : default;
                    settingModel.MucNuocChet = parameters.MucNuocChet.HasValue ? parameters.MucNuocChet.Value : default;
                    settingModel.DungTichHoMNC = parameters.DungTichHoMNC.HasValue ? parameters.DungTichHoMNC.Value : default;
                    settingModel.K_DCTT = parameters.K_DCTT.HasValue ? parameters.K_DCTT.Value : default;
                    settingModel.DCTT_QuyDinh = parameters.DCTT_QuyDinh.HasValue ? parameters.DCTT_QuyDinh.Value : default;
                    settingModel.Nhamay = parameters.Nhamay;
                    settingModel.DCTT_Toggle = parameters.DCTT_Toggle.HasValue ? parameters.DCTT_Toggle.Value : false;
                    settingModel.Upstream_Cal_Toggle = parameters.Upstream_Cal_Toggle.HasValue ? parameters.Upstream_Cal_Toggle.Value : false;
                }
                settingModel.PLCLiveBit = (new PlcService()).GetCurrentPLCStatus();
                return settingModel;
            }
        }

    }
}
