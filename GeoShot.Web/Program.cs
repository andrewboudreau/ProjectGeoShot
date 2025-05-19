using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<GeoShot.Web.Features.Weather.IWeatherService,
    GeoShot.Web.Features.Weather.WeatherService>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();

app.Run();
