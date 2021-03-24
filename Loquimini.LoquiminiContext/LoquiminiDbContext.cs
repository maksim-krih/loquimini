using Loquimini.Common.EntityFrameworkTypeConfigurator;
using Loquimini.LoquiminiContext.MapConfigs;
using Loquimini.Model.Enteties;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Loquimini.LoquiminiContext
{
    public class LoquiminiDbContext : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>,
        UserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public LoquiminiDbContext(DbContextOptions options) : base(options)
        { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.AddConfiguration(new UserConfiguration());
        }
    }
}
