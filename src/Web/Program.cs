using SharedTools.Web;
using SharedTools.Web.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMemoryCache()
    .AddRazorPages();

var projectGeoShot = await ProjectGeoShotResourceUrl();
await builder.AddWebModules([projectGeoShot]);

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


    var (version, _) = await GitHubDownloadExtensions
        .GetLatestReleaseVersionAsync(resource);

    return GitHubDownloadExtensions
        .BuildReleaseUrl(resource with { Version = version });
}