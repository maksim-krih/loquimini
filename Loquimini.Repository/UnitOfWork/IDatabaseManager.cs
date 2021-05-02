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

        bool SaveChanges();

        Task<bool> SaveChangesAsync();
    }
}
