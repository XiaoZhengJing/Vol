using VOL.Entity.MappingConfiguration;
using VOL.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VOL.Entity.MappingConfiguration
{
    public class LTB_CounterTypeMapConfig : EntityMappingConfiguration<LTB_CounterType>
    {
        public override void Map(EntityTypeBuilder<LTB_CounterType>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

