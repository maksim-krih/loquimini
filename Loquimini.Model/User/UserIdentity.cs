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

        public string LastName { get; set; }

        public string Email { get; set; }

        public UserIdentity(IEnumerable<Claim> claims)
        {
            Email = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value ?? string.Empty;
            LastName = claims.FirstOrDefault(c => c.Type == UserClaims.LastName)?.Value ?? string.Empty;
            FirstName = claims.FirstOrDefault(c => c.Type == UserClaims.FirstName)?.Value ?? string.Empty;
            Id = Guid.Parse(claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value ?? Guid.Empty.ToString());
        }
    }
}
