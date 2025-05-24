using Azure.Storage.Blobs;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using ProjectGeoShot.Web.Features.Game;

using SharedTools.Web.Modules;

namespace ProjectGeoShot.Razor;

public class ProjectGeoShotWebModule : IWebModule
{
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
