using System.Text.Json;

namespace GeoShot.Web.Features.Weather;

public interface IWeatherService
{
    Task<JsonElement> FetchWeatherAsync(double lat, double lon, IEnumerable<int>? altitudes = null);
    Task<List<WeatherData>> GetWeatherAlongPathAsync(
        double startLat, double startLon,
        double endLat, double endLon,
        int numPoints = 5,
        IEnumerable<int>? altitudes = null);
}
