using ProjectGeoShot.Web.Features.Game;
using Azure.Storage.Blobs;

using SharedTools.Web.Modules;

var builder = WebApplication.CreateBuilder(args);

//
//builder.AddWebModules(Path.Combine(builder.Environment.ContentRootPath, "WebModules"));

var version = "0.1.2";
await builder.AddWebModules([
    $"https://github.com/andrewboudreau/ProjectGeoShot/releases/download/v{version}/ProjectGeoShot.Razor-{version}.dll"
]);

builder.Services
    .AddMemoryCache()
    .AddRazorPages();

await builder.AddWebModules([""]);

var connection = builder.Configuration["AzureBlobConnection"] ?? "UseDevelopmentStorage=true";
builder.Services.AddSingleton<IBattleStorage>(sp =>
    new AzureBlobBattleStorage(new BlobServiceClient(connection), "battles"));

var app = builder.Build();

app.UseStaticFiles();
app.UseWebModules();
app.UseRouting();
app.MapRazorPages();
app.UseWebModules();

app.Run();