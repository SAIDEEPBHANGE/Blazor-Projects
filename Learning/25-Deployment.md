# Blazor Application Deployment

## Deployment Steps

1. Build and test with the release configuration.
2. Configure environment-specific connection strings and settings.
3. Apply EF Core migrations safely.
4. Publish static and server assets.
5. Configure HTTPS and application URLs.
6. Add health checks and logging.
7. Verify authentication callbacks and API origins.
8. Smoke-test routes and database operations.

```powershell
dotnet publish .\MyApp\MyApp.csproj -c Release -o .\publish
dotnet run --project .\MyApp\MyApp.csproj --environment Production
```

## Hosting

- Interactive Server apps require ASP.NET Core hosting and SignalR support.
- WebAssembly static builds can use static web hosting plus a separate API.
- Azure App Service, containers, IIS, Nginx, and reverse proxies can host server apps.
- Sticky sessions or distributed backplane patterns matter for some server deployment designs.

## Checklist

- HTTPS is enforced.
- Production secrets are external.
- Database migrations are reviewed.
- Errors are logged and hidden from users.
- Health checks cover database/API dependencies.
- Static assets use caching and content hashes.
- Telemetry identifies request latency and failures.
