using Loquimini.Common.Enums;
using Loquimini.Model.Core;
using System;
using System.Collections.Generic;

namespace Loquimini.Model.Entities
{
    public class BuildingInfo : CoreEntity<Guid>
    {
        public int Area { get; set; }

        public Guid? FlatId { get; set; }

        public Guid? HouseId { get; set; }

        public virtual Flat Flat { get; set; }

        public virtual House House { get; set; }
    }
}