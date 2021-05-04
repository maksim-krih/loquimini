using AutoMapper;
using Loquimini.Common.Enums;
using Loquimini.Mapping;
using Loquimini.Model.Entities;
using System;
using System.Collections.Generic;

namespace Loquimini.ModelDTO.HouseDTO
{
    public class BuildingInfoDTO: IProfileBase
    {
        public int Area { get; set; }

        public IProfileExpression Configure(IProfileExpression config)
        {
            config.CreateMap<BuildingInfo, BuildingInfoDTO>()
                .ReverseMap();

            return config;
        }
    }
}
