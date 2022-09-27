namespace EChartsProject.Models
{
    //eCharts做图使用的Model(饼图)
    public class UserReportModel
    {
        public TitleAttribute title { get; set; }

        public TooltipAttribute tooltip { get; set; }

        public LegendAttribute legend { get; set; }

        public List<SeriesAttribute> series { get; set; }
    }

    public class TitleAttribute
    {
        public string text { get; set; }

        //'center'
        public string x { get; set; }
    }

    public class TooltipAttribute
    {
        //"{b} : {c} ({d}%)"
        public string formatter { get; set; }
    }

    public class LegendAttribute
    {
        //['男', '女']
        public List<string> data { get; set; }
        //'left'
        public string left { get; set; }

        public string orient { get; set; }
    }

    public class SeriesAttribute
    {
        //"pie"
        public string type { get; set; }

        public List<SeriesDataAttribute> data { get; set; }

    }
    public class SeriesDataAttribute
    {
        public int value { get; set; }
        public string name { get; set; }
    }
}
