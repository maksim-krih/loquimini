using Loquimini.Model.Core;
using System;

namespace Loquimini.Model.Entities
{
    public class Flat : TrackableEntity<Guid>
    {
        public string Number { get; set; }

        public Guid UserId { get; set; }

        public Guid InfoId { get; set; }

        public Guid HouseId { get; set; }

        public virtual User User { get; set; }

        public virtual BuildingInfo Info { get; set; }

        public virtual House House { get; set; }
    }
}