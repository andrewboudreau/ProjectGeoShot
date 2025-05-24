using ProjectGeoShot.Web.Features.Weather;
using ProjectGeoShot.Web.Features.Game;
using Azure.Storage.Blobs;

using SharedTools.Web.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.AddWebModules(Path.Combine(builder.Environment.ContentRootPath, "WebModules"));

builder.Services
    .AddMemoryCache()
    .AddSingleton<IWeatherService, WeatherService>()
    .AddRazorPages();

var connection = builder.Configuration["AzureBlobConnection"] ?? "UseDevelopmentStorage=true";
builder.Services.AddSingleton<IBattleStorage>(sp =>
    new AzureBlobBattleStorage(new BlobServiceClient(connection), "battles"));

var app = builder.Build();

app.UseStaticFiles();
app.UseWebModules();
app.UseRouting();
app.MapRazorPages();

app.Run();