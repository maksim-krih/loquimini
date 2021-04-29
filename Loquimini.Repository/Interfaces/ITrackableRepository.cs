using Loquimini.Model.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Loquimini.Interfaces.Repository
{
    public interface ITrackableRepository<TEntity, TKey> 
        : IGenericRepository<TEntity>
        where TEntity : class, ITrackableEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        TEntity Deactivate(TEntity entity);
        TEntity Activate(TEntity entity);

        void ActivateRange(IEnumerable<TEntity> entities);
        void DeactivateRange(IEnumerable<TEntity> entities);
    }
}
