# ProjectGeoShot
Turn-based, geo-anchored "Gorillas/Worms" with real-world terrain & live wind data.

# Overview
A proof-of-concept artillery game where each player�s launcher sits at real-world coordinates.

# Project features:
 - Geo-Projection: Convert latitude/longitude into a flat game world (meters).
 - Live Weather: Fetch surface (and optional altitude) wind data at shot time for realistic trajectory drift.
 - Satellite & DEM Terrain: Stitch open-source imagery (NASA GIBS) and elevation (SRTM) into your playfield.
 - Deterministic Simulation: Exchange only { angle, power, weatherSnapshot } so both clients replay identical shots.
 - Modular Architecture: Swappable physics, weather, terrain, and networking layers (SignalR or REST).
 - Get started by cloning, entering your coords, and firing your first geo-ballistic shot across tens of kilometers!

## Weather Service
`ProjectGeoShot.Web` includes a lightweight weather service that retrieves data from
[Open-Meteo](https://open-meteo.com/). It can fetch conditions for a single
location or for several points along a path. Results are cached in memory for a
short period.

This project aims to experiment with rendering real-world topography in a very simple ASP.NET application. The `/ProjectGeoShot.Web` folder contains a minimal API project with Razor Pages.

The index page displays a map using open topographic map tiles from [OpenTopoMap](https://opentopomap.org). You can provide latitude and longitude values to center the map on a specific location.

## How to build and run

1. Ensure the [.NET 9 SDK](https://dotnet.microsoft.com/download) is installed.
2. From the `GeoShot.Web` directory run:
   ```bash
   dotnet run
   ```
3. Navigate to `http://localhost:5000` in your browser.

Use the form on the page to update the latitude and longitude of the displayed location.

## Future Work
- Animation & Mocap: See [ANIMATION_PIPELINE.md](ANIMATION_PIPELINE.md) for notes on rigs, formats, retargeting, and mocap conversion.
