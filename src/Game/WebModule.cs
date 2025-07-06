using Azure.Storage.Blobs;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
        // 1. The module tells the host how to configure its options from the IConfiguration
        builder.Services.AddOptions<AzureBlobStorageOptions>()
            .BindConfiguration(AzureBlobStorageOptions.SectionName)
            .ValidateDataAnnotations() // This will check the [Required] attributes at startup
            .ValidateOnStart();

        Console.WriteLine("OMGO OMG OMG ConfigureBuilder for ProjectGeoShot");
        // 2. The module registers its services, consuming the strongly-typed options
        builder.Services.AddSingleton<IBattleStorage>(sp =>
        {
            // We request IOptions<T> from the service provider
            var options = sp.GetRequiredService<IOptions<AzureBlobStorageOptions>>().Value;

            var blobServiceClient = new BlobServiceClient(options.ConnectionString);
            return new AzureBlobBattleStorage(blobServiceClient, options.BattlesContainerName);
        });
    }
}
