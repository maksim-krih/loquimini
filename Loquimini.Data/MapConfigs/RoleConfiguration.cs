using Loquimini.Common.EntityFrameworkTypeConfigurator;
using Loquimini.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loquimini.Data.MapConfigs
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public override void Map(EntityTypeBuilder<User> entityBuilder)
        {
            entityBuilder
                .HasMany(e => e.UserRoles)
                .WithOne()
                .HasForeignKey(e => e.UserId);
        }
    }
}
