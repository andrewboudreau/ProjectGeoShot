namespace ProjectGeoShot.Game; 

public class Player
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public string Name { get; set; } = string.Empty;
}
