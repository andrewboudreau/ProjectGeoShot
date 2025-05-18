# ProjectGeoShot
Turn-based, geo-anchored "Gorillas/Worms" with real-world terrain & live wind data.

## Weather Module (C#)
A small .NET console application located in `WeatherFetcher/` demonstrates how to retrieve current weather and wind data from [Open-Meteo](https://open-meteo.com/). Weather information can be fetched for multiple points along a geographic path with wind speeds at different altitudes.

### Building
Requires the .NET 6 SDK or newer.

```bash
dotnet build WeatherFetcher/WeatherFetcher.csproj
```

### Running
```bash
dotnet run --project WeatherFetcher
```
This queries Open-Meteo for a path from San Francisco to Los Angeles and prints the results. Network access is required for the API call.
