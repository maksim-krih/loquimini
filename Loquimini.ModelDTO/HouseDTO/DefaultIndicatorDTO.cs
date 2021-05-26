using AutoMapper;
using Loquimini.Common.Enums;
using Loquimini.Mapping;
using Loquimini.Model.Entities;
using System;
using System.Collections.Generic;

namespace Loquimini.ModelDTO.HouseDTO
{
    public class DefaultIndicatorDTO: IProfileBase
    {
        public ReceiptType Type { get; set; }

        public int Value { get; set; }

        public IProfileExpression Configure(IProfileExpression config)
        {
            config.CreateMap<DefaultIndicator, DefaultIndicatorDTO>()
                .ReverseMap();

            return config;
        }
    }
}
