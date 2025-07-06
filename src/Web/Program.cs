using SharedTools.Web.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMemoryCache()
    .AddRazorPages();

await builder.AddWebModules(["ProjectGeoShot.Game"]);

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.UseWebModules();

app.Run();

