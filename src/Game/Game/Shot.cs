namespace ProjectGeoShot.Game;

public class Shot
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public Guid PlayerId { get; init; }
    public double Angle { get; set; }
    public double Power { get; set; }
    public WeatherSnapshot Weather { get; set; } = new(new(0,0), new(0,0));
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
