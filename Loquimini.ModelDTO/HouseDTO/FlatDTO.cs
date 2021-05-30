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

        public string HouseNumber { get; set; }
        
        public string Street { get; set; }

        public Guid HouseId { get; set; }

        public Guid? UserId { get; set; }

        public BuildingInfoDTO Info { get; set; }

        public IProfileExpression Configure(IProfileExpression config)
        {
            config.CreateMap<Flat, FlatDTO>()
                .ForMember(x => x.Street, cfg => cfg.MapFrom(m => m.House.Street))
                .ForMember(x => x.HouseNumber, cfg => cfg.MapFrom(m => m.House.Number))
                .ReverseMap()
                .ForMember(x => x.Id, cfg => cfg.Ignore());

            return config;
        }
    }
}
