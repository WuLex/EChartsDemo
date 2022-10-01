using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SqlSugar;
using System.Data;

namespace EChartsProject.Controllers
{
    public class EchController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private readonly ISqlSugarClient _db;

        public EchController(IConfiguration configuration, ISqlSugarClient db)
        {
            _configuration = configuration;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Bar()
        {
            return View();
        }

        //string con = @"Data Source='" + System.Web.HttpContext.Current.Server.MapPath("~/app_data/chartdb.sdf") + "'";
        //DataSet ds = new DataSet();
        //SqlCeDataAdapter da = new SqlCeDataAdapter();
        //JavaScriptSerializer jsS = new JavaScriptSerializer();
        //List<object> lists = new List<object>();
        //string result = "";

        //public void ProcessRequest(HttpContext context)
        //{
        //    string command = context.Request["cmd"];

        //    switch (command)
        //    {
        //        case "pie":
        //            GetPie(context);
        //            break;
        //        case "bar":
        //            GetBars(context);
        //            break;
        //    };

        //}

        public async Task<string> GetPieAsync()
        {
            string sql = @"  select categoryname as name, count(*) as count from lists group by categoryname";
            var dt = await GetData(sql);
            var lists = new List<object>();
            foreach (DataRow dr in dt.Rows)
            {
                var obj = new { name = dr["name"], value = dr["count"] };
                lists.Add(obj);
            }

            var result = JsonConvert.SerializeObject(lists);
            return result;

            //jsS = new JavaScriptSerializer();
            //result = jsS.Serialize(lists);
            //context.Response.Write(result);
        }

        public async Task<string> GetBars()
        {
            string sql = @"  select CONVERT(NVARCHAR(10),createdate,120) as date,   count(*) as count from lists  group by CONVERT(NVARCHAR(10),createdate,120) ";
            var dt = await GetData(sql);
            var lists = new List<object>();

            foreach (DataRow dr in dt.Rows)
            {
                var obj = new { name = dr["date"], value = dr["count"] };
                lists.Add(obj);
            }
            var result = JsonConvert.SerializeObject(lists);

            return result;
            //context.Response.Write(result);
        }

        public async Task<DataTable> GetData(string sql)
        {
            var dt = await _db.Ado.GetDataTableAsync(sql);
            return dt;
        }
    }
}