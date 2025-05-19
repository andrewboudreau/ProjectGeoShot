
using ProjectGeoShot.Web.Features.Weather;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMemoryCache()
    .AddSingleton<IWeatherService, WeatherService>()
    .AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();
