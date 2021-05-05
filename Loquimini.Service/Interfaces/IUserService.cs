using Loquimini.Model.Entities;
using Loquimini.ModelDTO.UserDTO;
using System;
using System.Threading.Tasks;

namespace Loquimini.Service.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<User> UpdateUserAsync(UserDTO userDTO);
    }
}
