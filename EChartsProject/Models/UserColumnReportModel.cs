namespace EChartsProject.Models
{
    //eCharts做图使用的Model(柱型图)
    public class UserColumnReportModel
    {
        public string[] color { get; set; }

        public TooltipCAttribute tooltip { get; set; }

        public GridColumn grid { get; set; }

        public List<XAxisColumn> xAxis { get; set; }

        public List<YAxisColumn> yAxis { get; set; }

        public List<SeriesColumn> series { get; set; }

    }

    public class TooltipCAttribute
    {
        public string trigger { get; set; }

        public AxisColumnPointer axisPointer { get; set; }
    }

    public class AxisColumnPointer
    {
        public string type { get; set; }
    }

    public class GridColumn
    {
        public string left { get; set; }

        public string right { get; set; }

        public string bottom { get; set; }

        public bool containLabel { get; set; }
    }

    public class XAxisColumn
    {
        public string type { get; set; }
        //['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
        public List<string> data { get; set; }

        public AxisTickColumn axisTick { get; set; }
    }

    public class AxisTickColumn
    {
        public bool alignWithLabel { get; set; }
    }

    public class YAxisColumn
    {
        public string type { get; set; }
    }

    public class SeriesColumn
    {
        public string name { get; set; }

        public string type { get; set; }

        public string barWidth { get; set; }

        public List<string> data { get; set; }
    }
}
