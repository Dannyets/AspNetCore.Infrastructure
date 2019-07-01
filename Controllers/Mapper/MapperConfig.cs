using AutoMapper;
using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Infrastructure.Controllers.Mapper
{
    public static class MapperConfig
    {
        public static void InitializeMapper(Dictionary<Type, Type> viewModelTypeToDbModelTypeMap)
        {
            var cfg = new MapperConfigurationExpression();

            foreach (var viewModelTypeToDbModelType in viewModelTypeToDbModelTypeMap)
            {
                var viewModelType = viewModelTypeToDbModelType.Key;
                var dbModelType = viewModelTypeToDbModelType.Value;

                cfg.CreateMap(viewModelType.GetType(), dbModelType.GetType());
            }

            AutoMapper.Mapper.Initialize(cfg);
        }
    }
}
