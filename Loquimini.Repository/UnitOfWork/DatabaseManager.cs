using Loquimini.Interfaces.Repository;
using Loquimini.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Loquimini.Repository.UnitOfWork
{
    public class DatabaseManager : IDatabaseManager
    {
        readonly DbContext _context;

        readonly IServiceProvider _serviceProvider;

        private ITrackableRepository<User, Guid> _userRepository;
        private IGenericRepository<Role> _roleRepository;

        public DatabaseManager(DbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public ITrackableRepository<User, Guid> UserRepository
        {
            get
            {
                _userRepository = _userRepository ?? _serviceProvider.GetService<ITrackableRepository<User, Guid>>();
                return _userRepository;
            }
        }

        public IGenericRepository<Role> RoleRepository
        {
            get
            {
                _roleRepository = _roleRepository ?? _serviceProvider.GetService<IGenericRepository<Role>>();
                return _roleRepository;
            }
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> SaveChangesAsync()
        {
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public void Dispose()
        {
            _userRepository?.Dispose();
            _roleRepository?.Dispose();


            _context.Dispose();
        }
    }
}
