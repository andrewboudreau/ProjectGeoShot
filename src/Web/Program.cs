using SharedTools.Web;
using SharedTools.Web.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMemoryCache()
    .AddRazorPages();

var projectGeoShot = await ProjectGeoShotResourceUrl();
var projectGeoShotAssembly = typeof(ProjectGeoShot.Game.WebModule).Assembly;

await builder.AddWebModules([], [projectGeoShotAssembly]);

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.UseWebModules();

app.Run();

static async Task<Uri> ProjectGeoShotResourceUrl()
{
    GitHubWebModuleResource resource = new(
        Owner: "andrewboudreau",
        Repo: nameof(ProjectGeoShot),
        FilenameBuilder: v => $"{nameof(ProjectGeoShot)}.Game-{v}.dll");

    return await GitHubDownloadExtensions.BuildLatestResourceUrl(resource);
}