using VOL.Entity.MappingConfiguration;
using VOL.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VOL.Entity.MappingConfiguration
{
    public class LTB_SeriesDemandsMapConfig : EntityMappingConfiguration<LTB_SeriesDemands>
    {
        public override void Map(EntityTypeBuilder<LTB_SeriesDemands>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

