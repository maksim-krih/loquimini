using Loquimini.Model.Core.Interfaces;
using System;

namespace Loquimini.Model.Core
{
    public class TrackableEntity<TKey> : CoreEntity<TKey>, ITrackableEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public Guid CreatedBy{ get; set; }

        public Guid ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
