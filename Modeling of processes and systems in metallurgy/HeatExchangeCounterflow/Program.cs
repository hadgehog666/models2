using HeatExchangeCounterflow.Data;
using HeatExchangeCounterflow.Services;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=heat.db"));

builder.Services.AddScoped<IHeatExchangeService, ModelsService>();

var app = builder.Build();

var cultures = new[] { new CultureInfo("ru-RU"), new CultureInfo("en-US") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("ru-RU"),
    SupportedCultures = cultures,
    SupportedUICultures = cultures
});

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Calculations}/{action=Index}/{id?}");

app.Run();
