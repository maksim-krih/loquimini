using Loquimini.Common.Enums;
using Loquimini.Model.Core;
using System;

namespace Loquimini.Model.Entities
{
    public class DefaultIndicator : CoreEntity<Guid>
    {
        public ReceiptType Type { get; set; }

        public int Value { get; set; } 

        public Guid InfoId { get; set; }

        public virtual BuildingInfo Info { get; set; }
    }
}