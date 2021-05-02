using Loquimini.Common;
using Loquimini.Model.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Loquimini.Model.Entities
{
    public class User : IdentityUser<Guid>, ITrackableEntity<Guid>
    {
        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; }

        public IList<Claim> GetUserClaims()
        {
            return new[] {
                new Claim(UserClaims.FirstName, FirstName),
                new Claim(UserClaims.LastName, LastName),
                new Claim(JwtRegisteredClaimNames.Email, Email),
                new Claim(JwtRegisteredClaimNames.Jti, Id.ToString())
            };
        }
    }
}
