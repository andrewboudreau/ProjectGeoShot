namespace ProjectGeoShot.Web.Features.Game;

public class Battle
{
    public Guid Id { get; init; } = GuidV7.NewGuid();
    public string Name { get; set; } = string.Empty;
    public List<Player> Players { get; } = new();
    public List<Shot> Shots { get; } = new();

    public bool IsFull => Players.Count >= 16;
}
