using AutoMapper;
using Loquimini.Common.Enums;
using Loquimini.Mapping;
using Loquimini.Model.Entities;
using System;
using System.Collections.Generic;

namespace Loquimini.ModelDTO.ReceiptDTO
{
    public class ReceiptGridDTO : IProfileBase
    {
        public Guid Id { get; set; }

        public ReceiptType Type { get; set; }

        public ReceiptStatus Status { get; set; }

        public double Rate { get; set; }

        public int? OldIndicator { get; set; }

        public int? NewIndictor { get; set; }

        public decimal Total { get; set; }

        public decimal Paid { get; set; }

        public decimal Debt { get; set; }

        public virtual House House { get; set; }

        public virtual Flat Flat { get; set; }

        public IProfileExpression Configure(IProfileExpression config)
        {
            config.CreateMap<Receipt, ReceiptGridDTO>()
                .ReverseMap()
                .ForMember(x => x.Id, cfg => cfg.Ignore());

            return config;
        }
    }
}
