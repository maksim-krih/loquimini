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
        private ITrackableRepository<House, Guid> _houseRepository;
        private ITrackableRepository<Flat, Guid> _flatRepository;
        private IGenericRepository<BuildingInfo> _buildingInfoRepository;
        private IGenericRepository<Receipt> _receiptRepository;

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

        public ITrackableRepository<House, Guid> HouseRepository
        {
            get
            {
                _houseRepository = _houseRepository ?? _serviceProvider.GetService<ITrackableRepository<House, Guid>>();
                return _houseRepository;
            }
        }

        public ITrackableRepository<Flat, Guid> FlatRepository
        {
            get
            {
                _flatRepository = _flatRepository ?? _serviceProvider.GetService<ITrackableRepository<Flat, Guid>>();
                return _flatRepository;
            }
        }

        public IGenericRepository<BuildingInfo> BuildingInfoRepository
        {
            get
            {
                _buildingInfoRepository = _buildingInfoRepository ?? _serviceProvider.GetService<IGenericRepository<BuildingInfo>>();
                return _buildingInfoRepository;
            }
        }

        public IGenericRepository<Receipt> ReceiptRepository
        {
            get
            {
                _receiptRepository = _receiptRepository ?? _serviceProvider.GetService<IGenericRepository<Receipt>>();
                return _receiptRepository;
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
            _houseRepository?.Dispose();
            _flatRepository?.Dispose();
            _buildingInfoRepository?.Dispose();
            _receiptRepository?.Dispose();


            _context.Dispose();
        }
    }
}
