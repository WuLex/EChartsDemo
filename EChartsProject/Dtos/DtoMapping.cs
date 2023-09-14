using EChartsProject.Models;
using Mapster;

namespace EChartsProject.Dtos
{
    public class DtoMapping
    {
        // 在启动代码或适当位置配置Mapster
        public static void ConfigureMappings()
        {
            TypeAdapterConfig<Product, ProductDto>.NewConfig()
                .Map(dest => dest.TagNames, src => src.DataTags.Select(dt => dt.Tag.Name).ToList());

            // 在这里可以添加其他映射配置
        }
    }
}
