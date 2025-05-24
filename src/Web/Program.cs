using ProjectGeoShot.Web.Features.Weather;

using SharedTools.Web.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.AddWebModules(Path.Combine(builder.Environment.ContentRootPath, "WebModules"));

builder.Services
    .AddMemoryCache()
    .AddSingleton<IWeatherService, WeatherService>()
    .AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();
app.UseWebModules();
app.UseRouting();
app.MapRazorPages();

app.Run();