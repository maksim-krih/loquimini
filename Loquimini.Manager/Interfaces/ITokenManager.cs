using Loquimini.Model.Entities;

namespace Loquimini.Manager.Interfaces
{
    public interface ITokenManager
    {
        string CreateToken(User user);

        string GenerateToken(int size = 32);
    }
}
