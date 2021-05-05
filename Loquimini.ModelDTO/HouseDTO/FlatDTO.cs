using AutoMapper;
using Loquimini.Common.Enums;
using Loquimini.Mapping;
using Loquimini.Model.Entities;
using System;
using System.Collections.Generic;

namespace Loquimini.ModelDTO.HouseDTO
{
    public class FlatDTO: IProfileBase
    {
        public Guid Id { get; set; }

        public string Number { get; set; }

        public Guid? UserId { get; set; }

        public BuildingInfoDTO Info { get; set; }

        public IProfileExpression Configure(IProfileExpression config)
        {
            config.CreateMap<Flat, FlatDTO>()
                .ReverseMap()
                .ForMember(x => x.Id, cfg => cfg.Ignore());

            return config;
        }
    }
}
