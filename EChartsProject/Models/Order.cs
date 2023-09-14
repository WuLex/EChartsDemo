namespace EChartsProject.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        public string OrderNum { get; set; }

        public string CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        // 其他属性
        public List<DataTag> DataTags { get; set; }
    }
}
