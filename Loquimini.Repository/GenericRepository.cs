using Loquimini.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Loquimini.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
         where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _entities;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        #region Base
        public virtual TEntity Create(TEntity entity)
        {
            return _entities.Add(entity).Entity;
        }
        public virtual TEntity Update(TEntity entity)
        {
            return _entities.Update(entity).Entity;
        }
        public virtual TEntity Delete(TEntity entity)
        {
            return _entities.Remove(entity).Entity;
        }
        public TEntity GetById<TKey>(TKey id)
        {
            return _entities.Find(id);
        }

        public virtual void CreateRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }
        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            _entities.UpdateRange(entities);
        }
        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => _entities.UpdateRange(entities));
        }
        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => _entities.RemoveRange(entities));
        }
        public virtual async Task CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            await _entities.AddRangeAsync(entities);
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity, DateTime? now = null)
        {
            var result = await _entities.AddAsync(entity);
            return result.Entity;
        }
        public async Task<TEntity> GetByIdAsync<TKey>(TKey id)
        {
            return await _entities.FindAsync(id);
        }
        
        #endregion Base

        #region With expression
        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression = null)
        {
            return _entities.Where(expression ?? (x => true));
        }
        public virtual bool Any(Expression<Func<TEntity, bool>> expression = null)
        {
            return _entities.Any(expression ?? (x => true));
        }
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression = null)
        {
            return _entities.FirstOrDefault(expression ?? (x => true));
        }
        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return await _entities.FirstOrDefaultAsync(expression ?? (x => true));
        }
        #endregion With expression

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
