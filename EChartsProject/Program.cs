using EChartsProject.Dtos;
using EChartsProject.Models;
using EChartsProject.Services;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;


var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
builder.Services.AddControllersWithViews();
#region 启动时，注册Mapster的TypeAdapterConfig和ServiceMapper
var config = new TypeAdapterConfig();
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>(); 
#endregion
builder.Services.AddDbContext<BcSwapDbContext>(options =>
      options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<UserInfoService>();
#region SqlSugar注入
builder.Services.AddScoped<ISqlSugarClient>(o =>
{
    return new SqlSugar.SqlSugarClient(new SqlSugar.ConnectionConfig()
    {
        ConnectionString = "Data Source=.;Initial Catalog=eUI;Persist Security Info=True;User ID=sa;Password=wu199010;Pooling=False;MAX Pool Size=2000;Min Pool Size=1;Connection Lifetime=30;",//必填, 数据库连接字符串
        DbType = DbType.SqlServer,//必填, 数据库类型
        IsAutoCloseConnection = true,//默认false, 时候知道关闭数据库连接, 设置为true无需使用using或者Close操作
        InitKeyType = SqlSugar.InitKeyType.SystemTable//默认SystemTable, 字段信息读取, 如：该属性是不是主键，标识列等等信息
    });
});
#endregion

var app = builder.Build();
// 在启动时配置映射
DtoMapping.ConfigureMappings();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
