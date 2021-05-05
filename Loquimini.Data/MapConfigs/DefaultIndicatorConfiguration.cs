using Loquimini.Common.EntityFrameworkTypeConfigurator;
using Loquimini.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loquimini.Data.MapConfigs
{
    public class DefaultIndicatorConfiguration : EntityTypeConfiguration<DefaultIndicator>
    {
        public override void Map(EntityTypeBuilder<DefaultIndicator> entityBuilder)
        {
            entityBuilder
                .HasOne(x => x.Info)
                .WithMany(ur => ur.DefaultIndicators)
                .HasForeignKey(ur => ur.InfoId);
        }
    }
}
