using Loquimini.Common.Enums;
using Loquimini.Model.Core;
using System;
using System.Collections.Generic;

namespace Loquimini.Model.Entities
{
    public class House : TrackableEntity<Guid>
    {
        public string Street { get; set; }

        public string Number { get; set; }

        public HouseType Type { get; set; }

        public Guid? UserId { get; set; }

        public Guid InfoId { get; set; }

        public double RentRate { get; set; }

        public virtual User User { get; set; }

        public virtual BuildingInfo Info { get; set; }

        public virtual ICollection<Flat> Flats { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}