using Azure.Storage.Blobs;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using SharedTools.Web.Modules;

namespace ProjectGeoShot.Game;

public class WebModule : IWebModule
{
    public void ConfigureApp(WebApplication app)
    {
        return;
    }

    public void ConfigureBuilder(WebApplicationBuilder builder)
    {
        var connection = builder.Configuration["AzureBlobConnection"] ?? "UseDevelopmentStorage=true";
        builder.Services.AddSingleton<IBattleStorage>(sp =>
            new AzureBlobBattleStorage(new BlobServiceClient(connection), "battles"));
    }
}
