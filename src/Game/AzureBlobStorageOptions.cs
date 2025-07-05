using System.ComponentModel.DataAnnotations;

namespace ProjectGeoShot.Game;

public class AzureBlobStorageOptions
{
    public const string SectionName = "ProjectGeoShot:AzureBlob";

    [Required(AllowEmptyStrings = false)]
    public string ConnectionString { get; set; } = default!;

    [Required(AllowEmptyStrings = false)]
    public string BattlesContainerName { get; set; } = default!;
}