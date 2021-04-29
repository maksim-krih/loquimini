using System;

namespace Loquimini.Model.Core.Interfaces
{
    public interface IStableEntity<TKey> : ICoreEntity<TKey> where TKey : IEquatable<TKey>
    {
        bool IsDeleted { get; set; }
    }
}
