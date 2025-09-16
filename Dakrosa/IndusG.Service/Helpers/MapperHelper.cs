using AutoMapper;
using IndusG.DataAccess;
using IndusG.Models;
using IndusG.Models.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndusG.Service
{
    public static class MapperHelper
    {
        public static readonly IMapper Mapper;

        static MapperHelper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DB_Data, MeasurementModel>();
                cfg.CreateMap<DB_Measurement, MeasurementModel>();
                cfg.CreateMap<DB_Data, DataModel>();
                cfg.CreateMap<DB_PLCConfiguration, PLCSettingModel>();
                cfg.CreateMap<SettingModel, DB_Parameter>();
                cfg.CreateMap<SettingModel, DB_Parameter>();
                cfg.CreateMap<DB_Parameter, DB_Parameter>();
            });

            Mapper = config.CreateMapper();
        }
    }
}
