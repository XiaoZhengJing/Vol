using VOL.Entity.MappingConfiguration;
using VOL.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VOL.Entity.MappingConfiguration
{
    public class LTB_SSCommonMapConfig : EntityMappingConfiguration<LTB_SSCommon>
    {
        public override void Map(EntityTypeBuilder<LTB_SSCommon>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

