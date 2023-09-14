namespace EChartsProject.Models
{
    // 更新模型
    public class DataTag
    {
        public int DataTagID { get; set; }
        public int DataTagType { get; set; } // 1表示订单，2表示商品
        public int LinkedID { get; set; } // 记录订单或商品ID，根据DataTagType的值来区分

        public int TagID { get; set; }
        // 其他属性
        public Tag Tag { get; set; }
    }
}
