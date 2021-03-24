using System;
using Microsoft.AspNetCore.Identity;

namespace Loquimini.Model.Enteties
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public virtual Role Role { get; set; }
    }
}
