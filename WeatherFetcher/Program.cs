using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherFetcher
{
    public record WeatherData(double Latitude, double Longitude, JsonElement Data);

    public static class Weather
    {
        public static async Task<JsonElement> FetchWeatherAsync(double lat, double lon, IEnumerable<int>? altitudes = null)
        {
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
                return JsonDocument.Parse(json).RootElement.Clone();
            }
            catch (Exception ex)
            {
                var err = $"{{\"error\":\"{ex.Message.Replace("\"", "'")}\"}}";
                return JsonDocument.Parse(err).RootElement.Clone();
            }
        }

        public static async Task<List<WeatherData>> GetWeatherAlongPathAsync(double startLat, double startLon, double endLat, double endLon, int numPoints = 5, IEnumerable<int>? altitudes = null)
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

    internal static class QueryHelpers
    {
        public static string AddQueryString(string uri, IDictionary<string, string?> query)
        {
            var builder = new System.Text.StringBuilder(uri);
            bool hasQuestion = uri.Contains('?');
            foreach (var kvp in query)
            {
                builder.Append(hasQuestion ? '&' : '?');
                hasQuestion = true;
                builder.Append(Uri.EscapeDataString(kvp.Key));
                builder.Append('=');
                builder.Append(Uri.EscapeDataString(kvp.Value ?? string.Empty));
            }
            return builder.ToString();
        }
    }

    internal class Program
    {
        static async Task Main()
        {
            var start = (37.7749, -122.4194); // San Francisco
            var end = (34.0522, -118.2437);   // Los Angeles

            var weatherData = await Weather.GetWeatherAlongPathAsync(start.Item1, start.Item2, end.Item1, end.Item2);

            foreach (var entry in weatherData)
            {
                Console.WriteLine($"Lat: {entry.Latitude}, Lon: {entry.Longitude}, Data: {entry.Data}");
            }
        }
    }
}
