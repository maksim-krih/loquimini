using Loquimini.Common.EntityFrameworkTypeConfigurator;
using Loquimini.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loquimini.Data.MapConfigs
{
    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public override void Map(EntityTypeBuilder<Role> entityBuilder)
        {
            entityBuilder.HasMany<UserRole>()
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        }
    }
}
