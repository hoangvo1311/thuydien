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
                cfg.CreateMap<DB_Sesan_2PLC, MeasurementModel>();
                cfg.CreateMap<DB_SesanMeasurement, MeasurementModel>();
                cfg.CreateMap<DB_Sesan_2PLC, DataModel>();
                cfg.CreateMap<DB_SesanPLCConfiguration, PLCSettingModel>();
                cfg.CreateMap<SettingModel, DB_SesanParameter>();
            });

            Mapper = config.CreateMapper();
        }
    }
}
