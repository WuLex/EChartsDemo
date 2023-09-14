using EChartsProject.Models.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EChartsProject.Models
{
    public class BcSwapDbContext:DbContext
    {
        public BcSwapDbContext(DbContextOptions<BcSwapDbContext> options)
        : base(options)
        {
            //延迟加载未启用：默认情况下，Entity Framework Core不会自动加载导航属性的数据（例如DataTags），
            //除非你显式启用了延迟加载。你可以通过以下方法之一启用延迟加载：
            //使用virtual关键字标记导航属性，以启用延迟加载。
            //例如：public virtual List<DataTag> DataTags { get; set; }
            //ChangeTracker.LazyLoadingEnabled = true;
        }

        // DbSet用于表示数据库中的表
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<DataTag> DataTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 定义表之间的关系映射，包括外键关系等
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new DataTagConfiguration());
            // 可以添加其他配置和映射

            base.OnModelCreating(modelBuilder);
        }
    }
}
