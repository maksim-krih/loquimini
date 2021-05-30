using AutoMapper;
using Loquimini.Common.Enums;
using Loquimini.Mapping;
using Loquimini.Model.Entities;
using System;
using Loquimini.ModelDTO.HouseDTO;

namespace Loquimini.ModelDTO.ReceiptDTO
{
    public class ReceiptGridDTO : IProfileBase
    {
        public Guid Id { get; set; }

        public ReceiptType Type { get; set; }

        public ReceiptStatus Status { get; set; }

        public double Rate { get; set; }

        public int? OldIndicator { get; set; }

        public int? NewIndicator { get; set; }

        public decimal Total { get; set; }

        public decimal Paid { get; set; }

        public decimal Debt { get; set; }

        public DateTime Date { get; set; }

        public HouseType HouseType { get; set; }

        public HouseDTO.HouseDTO House { get; set; }

        public FlatDTO Flat { get; set; }

        public IProfileExpression Configure(IProfileExpression config)
        {
            config.CreateMap<Receipt, ReceiptGridDTO>()
                .ForMember(x => x.Date, cfg => cfg.MapFrom(m => m.CreatedDate))
                .ForMember(x => x.HouseType, cfg => cfg.MapFrom(m => m.House != null ? HouseType.Private : HouseType.Apartment))
                .ReverseMap()
                .ForMember(x => x.Id, cfg => cfg.Ignore());

            return config;
        }
    }
}
