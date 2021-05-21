using Loquimini.Common.EntityFrameworkTypeConfigurator;
using Loquimini.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loquimini.Data.MapConfigs
{
    public class HouseConfiguration : EntityTypeConfiguration<House>
    {
        public override void Map(EntityTypeBuilder<House> entityBuilder)
        {
            entityBuilder
                .HasOne(x => x.User)
                .WithMany(ur => ur.Houses)
                .HasForeignKey(ur => ur.UserId);

            entityBuilder
                .HasOne(x => x.Info)
                .WithOne(ur => ur.House)
                .HasForeignKey<BuildingInfo>(ur => ur.HouseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
