namespace ProjectGeoShot.Web.Features.Game;

public class Player
{
    public Guid Id { get; init; } = GuidV7.NewGuid();
    public string Name { get; set; } = string.Empty;
}
