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
=======

This project aims to experiment with rendering real-world topography in a very simple ASP.NET application. The `/GeoShot.Web` folder contains a minimal API project with Razor Pages.

The index page displays a map using open topographic map tiles from [OpenTopoMap](https://opentopomap.org). You can provide latitude and longitude values to center the map on a specific location.

## How to build and run

1. Ensure the [.NET 9 SDK](https://dotnet.microsoft.com/download) is installed.
2. From the `GeoShot.Web` directory run:
   ```bash
   dotnet run
   ```
3. Navigate to `http://localhost:5000` in your browser.

Use the form on the page to update the latitude and longitude of the displayed location.
