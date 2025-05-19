using System.Text.Json;

namespace GeoShot.Web.Features.Weather;

public record WeatherData(double Latitude, double Longitude, JsonElement Data);
