namespace EChartsProject.Models
{
    public class ChartDto
    {
        /// <summary>
        /// 使用 dimensions 定义 series.data 或者 dataset.source 的每个维度的信息。
        /// </summary>
        public List<string> Dimensions { get; set; }

        /// <summary>
        /// y轴
        /// </summary>
        public List<Dictionary<string, string>> Source { get; set; }
    }
}
