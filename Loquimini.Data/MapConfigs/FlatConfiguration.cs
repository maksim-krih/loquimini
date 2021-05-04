using Loquimini.Common.EntityFrameworkTypeConfigurator;
using Loquimini.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loquimini.Data.MapConfigs
{
    public class FlatConfiguration : EntityTypeConfiguration<Flat>
    {
        public override void Map(EntityTypeBuilder<Flat> entityBuilder)
        {
            entityBuilder
                .HasOne(x => x.User)
                .WithMany(ur => ur.Flats)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            entityBuilder
                .HasOne(x => x.Info)
                .WithOne(ur => ur.Flat)
                .HasForeignKey<BuildingInfo>(ur => ur.FlatId);

            entityBuilder
                .HasOne(x => x.House)
                .WithMany(ur => ur.Flats)
                .HasForeignKey(ur => ur.HouseId)
                .IsRequired();
        }
    }
}
