using Loquimini.Interfaces.Repository;
using Loquimini.Manager.Interfaces;
using Loquimini.Model.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loquimini.Repository
{
    public class TrackableRepository<TrackableEntity, TKey> 
        : GenericRepository<TrackableEntity>, ITrackableRepository<TrackableEntity, TKey>
        where TrackableEntity : class, ITrackableEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        private readonly IIdentityManager _identityManager;

        public TrackableRepository(DbContext context, IIdentityManager identityManager) : base(context)
        {
            _identityManager = identityManager;
        }

        public override TrackableEntity Create(TrackableEntity entity)
        {
            var currentUser = _identityManager.GetCurrentUser();

            if (currentUser != null && currentUser.Id != Guid.Empty)
            {
                entity.CreatedBy = currentUser.Id;
            }

            entity.CreatedDate = entity.ModifiedDate = DateTime.UtcNow;

            return base.Create(entity);
        }
        public override TrackableEntity Update(TrackableEntity entity)
        {
            var currentUser = _identityManager.GetCurrentUser();

            if (currentUser != null && currentUser.Id != Guid.Empty)
            {
                entity.ModifiedBy = currentUser.Id;
            }

            entity.ModifiedDate = DateTime.UtcNow;

            return base.Update(entity);
        }
        public virtual TrackableEntity Activate(TrackableEntity entity)
        {
            entity.IsDeleted = false;
            return base.Update(entity);
        }
        public virtual TrackableEntity Deactivate(TrackableEntity entity)
        {
            entity.IsDeleted = true;
            return base.Update(entity);
        }

        public override void CreateRange(IEnumerable<TrackableEntity> entities)
        {
            var currentUser = _identityManager.GetCurrentUser();

            foreach (var entity in entities)
            {
                if (currentUser != null && currentUser.Id != Guid.Empty)
                {
                    entity.CreatedBy = currentUser.Id;
                }

                entity.CreatedDate = entity.ModifiedDate = DateTime.UtcNow;
            }

            base.CreateRange(entities);
        }
        public override void UpdateRange(IEnumerable<TrackableEntity> entities)
        {
            var currentUser = _identityManager.GetCurrentUser();

            foreach (var entity in entities)
            {
                if (currentUser != null && currentUser.Id != Guid.Empty)
                {
                    entity.ModifiedBy = currentUser.Id;
                }

                entity.ModifiedDate = DateTime.UtcNow;
            }

            base.UpdateRange(entities);
        }
        public virtual void ActivateRange(IEnumerable<TrackableEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = false;
            }

            base.UpdateRange(entities);
        }
        public virtual void DeactivateRange(IEnumerable<TrackableEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
            }

            base.UpdateRange(entities);
        }

        public override Task<TrackableEntity> CreateAsync(TrackableEntity entity, DateTime? now = null)
        {
            var currentUser = _identityManager.GetCurrentUser();

            if (currentUser != null && currentUser.Id != Guid.Empty)
            {
                entity.CreatedBy = currentUser.Id;
            }

            entity.CreatedDate = entity.ModifiedDate = now ?? DateTime.UtcNow;

            return base.CreateAsync(entity);
        }
        public override async Task CreateRangeAsync(IEnumerable<TrackableEntity> entities)
        {
            var currentUser = _identityManager.GetCurrentUser();

            foreach (var entity in entities)
            {
                if (currentUser != null && currentUser.Id != Guid.Empty)
                {
                    entity.CreatedBy = currentUser.Id;
                }

                entity.CreatedDate = entity.ModifiedDate = DateTime.UtcNow;
            }

            await base.CreateRangeAsync(entities);
        }

        public override async Task UpdateRangeAsync(IEnumerable<TrackableEntity> entities)
        {
            var currentUser = _identityManager.GetCurrentUser();
            if (entities.Any())
            {
                foreach (var entity in entities)
                {
                    if (currentUser != null && currentUser.Id != Guid.Empty)
                    {
                        entity.ModifiedBy = currentUser.Id;
                    }

                    entity.ModifiedDate = DateTime.UtcNow;
                }

                await base.UpdateRangeAsync(entities);
            }
        }
    }
}
