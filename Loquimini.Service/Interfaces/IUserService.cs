using System;
using System.Threading.Tasks;

namespace Loquimini.Service.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<object> Login(object loginUser);

	}
}
