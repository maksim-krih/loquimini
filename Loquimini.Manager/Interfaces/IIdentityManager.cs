using Loquimini.Model.User;

namespace Loquimini.Manager.Interfaces
{
    public interface IIdentityManager
    {
        UserIdentity GetCurrentUser(string token = null);
    }
}
