using Azure.Storage.Blobs;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using SharedTools.Web;
using SharedTools.Web.Modules;

namespace ProjectGeoShot.Game;

public class WebModule : IWebModule
{
    public static GitHubWebModuleResource GitHubResource { get; } =
        new GitHubWebModuleResource(
            Owner: "andrewboudreau", 
            Repo: nameof(ProjectGeoShot), 
            FilenameBuilder: v => $"{nameof(ProjectGeoShot)}.Game-{v}.dll");

    public void Configure(WebApplication app)
    {
        return;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        //var connection = builder.Configuration["AzureBlobConnection"] ?? "UseDevelopmentStorage=true";
        var connection = "UseDevelopmentStorage=true";
        services.AddSingleton<IBattleStorage>(sp =>
            new AzureBlobBattleStorage(new BlobServiceClient(connection), "battles"));
    }
}
