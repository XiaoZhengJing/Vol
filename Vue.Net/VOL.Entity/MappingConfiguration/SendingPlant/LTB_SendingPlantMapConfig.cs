using VOL.Entity.MappingConfiguration;
using VOL.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VOL.Entity.MappingConfiguration
{
    public class LTB_SendingPlantMapConfig : EntityMappingConfiguration<LTB_SendingPlant>
    {
        public override void Map(EntityTypeBuilder<LTB_SendingPlant>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

