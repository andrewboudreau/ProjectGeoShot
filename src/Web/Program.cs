
using ProjectGeoShot.Web.Features.Weather;
using ProjectGeoShot.Web.Features.Game;
using Azure.Storage.Blobs;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMemoryCache()
    .AddSingleton<IWeatherService, WeatherService>()
    .AddRazorPages();

var connection = builder.Configuration["AzureBlobConnection"] ?? "UseDevelopmentStorage=true";
builder.Services.AddSingleton<IBattleStorage>(sp =>
    new AzureBlobBattleStorage(new BlobServiceClient(connection), "battles"));

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();
