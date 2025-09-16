using IndusG.DataAccess;
using IndusG.Models;
using IndusG.Models.Setting;
using System;
using System.Linq;

namespace IndusG.Service
{
    public class IAMOSettingService
    {
        public IAMOConfigurationModel GetIAMOSettingModel()
        {
            using (var context = new QuantracEntities())
            {
                var settingModel = new IAMOConfigurationModel();

                var iamoPLCConfig = context.DB_IAMO_Configuration.FirstOrDefault();
                if (iamoPLCConfig != null)
                {
                    settingModel = MapperHelper.Mapper
                        .Map<DB_IAMO_Configuration, IAMOConfigurationModel>(iamoPLCConfig);
                }
                return settingModel;
            }
        }

        public ResponseData SaveIAMO_Configuration(IAMOConfigurationModel iamoConfigurationModel)
        {
            try
            {
                using (var context = new QuantracEntities())
                {
                    var IAMOSetting = context.DB_IAMO_Configuration.FirstOrDefault();
                    if (IAMOSetting == null)
                    {
                        context.DB_IAMO_Configuration
                            .Add(MapperHelper.Mapper.Map<IAMOConfigurationModel, DB_IAMO_Configuration>(iamoConfigurationModel));
                    }
                    else
                    {
                        if (WorkContext.IsAdmin())
                        {
                            IAMOSetting.SlaveID1 = iamoConfigurationModel.SlaveID1;
                            IAMOSetting.SlaveID2 = iamoConfigurationModel.SlaveID2;
                            IAMOSetting.AddressP1 = iamoConfigurationModel.AddressP1;
                            IAMOSetting.AddressP2 = iamoConfigurationModel.AddressP2;
                            IAMOSetting.AddressQ1 = iamoConfigurationModel.AddressQ1;
                            IAMOSetting.AddressQ2 = iamoConfigurationModel.AddressQ2;
                            IAMOSetting.TiSoP1 = iamoConfigurationModel.TiSoP1;
                            IAMOSetting.TiSoP2 = iamoConfigurationModel.TiSoP2;
                            IAMOSetting.TiSoQ1 = iamoConfigurationModel.TiSoQ1;
                            IAMOSetting.TiSoQ2 = iamoConfigurationModel.TiSoQ2;
                            IAMOSetting.BasedOnTiSo = iamoConfigurationModel.BasedOnTiSo;
                        }

                        IAMOSetting.HieuSuatMayPhat = iamoConfigurationModel.HieuSuatMayPhat;
                        IAMOSetting.HieuSuatCoKhi = iamoConfigurationModel.HieuSuatCoKhi;
                        IAMOSetting.HieuSuatTurbine = iamoConfigurationModel.HieuSuatTurbine;
                        IAMOSetting.CotAp = iamoConfigurationModel.CotAp;
                        IAMOSetting.Portname = iamoConfigurationModel.Portname;
                        IAMOSetting.Baudrate = iamoConfigurationModel.Baudrate;
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
    }
}
