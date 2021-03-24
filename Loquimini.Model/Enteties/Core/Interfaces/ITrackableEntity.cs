using System;

namespace Loquimini.Model.Core.Interfaces
{
    public interface ITrackableEntity<TKey> : ICoreEntity<TKey> where TKey : IEquatable<TKey>
    {
        Guid ModifiedBy { get; set; }

        DateTime ModifiedDate { get; set; }

        Guid CreatedBy { get; set; }

        DateTime CreatedDate { get; set; }

        bool IsDeleted { get; set; }
    }
}
