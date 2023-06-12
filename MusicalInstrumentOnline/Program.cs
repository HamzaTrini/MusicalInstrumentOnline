using System.Net.Mail;
using System.Net;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration));
// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddMvc();
builder.Configuration.GetConnectionString("ConnectionName");
//services.AddSession(o => { o.IdleTimeout = TimeSpan.FromSeconds(60); });
builder.Services.AddSession(o => { o.IdleTimeout = TimeSpan.FromMinutes(15); });

var app = builder.Build();
var builderr = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("email.json");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
