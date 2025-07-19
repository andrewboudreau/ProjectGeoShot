using Azure.Storage.Blobs;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using SharedTools.Web.Modules;

namespace ProjectGeoShot.Game;

public class WebModule : IApplicationPartModule
{
    public string Name => "ProjectGeoShot.Game";

    public void Configure(WebApplication app)
    {
        app.MapGet("/projectgeoshot-game/info", () => new
        {
            Module = Name,
            Version = GetType().Assembly.GetName().Version?.ToString() ?? "1.0.0",
            Status = "Active"
        });
    }

    public void ConfigureApplicationParts(ApplicationPartManager applicationPartManager)
    {
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // 1. The module tells the host how to configure its options from the IConfiguration
        services.AddOptions<AzureBlobStorageOptions>()
            .BindConfiguration(AzureBlobStorageOptions.SectionName)
            .ValidateDataAnnotations() // This will check the [Required] attributes at startup
            .ValidateOnStart();

        // 2. The module registers its services, consuming the strongly-typed options
        services.AddSingleton<IBattleStorage>(sp =>
        {
            // We request IOptions<T> from the service provider
            var options = sp.GetRequiredService<IOptions<AzureBlobStorageOptions>>().Value;

            var blobServiceClient = new BlobServiceClient(options.ConnectionString);
            return new AzureBlobBattleStorage(blobServiceClient, options.BattlesContainerName);
        });
    }
}
