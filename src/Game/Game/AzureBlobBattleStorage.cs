using System.Text.Json;
using Azure.Storage.Blobs;

namespace ProjectGeoShot.Game;

public class AzureBlobBattleStorage : IBattleStorage
{
    private readonly BlobContainerClient container;

    public AzureBlobBattleStorage(BlobServiceClient client, string containerName)
    {
        container = client.GetBlobContainerClient(containerName);
    }

    public async Task SaveAsync(Battle battle, CancellationToken ct = default)
    {
        await container.CreateIfNotExistsAsync(cancellationToken: ct);
        var blob = container.GetBlobClient(GetFileName(battle.Id));
        using var stream = new MemoryStream();
        await JsonSerializer.SerializeAsync(stream, battle, cancellationToken: ct);
        stream.Position = 0;
        await blob.UploadAsync(stream, overwrite: true, cancellationToken: ct);
    }

    public async Task<Battle?> LoadAsync(Guid battleId, CancellationToken ct = default)
    {
        var blob = container.GetBlobClient(GetFileName(battleId));
        if (!await blob.ExistsAsync(ct))
            return null;

        var response = await blob.DownloadAsync(cancellationToken: ct);
        return await JsonSerializer.DeserializeAsync<Battle>(response.Value.Content, cancellationToken: ct);
    }

    private static string GetFileName(Guid id) => $"{id}.json";
}
