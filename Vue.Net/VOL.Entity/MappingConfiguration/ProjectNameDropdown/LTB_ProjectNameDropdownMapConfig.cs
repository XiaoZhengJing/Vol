using VOL.Entity.MappingConfiguration;
using VOL.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VOL.Entity.MappingConfiguration
{
    public class LTB_ProjectNameDropdownMapConfig : EntityMappingConfiguration<LTB_ProjectNameDropdown>
    {
        public override void Map(EntityTypeBuilder<LTB_ProjectNameDropdown>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

