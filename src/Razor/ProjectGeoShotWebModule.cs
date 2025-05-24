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
        throw new NotImplementedException();
    }

    public void ConfigureServices(WebApplicationBuilder builder)
    {
        var connection = builder.Configuration["AzureBlobConnection"] ?? "UseDevelopmentStorage=true";
        builder.Services.AddSingleton<IBattleStorage>(sp =>
            new AzureBlobBattleStorage(new BlobServiceClient(connection), "battles"));
    }
}
