using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EChartsProject.Models.Configurations
{
    public class DataTagConfiguration : IEntityTypeConfiguration<DataTag>
    {
        public void Configure(EntityTypeBuilder<DataTag> builder)
        {
            builder.HasKey(dt => dt.DataTagID);

            builder.Property(dt => dt.DataTagType)
                .IsRequired();

            // 配置与Tag的关联
            builder.HasOne(dt => dt.Tag)
                .WithMany()
                .HasForeignKey(dt => dt.TagID)
                .IsRequired();
        }
    }
}
