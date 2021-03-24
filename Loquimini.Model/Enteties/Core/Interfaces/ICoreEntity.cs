using System;

namespace Loquimini.Model.Core.Interfaces
{
    public interface ICoreEntity<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}
