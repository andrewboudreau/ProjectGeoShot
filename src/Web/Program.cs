using SharedTools.Web.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMemoryCache()
    .AddRazorPages();

var version = "0.1.4";
await builder.AddWebModules([
    $"https://github.com/andrewboudreau/ProjectGeoShot/releases/download/v{version}/ProjectGeoShot.Razor-{version}.dll"
]);

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.UseWebModules();

app.Run();