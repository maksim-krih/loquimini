using Loquimini.Common.Enums;
using Loquimini.Model.Core;
using System;

namespace Loquimini.Model.Entities
{
    public class Receipt : TrackableEntity<Guid>
    {
        public ReceiptType Type { get; set; }

        public ReceiptStatus Status { get; set; }

        public double Rate { get; set; } 
        
        public int? OldIndicator { get; set; } 

        public int? NewIndicator { get; set; }

        public decimal Total { get; set; }

        public decimal Paid { get; set; }

        public decimal Debt { get; set; }

        public Guid? HouseId { get; set; }

        public Guid? FlatId { get; set; }

        public virtual House House { get; set; }

        public virtual Flat Flat { get; set; }
    }
}