using System.Text.Json;

using Microsoft.Extensions.Caching.Memory;

namespace ProjectGeoShot.Game.Weather;

public interface IWeatherService
{
    Task<JsonElement> FetchWeatherAsync(double lat, double lon, IEnumerable<int>? altitudes = null);

    Task<ICollection<WeatherData>> GetWeatherAlongPathAsync(
        double startLat, double startLon,
        double endLat, double endLon,
        int numPoints = 5,
        IEnumerable<int>? altitudes = null);
}

public record WeatherData(double Latitude, double Longitude, JsonElement Data);

public class WeatherService : IWeatherService
{
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);

    public WeatherService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<JsonElement> FetchWeatherAsync(double lat, double lon, IEnumerable<int>? altitudes = null)
    {
        string key = $"{lat},{lon}";
        if (_cache.TryGetValue(key, out JsonElement cached))
        {
            return cached;
        }

        altitudes ??= new List<int> { 10, 80, 120 };
        var hourlyParams = new List<string>();
        foreach (var alt in altitudes)
        {
            hourlyParams.Add($"wind_speed_{alt}m");
            hourlyParams.Add($"wind_direction_{alt}m");
        }

        var query = new Dictionary<string, string?>
        {
            ["latitude"] = lat.ToString(System.Globalization.CultureInfo.InvariantCulture),
            ["longitude"] = lon.ToString(System.Globalization.CultureInfo.InvariantCulture),
            ["current_weather"] = "true",
            ["hourly"] = string.Join(',', hourlyParams)
        };

        try
        {
            using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
            var uri = QueryHelpers.AddQueryString("https://api.open-meteo.com/v1/forecast", query);
            var response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var element = JsonDocument.Parse(json).RootElement.Clone();
            _cache.Set(key, element, _cacheDuration);
            return element;
        }
        catch (Exception ex)
        {
            var err = $"{{\"error\":\"{ex.Message.Replace("\"", "'")}\"}}";
            var element = JsonDocument.Parse(err).RootElement.Clone();
            _cache.Set(key, element, _cacheDuration);
            return element;
        }
    }

    public async Task<ICollection<WeatherData>> GetWeatherAlongPathAsync(double startLat, double startLon, double endLat, double endLon, int numPoints = 5, IEnumerable<int>? altitudes = null)
    {
        var points = InterpolatePoints(startLat, startLon, endLat, endLon, numPoints);
        var results = new List<WeatherData>();
        foreach (var (lat, lon) in points)
        {
            var data = await FetchWeatherAsync(lat, lon, altitudes);
            results.Add(new WeatherData(lat, lon, data));
        }
        return results;
    }

    private static IEnumerable<(double lat, double lon)> InterpolatePoints(double startLat, double startLon, double endLat, double endLon, int numPoints)
    {
        for (int i = 0; i < numPoints; i++)
        {
            double fraction = numPoints > 1 ? (double)i / (numPoints - 1) : 0;
            double lat = startLat + (endLat - startLat) * fraction;
            double lon = startLon + (endLon - startLon) * fraction;
            yield return (lat, lon);
        }
    }
}
