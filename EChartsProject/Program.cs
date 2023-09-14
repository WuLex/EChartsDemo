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
#region ����ʱ��ע��Mapster��TypeAdapterConfig��ServiceMapper
var config = new TypeAdapterConfig();
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>(); 
#endregion
builder.Services.AddDbContext<BcSwapDbContext>(options =>
      options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<UserInfoService>();
#region SqlSugarע��
builder.Services.AddScoped<ISqlSugarClient>(o =>
{
    return new SqlSugar.SqlSugarClient(new SqlSugar.ConnectionConfig()
    {
        ConnectionString = "Data Source=.;Initial Catalog=eUI;Persist Security Info=True;User ID=sa;Password=wu199010;Pooling=False;MAX Pool Size=2000;Min Pool Size=1;Connection Lifetime=30;",//����, ���ݿ������ַ���
        DbType = DbType.SqlServer,//����, ���ݿ�����
        IsAutoCloseConnection = true,//Ĭ��false, ʱ��֪���ر����ݿ�����, ����Ϊtrue����ʹ��using����Close����
        InitKeyType = SqlSugar.InitKeyType.SystemTable//Ĭ��SystemTable, �ֶ���Ϣ��ȡ, �磺�������ǲ�����������ʶ�еȵ���Ϣ
    });
});
#endregion

var app = builder.Build();
// ������ʱ����ӳ��
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
