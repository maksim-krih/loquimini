using Loquimini.Common.EntityFrameworkTypeConfigurator;
using Loquimini.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loquimini.Data.MapConfigs
{
    public class ReceiptConfiguration : EntityTypeConfiguration<Receipt>
    {
        public override void Map(EntityTypeBuilder<Receipt> entityBuilder)
        {
            entityBuilder
                .HasOne(x => x.House)
                .WithMany(ur => ur.Receipts)
                .HasForeignKey(ur => ur.HouseId);

            entityBuilder
                .HasOne(x => x.Flat)
                .WithMany(ur => ur.Receipts)
                .HasForeignKey(ur => ur.FlatId);
        }
    }
}
