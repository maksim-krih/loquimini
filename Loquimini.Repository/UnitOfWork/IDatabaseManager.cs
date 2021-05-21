using Loquimini.Interfaces.Repository;
using Loquimini.Model.Entities;
using System;
using System.Threading.Tasks;

namespace Loquimini.Repository.UnitOfWork
{
    public interface IDatabaseManager : IDisposable
    {
        ITrackableRepository<User, Guid> UserRepository { get; }
        IGenericRepository<Role> RoleRepository { get; }
        ITrackableRepository<House, Guid> HouseRepository { get; }
        ITrackableRepository<Flat, Guid> FlatRepository { get; }
        IGenericRepository<BuildingInfo> BuildingInfoRepository { get; }
        IGenericRepository<Receipt> ReceiptRepository { get; }

        bool SaveChanges();

        Task<bool> SaveChangesAsync();
    }
}
