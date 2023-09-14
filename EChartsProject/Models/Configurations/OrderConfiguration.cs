using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace EChartsProject.Models.Configurations
{

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.OrderID);

            builder.Property(o => o.CustomerID)
                .IsRequired()
                .HasMaxLength(255);
           
            // 配置与DataTags的关系
            builder.HasMany(o => o.DataTags)
                .WithOne()
                .HasForeignKey(dt => dt.LinkedID)
                .IsRequired(false);

            // 其他配置...
        }
    }

}
