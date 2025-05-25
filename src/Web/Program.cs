using SharedTools.Web.Modules;

using static SharedTools.Web.GitHubDownloadExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMemoryCache()
    .AddRazorPages();

var release = await GetLatestReleaseVersionAsync(
    BuildLatestUrl("ProjectGeoShot", "andrewboudreau").ToString());

var releaseUrl = BuildReleaseUrl(
    "ProjectGeoShot",
    "andrewboudreau",
    release.Version,
    v => $"ProjectGeoShot.Game-{v}.dll");

await builder.AddWebModules([releaseUrl.ToString()]);

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.UseWebModules();

app.Run();
