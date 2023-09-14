using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace EChartsProject.Models.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(t => t.TagID);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(255);

            // 其他配置...
        }
    }
}
