using VOL.Entity.MappingConfiguration;
using VOL.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VOL.Entity.MappingConfiguration
{
    public class LTB_PlantMapConfig : EntityMappingConfiguration<LTB_Plant>
    {
        public override void Map(EntityTypeBuilder<LTB_Plant>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

