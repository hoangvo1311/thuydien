using AutoMapper;
using IndusG.DataAccess;
using IndusG.Models;
using IndusG.Models.Setting;

namespace IndusG.Service
{
    public static class MapperHelper
    {
        public static readonly IMapper Mapper;

        static MapperHelper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DB_DakHnol, MeasurementModel>();
                cfg.CreateMap<DB_DakHnolMeasurement, MeasurementModel>();
                cfg.CreateMap<DB_SesanMeasurement, MeasurementModel>();
                cfg.CreateMap<DB_DakHnol, DataModel>();
                cfg.CreateMap<DB_DakHnolPLCConfiguration, PLCSettingModel>();
                cfg.CreateMap<DB_SesanPLCConfiguration, PLCSettingModel>();
                cfg.CreateMap<SettingModel, DB_DakHnolParameter>();
                cfg.CreateMap<SettingModel, DB_SesanParameter>();
                cfg.CreateMap<IAMOConfigurationModel, DB_IAMO_Configuration>();
                cfg.CreateMap<DB_IAMO_Configuration, IAMOConfigurationModel>();
                cfg.CreateMap<DB_IAMO_Measurement, IAMO_MeasurementModel>();
            });

            Mapper = config.CreateMapper();
        }
    }
}
