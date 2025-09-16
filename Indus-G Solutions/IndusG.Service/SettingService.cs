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
                using (var context = new DakSrong4NMEntities())
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
                //using (var context = new DakSrong4NMEntities())
                //{
                //    var parameters = context.DB_SesanParameter.FirstOrDefault();
                //    if (parameters == null)
                //    {
                //        context.DB_SesanParameter.Add(MapperHelper.Mapper.Map<SettingModel, DB_SesanParameter>(settingModel));
                //    }
                //    else
                //    {
                //        if (WorkContext.IsAdmin())
                //        {
                //            parameters.K_ChuaCoHep = settingModel.K_ChuaCoHep;
                //            parameters.K_CoHepNgang = settingModel.K_CoHepNgang;
                //            parameters.K_CoHepDung = settingModel.K_CoHepDung;
                //            parameters.K_LuuLuong = settingModel.K_LuuLuong;
                //            parameters.H_MayPhat = settingModel.H_MayPhat;
                //            parameters.H_CoKhi = settingModel.H_CoKhi;
                //            parameters.H_Turbine = settingModel.H_Turbine;
                //            parameters.CaoTrinhNguongTran = settingModel.CaoTrinhNguongTran;
                //            parameters.ChieuDaiDapTran = settingModel.ChieuDaiDapTran;
                //            parameters.CaoTrinhNguongKenhXa = settingModel.CaoTrinhNguongKenhXa;
                //            parameters.ChieuRongKenhXa = settingModel.ChieuRongKenhXa;
                //            parameters.DungTichHuuIch = settingModel.DungTichHuuIch;
                //            //parameters.DungTichHuuIch1 = settingModel.DungTichHuuIch1;
                //            //parameters.DungTichHuuIch2 = settingModel.DungTichHuuIch2;
                //            parameters.MucNuocChet = settingModel.MucNuocChet;
                //            parameters.DungTichHoMNC = settingModel.DungTichHoMNC;
                //            parameters.K_DCTT = settingModel.K_DCTT;
                //            parameters.DCTT_QuyDinh = settingModel.DCTT_QuyDinh;
                //            parameters.Nhamay = settingModel.Nhamay;
                //            parameters.DungTich1 = settingModel.DungTich1;
                //            parameters.DungTich2 = settingModel.DungTich2;
                //            parameters.DungTich3 = settingModel.DungTich3;
                //            parameters.DungTich4 = settingModel.DungTich4;
                //        }
                        
                //        parameters.DCTT_Toggle = settingModel.DCTT_Toggle;
                //    }
                //    context.SaveChanges();
                //}
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
            return null;
            //    using (var context = new DakSrong4NMEntities())
            //    {
            //        var settingModel = new SettingModel();

            //        var plcConfiguration = context.DB_SesanPLCConfiguration.FirstOrDefault();
            //        if (plcConfiguration != null)
            //        {
            //            settingModel.PLCSetting = MapperHelper.Mapper.Map<DB_SesanPLCConfiguration, PLCSettingModel>(plcConfiguration);
            //        } else
            //        {
            //            settingModel.PLCSetting = new PLCSettingModel();
            //        }

            //        var parameters = context.DB_SesanParameter.FirstOrDefault();
            //        if (parameters != null)
            //        {
            //            settingModel.K_ChuaCoHep = parameters.K_ChuaCoHep;
            //            settingModel.K_CoHepNgang = parameters.K_CoHepNgang;
            //            settingModel.K_CoHepDung = parameters.K_CoHepDung;
            //            settingModel.K_LuuLuong = parameters.K_LuuLuong;
            //            settingModel.H_MayPhat = parameters.H_MayPhat;
            //            settingModel.H_CoKhi = parameters.H_CoKhi;
            //            settingModel.H_Turbine = parameters.H_Turbine;
            //            settingModel.CaoTrinhNguongTran = parameters.CaoTrinhNguongTran;
            //            settingModel.ChieuDaiDapTran = parameters.ChieuDaiDapTran;
            //            settingModel.CaoTrinhNguongKenhXa = parameters.CaoTrinhNguongKenhXa;
            //            settingModel.ChieuRongKenhXa = parameters.ChieuRongKenhXa;
            //            settingModel.DungTichHuuIch = parameters.DungTichHuuIch;
            //            settingModel.MucNuocChet = parameters.MucNuocChet;
            //            settingModel.DungTichHoMNC = parameters.DungTichHoMNC;
            //            settingModel.K_DCTT = parameters.K_DCTT;
            //            settingModel.DCTT_QuyDinh = parameters.DCTT_QuyDinh;
            //            settingModel.Nhamay = parameters.Nhamay;
            //            settingModel.DCTT_Toggle = parameters.DCTT_Toggle.HasValue ? parameters.DCTT_Toggle.Value : false;
            //            settingModel.DungTich1 = parameters.DungTich1;
            //            settingModel.DungTich2 = parameters.DungTich2;
            //            settingModel.DungTich3 = parameters.DungTich3;
            //            settingModel.DungTich4 = parameters.DungTich4;

            //            //settingModel.K_ChuaCoHep = parameters.K_ChuaCoHep.HasValue ? parameters.K_ChuaCoHep.Value : default;
            //            //settingModel.K_CoHepNgang = parameters.K_CoHepNgang.HasValue ? parameters.K_CoHepNgang.Value : default;
            //            //settingModel.K_CoHepDung = parameters.K_CoHepDung.HasValue ? parameters.K_CoHepDung.Value : default;
            //            //settingModel.K_LuuLuong = parameters.K_LuuLuong.HasValue ? parameters.K_LuuLuong.Value : default;
            //            //settingModel.H_MayPhat = parameters.H_MayPhat.HasValue ? parameters.H_MayPhat.Value : default;
            //            //settingModel.H_CoKhi = parameters.H_CoKhi.HasValue ? parameters.H_CoKhi.Value : default;
            //            //settingModel.H_Turbine = parameters.H_Turbine.HasValue ? parameters.H_Turbine.Value : default;
            //            //settingModel.CaoTrinhNguongTran = parameters.CaoTrinhNguongTran.HasValue ? parameters.CaoTrinhNguongTran.Value : default;
            //            //settingModel.ChieuDaiDapTran = parameters.ChieuDaiDapTran.HasValue ? parameters.ChieuDaiDapTran.Value : default;
            //            //settingModel.CaoTrinhNguongKenhXa = parameters.CaoTrinhNguongKenhXa.HasValue ? parameters.CaoTrinhNguongKenhXa.Value : default;
            //            //settingModel.ChieuRongKenhXa = parameters.ChieuRongKenhXa.HasValue ? parameters.ChieuRongKenhXa.Value : default;
            //            //settingModel.DungTichHuuIch1 = parameters.DungTichHuuIch1.HasValue ? parameters.DungTichHuuIch1.Value : default;
            //            //settingModel.DungTichHuuIch2 = parameters.DungTichHuuIch2.HasValue ? parameters.DungTichHuuIch2.Value : default;
            //            //settingModel.MucNuocChet = parameters.MucNuocChet.HasValue ? parameters.MucNuocChet.Value : default;
            //            //settingModel.DungTichHoMNC = parameters.DungTichHoMNC.HasValue ? parameters.DungTichHoMNC.Value : default;
            //            //settingModel.K_DCTT = parameters.K_DCTT.HasValue ? parameters.K_DCTT.Value : default;
            //            //settingModel.DCTT_QuyDinh = parameters.DCTT_QuyDinh.HasValue ? parameters.DCTT_QuyDinh.Value : default;
            //            //settingModel.Nhamay = parameters.Nhamay;
            //            //settingModel.DCTT_Toggle = parameters.DCTT_Toggle.HasValue ? parameters.DCTT_Toggle.Value : false;
            //        }
            //        settingModel.PLCLiveBit = (new PlcService()).GetCurrentPLCStatus();
            //        return settingModel;
            //    }
        }

    }
}
