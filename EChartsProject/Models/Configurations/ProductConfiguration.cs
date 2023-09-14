using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EChartsProject.Models.Configurations
{

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductID);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255);


            // 配置与DataTags的关系
            builder.HasMany(p => p.DataTags)
                .WithOne() // 指定DataTag中的Product导航属性
                .HasForeignKey(dt => dt.LinkedID) // 指定外键属性
                .IsRequired(false); // 如果允许ProductID为空，请设置为true

            // 其他配置...
        }
    }

}
