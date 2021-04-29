using Loquimini.Manager.Interfaces;
using Loquimini.Model.User;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Loquimini.Manager
{
    public class IdentityManager : IIdentityManager
    {
        private readonly ClaimsPrincipal _principal;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        public IdentityManager(ClaimsPrincipal principal)
        {
            _principal = principal;
        }

        public UserIdentity GetCurrentUser(string token = null)
        {
            if (token != null)
            {
                return new UserIdentity(_jwtSecurityTokenHandler.ReadJwtToken(token).Claims);
            }

            return _principal != null && _principal.Claims.Any() ? new UserIdentity(_principal.Claims) : null;
        }
    }
}
