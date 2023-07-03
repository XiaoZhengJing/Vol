using VOL.Entity.MappingConfiguration;
using VOL.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VOL.Entity.MappingConfiguration
{
    public class LTB_ServiceDemandsMapConfig : EntityMappingConfiguration<LTB_ServiceDemands>
    {
        public override void Map(EntityTypeBuilder<LTB_ServiceDemands>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

