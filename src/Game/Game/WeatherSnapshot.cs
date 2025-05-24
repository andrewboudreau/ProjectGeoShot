namespace ProjectGeoShot.Game;

public record WindInfo(double Speed, double Deg);

public record WeatherSnapshot(WindInfo Surface, WindInfo At500m);
