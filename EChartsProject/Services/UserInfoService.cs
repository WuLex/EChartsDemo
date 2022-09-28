using EChartsProject.Models;
using SqlSugar;
using System.Data;
using System.Text;

namespace EChartsProject.Services
{
    public class UserInfoService
    {
        private readonly IConfiguration _configuration;
        private readonly ISqlSugarClient _db;

        public UserInfoService(IConfiguration configuration, ISqlSugarClient db)
        {
            _configuration = configuration;
            _db = db;
        }

        //用户信息(表格显示)
        public DataTable SearchInfo(UserParams userInfo)
        {
            //如果你的数据库是sqlserver 2012以上,也可以使用getPage2012方法
            //DataTable dtUserInfo = DBHelper.SearchSql(getPage2005(userInfo));
            DataTable dtUserInfo = _db.Ado.GetDataTable(getPage2005(userInfo));
            return dtUserInfo;
        }

        //添加用户
        public async Task<bool> Add(UserInfo userInfo)
        {
            StringBuilder sbAddUser = new StringBuilder();
            sbAddUser.AppendLine("insert into dbo.UserInfo(Name,Tel,[Address],Pwd,Email,Gender)");
            sbAddUser.AppendLine("Values(");
            sbAddUser.Append("'" + userInfo.Name + "',");
            sbAddUser.Append("'" + userInfo.Tel.ToString() + "',");
            sbAddUser.Append("'" + userInfo.Address + "',");
            sbAddUser.Append("'" + userInfo.Pwd + "',");
            sbAddUser.Append("'" + userInfo.Email + "',");
            sbAddUser.Append("'" + userInfo.Gender + "'");
            sbAddUser.Append(")");

            //int iResult = DBHelper.ExcuteNoQuerySql(sbAddUser.ToString());
            int iResult = await _db.Ado.ExecuteCommandAsync(sbAddUser.ToString());
            if (iResult == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //修改用户
        public async Task<bool> EditAsync(UserInfo userInfo)
        {
            StringBuilder sbAddUser = new StringBuilder();
            sbAddUser.Append("Update dbo.UserInfo Set Name ='" + userInfo.Name + "', ");
            sbAddUser.Append("Tel='" + userInfo.Tel + "',");
            sbAddUser.Append("[Address]='" + userInfo.Address + "',");
            sbAddUser.Append("Pwd='" + userInfo.Pwd + "',");
            sbAddUser.Append("Email='" + userInfo.Email + "',");
            sbAddUser.Append("Gender='" + userInfo.Gender + "'");
            sbAddUser.AppendLine("where Id=" + userInfo.Id);

            //int iResult = DBHelper.ExcuteNoQuerySql(sbAddUser.ToString());
            int iResult = await _db.Ado.ExecuteCommandAsync(sbAddUser.ToString());
            if (iResult == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //删除用户
        public async Task<bool> DelAsync(UserParams userInfo)
        {
            StringBuilder sbAddUser = new StringBuilder();
            sbAddUser.Append("Delete From dbo.UserInfo Where Id=" + userInfo.id);

            //int iResult = DBHelper.ExcuteNoQuerySql(sbAddUser.ToString());
            int iResult = await _db.Ado.ExecuteCommandAsync(sbAddUser.ToString());
            if (iResult == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //根据用户ID获取用户信息
        public DataTable GetUserInfo(UserParams userInfo)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendLine("Select Id,Name,Tel,Address,Pwd,Email,Cast(Gender as char(1)) gender from dbo.UserInfo With(Nolock)");
            sbSI.AppendLine("Where Id =" + userInfo.id);

            //DataTable dtUserInfo = DBHelper.SearchSql(sbSI.ToString());
            DataTable dtUserInfo = _db.Ado.GetDataTable(sbSI.ToString());
            return dtUserInfo;
        }

        //更新用户头像图片路径
        public async Task<bool> UpdatePicAsync(UserInfo userInfo)
        {
            StringBuilder sbAddUser = new StringBuilder();
            sbAddUser.Append("Update dbo.UserInfo Set PicUrl ='" + userInfo.PicUrl + "' ");
            sbAddUser.AppendLine("Where Id=" + userInfo.Id);

            //int iResult = DBHelper.ExcuteNoQuerySql(sbAddUser.ToString());
            int iResult = await _db.Ado.ExecuteCommandAsync(sbAddUser.ToString());
            if (iResult == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //获取用户总数(按性别分组)
        public DataTable GetUserGenderGroup()
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendLine("Select Gender,Count(Gender) GenderCount From dbo.UserInfo With(Nolock) Group By Gender");

            //DataTable dtUserGender = DBHelper.SearchSql(sbSI.ToString());
            DataTable dtUserGender = _db.Ado.GetDataTable(sbSI.ToString());
            return dtUserGender;
        }

        //获取用户总数(按天分组),显示最近7天数据
        public DataTable GetUserDateGroup()
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendLine("Select Top 7 Convert(varchar(10), CreateTime, 120) CreateTime,");
            sbSI.AppendLine("Count(*) CTCount From dbo.UserInfo With(Nolock)");
            sbSI.AppendLine("Group by  Convert(varchar(10), CreateTime, 120)");
            sbSI.AppendLine("Order by CreateTime Desc");

            //DataTable dtUserGender = DBHelper.SearchSql(sbSI.ToString());
            DataTable dtUserGender = _db.Ado.GetDataTable(sbSI.ToString());
            return dtUserGender;
        }

        #region Sql Server2005及以上的分页

        // <summary>
        /// Sql Server2005及以上的分页
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>数据库查询语句</returns>
        private string getPage2005(UserParams userInfo)
        {
            StringBuilder sbSI = new StringBuilder();
            //SqlServer2005及以上分页方式
            if (!userInfo.isExport)
            {
                sbSI.AppendLine("Declare @PageSize int, @PageIndex int  ");
                sbSI.AppendLine("Set @PageSize = " + userInfo.rows);
                sbSI.AppendLine("Set @PageIndex = " + userInfo.page);
                sbSI.AppendLine("Select Id,Name,Tel,[Address],Pwd,Email,Gender,Convert(varchar(10),CreateTime,120) CreateTime,PicUrl,Total From ( ");
            }
            sbSI.Append("Select ");
            if (!userInfo.isExport)
            {
                sbSI.AppendLine("Row_Number() over (order by CreateTime Desc) RowNum,");
            }
            sbSI.AppendLine("Id,Name,Tel,  [Address],Pwd,Email,Cast(Gender as char(1)) Gender,CreateTime,PicUrl");
            if (!userInfo.isExport)
            {
                sbSI.AppendLine(",COUNT(*) OVER(PARTITION BY '') AS Total ");
            }
            sbSI.AppendLine("From [dbo].UserInfo With(Nolock)");

            if (!userInfo.isExport)
            {
                sbSI.AppendLine(") A ");
            }
            sbSI.AppendLine("Where 1=1");
            if (!userInfo.isExport)
            {
                sbSI.AppendLine("And  A.RowNum between (((@PageIndex-1)*@PageSize)+1) and (@PageIndex*@PageSize)");
            }
            sbSI.AppendLine("And Name like '%" + userInfo.name + "%'");
            //添加创建日期查询
            if (userInfo.stTime.Year != 1 && userInfo.edTime.Year != 1)
            {
                sbSI.AppendLine("And CreateTime Between '" + userInfo.stTime + "' And '" + userInfo.edTime.AddDays(1).AddSeconds(-1) + "'");
            }
            else if (userInfo.stTime.Year != 1)
            {
                sbSI.AppendLine("And CreateTime >= '" + userInfo.stTime + "'");
            }
            else if (userInfo.edTime.Year != 1)
            {
                sbSI.AppendLine("And CreateTime <= '" + userInfo.edTime.AddDays(1).AddSeconds(-1) + "' ");
            }
            sbSI.AppendLine("Order by Id Desc");

            return sbSI.ToString();
        }

        #endregion Sql Server2005及以上的分页

        #region Sql Server2012及以上的分页

        /// <summary>
        /// Sql Server2012及以上的分页
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>数据库查询语句</returns>
        private string getPage2012(UserParams userInfo)
        {
            StringBuilder sbSI = new StringBuilder();
            //SqlServer 2012及以上分页方式
            //如果是导出Excel，则不需要分页
            if (!userInfo.isExport)
            {
                sbSI.AppendLine("Declare @PageSize int, @PageIndex int  ");
                sbSI.AppendLine("Set @PageSize = " + userInfo.rows);
                sbSI.AppendLine("Set @PageIndex = " + userInfo.page);
            }
            sbSI.AppendLine("Select Id,Name,Tel,Address,Pwd,Email, Cast(Gender as char(1)) Gender,");
            sbSI.AppendLine("Convert(varchar(10),CreateTime,120) CreateTime,PicUrl");
            if (!userInfo.isExport)
            {
                sbSI.AppendLine(",COUNT(*) OVER(PARTITION BY '') AS Total ");
            }
            sbSI.AppendLine("From dbo.UserInfo With(Nolock)");
            sbSI.AppendLine("Where Name like '%" + userInfo.name + "%'");
            //添加创建日期查询
            if (userInfo.stTime.Year != 1 && userInfo.edTime.Year != 1)
            {
                sbSI.AppendLine("And CreateTime Between '" + userInfo.stTime + "' And '" + userInfo.edTime.AddDays(1).AddSeconds(-1) + "'");
            }
            else if (userInfo.stTime.Year != 1)
            {
                sbSI.AppendLine("And CreateTime >= '" + userInfo.stTime + "'");
            }
            else if (userInfo.edTime.Year != 1)
            {
                sbSI.AppendLine("And CreateTime <= '" + userInfo.edTime.AddDays(1).AddSeconds(-1) + "' ");
            }

            sbSI.AppendLine("Order by Id Desc");
            if (!userInfo.isExport)
            {
                sbSI.AppendLine("OFFSET (@PageIndex-1)*@PageSize Rows  ");
                sbSI.AppendLine("FETCH NEXT @PageSize ROWS ONLY; ");
            }

            return sbSI.ToString();
        }

        #endregion Sql Server2012及以上的分页


        #region 图表
        //用户报表(饼图)
        public UserReportModel GetUserReport()
        {
            UserReportModel userReportModel = new UserReportModel();

            DataTable dtList = GetUserGenderGroup();

            userReportModel.title = new TitleAttribute() { text = "用户性别比例", x = "center" };
            userReportModel.tooltip = new TooltipAttribute() { formatter = "{b} : {c} ({d}%)" };
            userReportModel.legend = new LegendAttribute() { data = new List<string> { "男", "女" }, left = "left", orient = "vertical" };
            userReportModel.series = new List<SeriesAttribute>() {
                new SeriesAttribute(){
                    type="pie",
                    data= new List<SeriesDataAttribute>(){
                        new SeriesDataAttribute(){
                            value= (dtList.Select("gender = 0").Count()>0)?Convert.ToInt32(dtList.Select("gender = 0")[0]["GenderCount"]):0,
                            name="女"},
                        new SeriesDataAttribute(){
                            value=(dtList.Select("gender = 1").Count()>0)?Convert.ToInt32(dtList.Select("gender = 1")[0]["GenderCount"]):0,
                            name="男"}
                    }}
            };

            return userReportModel;
        }

        //用户报表(柱状)
        public UserColumnReportModel GetUserColumnReport()
        {
            UserColumnReportModel userColumnReportModel = new UserColumnReportModel();

            DataTable dtList = GetUserDateGroup();
            //排序
            DataTable dtCopy = dtList.Copy();
            DataView dv = dtList.DefaultView;
            dv.Sort = "CreateTime Asc";
            dtCopy = dv.ToTable();

            List<string> xAxisData = new List<string>();
            List<string> seriesData = new List<string>();

            foreach (DataRow row in dtCopy.Rows)
            {
                xAxisData.Add(row["CreateTime"].ToString());
                seriesData.Add(row["CTCount"].ToString());
            }

            userColumnReportModel.color = new string[] { "#3398DB" };
            userColumnReportModel.tooltip = new TooltipCAttribute()
            {
                axisPointer = new AxisColumnPointer() { type = "shadow" },
                trigger = "axis"
            };
            userColumnReportModel.grid = new GridColumn()
            {
                left = "3%",
                right = "4%",
                bottom = "3%",
                containLabel = true
            };
            userColumnReportModel.xAxis = new List<XAxisColumn>()
            {
                 new XAxisColumn()
                 {
                     type="category",
                     data=xAxisData,
                     axisTick=new AxisTickColumn()
                     {
                         alignWithLabel=true
                     }
                 }
            };
            userColumnReportModel.yAxis = new List<YAxisColumn>()
            {
                new YAxisColumn()
                {
                    type="value"
                }
            };
            userColumnReportModel.series = new List<SeriesColumn>()
            {
                new SeriesColumn()
                {
                    name="总人数",
                    type="bar",
                    barWidth="60%",
                    data=seriesData
                }
            };

            return userColumnReportModel;
        }

        //用户报表(线形)
        public UserColumnReportModel GetUserLineReport()
        {
            UserColumnReportModel userColumnReportModel = new UserColumnReportModel();

            DataTable dtList = GetUserDateGroup();
            //排序
            DataTable dtCopy = dtList.Copy();
            DataView dv = dtList.DefaultView;
            dv.Sort = "CreateTime Asc";
            dtCopy = dv.ToTable();

            List<string> xAxisData = new List<string>();
            List<string> seriesData = new List<string>();

            foreach (DataRow row in dtCopy.Rows)
            {
                xAxisData.Add(row["CreateTime"].ToString());
                seriesData.Add(row["CTCount"].ToString());
            }

            userColumnReportModel.color = new string[] { "red" };
            userColumnReportModel.tooltip = new TooltipCAttribute()
            {
                //axisPointer = new AxisColumnPointer() { type = "shadow" },
                trigger = "item"
            };
            userColumnReportModel.xAxis = new List<XAxisColumn>()
            {
                 new XAxisColumn()
                 {
                     type="category",
                     data=xAxisData,
                     //axisTick=new AxisTickColumn()
                     //{
                     //    alignWithLabel=true
                     //}
                 }
            };
            userColumnReportModel.yAxis = new List<YAxisColumn>()
            {
                new YAxisColumn()
                {
                    type="value"
                }
            };
            userColumnReportModel.series = new List<SeriesColumn>()
            {
                new SeriesColumn()
                {
                    name="总人数",
                    type="line",
                   //barWidth="60%",
                    data=seriesData
                }
            };

            return userColumnReportModel;
        }
        #endregion
    }
}