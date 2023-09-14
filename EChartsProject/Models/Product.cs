namespace EChartsProject.Models
{
    // 更新Product模型
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        // 其他属性
        public virtual List<DataTag> DataTags { get; set; }
    }
}
