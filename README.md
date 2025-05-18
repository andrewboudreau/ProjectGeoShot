# ProjectGeoShot

This project aims to experiment with rendering real-world topography in a very simple ASP.NET application. The `/GeoShot.Web` folder contains a minimal API project with Razor Pages.

The index page displays a map using open topographic map tiles from [OpenTopoMap](https://opentopomap.org). You can provide latitude and longitude values to center the map on a specific location.

## How to build and run

1. Ensure the [.NET 6 SDK](https://dotnet.microsoft.com/download) is installed.
2. From the `GeoShot.Web` directory run:
   ```bash
   dotnet run
   ```
3. Navigate to `http://localhost:5000` in your browser.

Use the form on the page to update the latitude and longitude of the displayed location.
