namespace ProjectGeoShot.Game;

public class Battle
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public string Name { get; set; } = string.Empty;
    public List<Player> Players { get; } = new();
    public List<Shot> Shots { get; } = new();

    public bool IsFull => Players.Count >= 16;
}
