using SharedTools.Web.Modules;

var builder = WebApplication.CreateBuilder(args);

await builder.AddApplicationPartModules([
    "ProjectGeoShot.Game",
    "SharedTools.ModuleManagement"
]);

var app = builder.Build();

app.UseApplicationPartModules();

// Add a simple home page that redirects to the Scoreboard module
app.MapGet("/", () => Results.Redirect("/ProjectGeoShot/"));

app.Run();
