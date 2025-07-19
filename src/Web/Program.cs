using SharedTools.Web.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMemoryCache()
    .AddRazorPages();

await builder.AddApplicationPartModules(["ProjectGeoShot.Game"]);

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseApplicationPartModules();

app.MapRazorPages();
app.Run();

