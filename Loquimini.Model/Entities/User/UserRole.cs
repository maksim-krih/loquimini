using System;
using Microsoft.AspNetCore.Identity;

namespace Loquimini.Model.Entities
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public virtual Role Role { get; set; }
    }
}
