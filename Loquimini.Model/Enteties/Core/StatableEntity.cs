using Loquimini.Model.Core.Interfaces;
using System;

namespace Loquimini.Model.Core
{
    public class StatableEntity<TKey>: CoreEntity<TKey>, IStableEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public bool IsDeleted { get; set; }
    }
}
