using Microsoft.AspNetCore.Mvc;

namespace EChartsProject.Controllers
{
    public class ChartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("sampledata")]
        public IActionResult SampleData()
        {
            var vo = GetSampleDataVO();
            return Ok(vo);
        }

        [HttpGet("stacked_line_chart_data")]
        public IActionResult stackedLineChartData()
        {
            var vo = GetStackedLineChartDataVO();
            return Ok(vo);
        }

        [HttpGet("product_data")]
        public IActionResult PieData()
        {
            List<string[]> pie = new List<string[]>();
            //string[,] pie = new string[5, 7];
            for (int i = 0; i < 5; i++)
            {
                if (i == 0)
                {
                    string[] cur = new string[] {
                        "产品",
                        DateTime.Now.AddYears(-5).Year.ToString(),
                        DateTime.Now.AddYears(-4).Year.ToString(),
                        DateTime.Now.AddYears(-3).Year.ToString(),
                        DateTime.Now.AddYears(-2).Year.ToString(),
                        DateTime.Now.AddYears(-1).Year.ToString(),
                        DateTime.Now.Year.ToString(),
                    };
                    pie.Add(cur);
                }
                else
                {
                    string[] cur = new string[] {
                        getProductName(i),
                        new Random().Next(1000,9999).ToString(),
                        new Random().Next(1000,9999).ToString(),
                        new Random().Next(1000,9999).ToString(),
                        new Random().Next(1000,9999).ToString(),
                        new Random().Next(1000,9999).ToString(),
                        new Random().Next(1000,9999).ToString(),
                    };
                    pie.Add(cur);
                }
            }
            return Ok(new { source = pie });
        }

        public ChartDto GetSampleDataVO()
        {
            ChartDto vo = new ChartDto();

            var dimensions = new List<string> { "产品", "2018", "2019", "2020" };

            Dictionary<string, string> dict1 = new Dictionary<string, string>
            {
                { "产品", "手机" },
                { "2018", "1000.01" },
                { "2019", "2000.02" },
                { "2020", "5000.50" }
            };

            Dictionary<string, string> dict2 = new Dictionary<string, string>
            {
                { "产品", "服饰" },
                { "2018", "3000.01" },
                { "2019", "5000.02" },
                { "2020", "8000.50" }
            };

            Dictionary<string, string> dict3 = new Dictionary<string, string>
            {
                { "产品", "日用" },
                { "2018", "11000.01" },
                { "2019", "12000.02" },
                { "2020", "15000.50" }
            };

            Dictionary<string, string> dict4 = new Dictionary<string, string>
            {
                { "产品", "电脑" },
                { "2018", "21000.01" },
                { "2019", "22000.02" },
                { "2020", "25000.50" }
            };

            List<Dictionary<string, string>> source = new List<Dictionary<string, string>>
            {
                dict1,
                dict2,
                dict3,
                dict4
            };

            vo.Dimensions = dimensions;
            vo.Source = source;

            return vo;
        }

        public ChartDto GetStackedLineChartDataVO()
        {
            ChartDto vo = new ChartDto();

            var dimensions = new List<string> { "流量入口", "邮件营销", "联盟广告", "视频广告", "直接访问", "搜索引擎" };
            List<Dictionary<string, string>> source = new List<Dictionary<string, string>>();

            for (int i = 1; i < dimensions.Count; i++)
            {

                //二维数组，其中第一行/列可以给出 维度名，也可以不给出，直接就是数据：
                Dictionary<string, string> dict = new Dictionary<string, string>
                {

                    { "流量入口", getWeekValue(i) },

                    { "邮件营销", new Random().Next(100, 999).ToString() },
                    { "联盟广告", new Random().Next(100, 999).ToString() },
                    { "视频广告", new Random().Next(100, 999).ToString() },
                    { "直接访问", new Random().Next(100, 999).ToString() },
                    { "搜索引擎", new Random().Next(100, 999).ToString() }
                };
                source.Add(dict);
            }

            vo.Dimensions = dimensions;
            vo.Source = source;


            //注意：如果使用了 dataset，那么可以在 dataset.dimensions 中定义 dimension ，或者在 dataset.source 的第一行/列中给出 dimension 名称。于是就不用在这里指定 dimension。但如果在这里指定了 dimensions，那么优先使用这里的。
            //二维数组，其中第一行/列可以给出 维度名，也可以不给出，直接就是数据：

            //注意，如果系列没有指定 data，并且 option 有 dataset，那么默认使用第一个 dataset。如果指定了 data，则不会再使用 dataset。
            //通常来说，数据用一个二维数组表示。如下，每一列被称为一个『维度』。
            /*
             series: [{
                 data: [
                     // 维度X   维度Y   其他维度 ...
                     [  3.4,    4.5,   15,   43],
                     [  4.2,    2.3,   20,   91],
                     [  10.8,   9.5,   30,   18],
                     [  7.2,    8.8,   18,   57]
                 ]
             }]
             */

            /*
             {
                // 默认第一个维度映射到类目轴，其他维度依次映射到series
	            "dimensions": ["流量入口", "邮件营销", "联盟广告", "视频广告", "直接访问", "搜索引擎"],
                "source": [{
		            "流量入口": "周一",
		            "邮件营销": "953",
		            "联盟广告": "713",
		            "视频广告": "468",
		            "直接访问": "149",
		            "搜索引擎": "574"
	            }, {
		            "流量入口": "周二",
		            "邮件营销": "778",
		            "联盟广告": "495",
		            "视频广告": "660",
		            "直接访问": "339",
		            "搜索引擎": "279"
	            }, {
		            "流量入口": "周三",
		            "邮件营销": "166",
		            "联盟广告": "617",
		            "视频广告": "116",
		            "直接访问": "468",
		            "搜索引擎": "462"
	            }, {
		            "流量入口": "周四",
		            "邮件营销": "843",
		            "联盟广告": "204",
		            "视频广告": "751",
		            "直接访问": "535",
		            "搜索引擎": "772"
	            }, {
		            "流量入口": "周五",
		            "邮件营销": "437",
		            "联盟广告": "492",
		            "视频广告": "139",
		            "直接访问": "980",
		            "搜索引擎": "923"
	            }] */
            return vo;
        }

        private string getWeekValue(int i)
        {
            string val = "未知";
            switch (i)
            {
                case 1:
                    val = "周一";
                    break;

                case 2:
                    val = "周二";
                    break;

                case 3:
                    val = "周三";
                    break;

                case 4:
                    val = "周四";
                    break;

                case 5:
                    val = "周五";
                    break;

                case 6:
                    val = "周六";
                    break;

                case 7:
                    val = "周日";
                    break;

                default:
                    break;
            }
            return val;
        }

        private string getProductName(int i)
        {
            switch (i)
            {
                case 1:
                    return "手机";

                case 2:
                    return "服饰";

                case 3:
                    return "日用";

                case 4:
                    return "电脑";

                default:
                    return "未知";
            }
        }
    }
}
