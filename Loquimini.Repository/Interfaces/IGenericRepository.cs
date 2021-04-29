using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Loquimini.Interfaces.Repository
{
    public interface IGenericRepository<TEntity> : IDisposable
        where TEntity : class
    {
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(TEntity entity);
        TEntity GetById<TKey>(TKey id);

        void CreateRange(IEnumerable<TEntity> entities);
        void UpdateRange(IEnumerable<TEntity> entities);
        void DeleteRange(IEnumerable<TEntity> entities);

        Task<TEntity> CreateAsync(TEntity entity, DateTime? now = null);
        Task<TEntity> GetByIdAsync<TKey>(TKey id);

        Task CreateRangeAsync(IEnumerable<TEntity> entities);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression = null);
        bool Any(Expression<Func<TEntity, bool>> expression = null);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression = null);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression = null);

        void SaveChanges();

        Task SaveChangesAsync();
    }
}