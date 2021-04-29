using Loquimini.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Loquimini.Model.User
{
    public class UserIdentity
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public UserIdentity(IEnumerable<Claim> claims)
        {
            FirstName = claims.FirstOrDefault(c => c.Type == UserClaims.FirstName)?.Value ?? string.Empty;
            Id = Guid.Parse(claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value ?? Guid.Empty.ToString());
        }
    }
}
