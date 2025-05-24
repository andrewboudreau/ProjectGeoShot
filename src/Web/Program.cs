using SharedTools.Web.Modules;

using System.Net;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMemoryCache()
    .AddRazorPages();

var release = await GetLatestReleaseVersionAsync("https://github.com/andrewboudreau/ProjectGeoShot/releases/latest");
var version = release.Version;

await builder.AddWebModules([
    $"https://github.com/andrewboudreau/ProjectGeoShot/releases/download/v{version}/ProjectGeoShot.Game-{version}.dll"
]);

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.UseWebModules();

app.Run();


static async Task<(string Version, Uri ReleaseUrl)> GetLatestReleaseVersionAsync(string latestUrl)
{
    // Create a handler to prevent auto-redirect
    var handler = new HttpClientHandler { AllowAutoRedirect = false };
    using var httpClient = new HttpClient(handler);

    var response = await httpClient.GetAsync(latestUrl);
    if (response.StatusCode == HttpStatusCode.Redirect || response.StatusCode == HttpStatusCode.MovedPermanently)
    {
        var redirectUrl = response.Headers.Location?.ToString()
            ?? throw new InvalidOperationException($"latestUrl returned HttpStatusCode={response.StatusCode} but location header is empty. Url=\"{latestUrl}\"");

        var match = Regex.Match(redirectUrl, @"tag/v(?<version>\d+\.\d+\.\d+)");
        if (match.Success)
        {
            return (Version: match.Groups["version"].Value, ReleaseUrl: new Uri(redirectUrl));
        }

        throw new InvalidOperationException($"latestUrl did redirect but couldn't parse the version from. Redirect=\"{redirectUrl}\" Url=\"{latestUrl}\"");
    }

    throw new InvalidOperationException($"latestUrl returned HttpStatusCode={response.StatusCode}. Url=\"{latestUrl}\"");
}