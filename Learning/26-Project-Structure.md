# Blazor Project Structure

```text
MyApp/
├── Components/
│   ├── Pages/
│   ├── Layout/
│   ├── Routes.razor
│   └── App.razor
├── Models/
├── Services/
├── Data/
├── Properties/
├── wwwroot/
├── appsettings.json
└── Program.cs
```

| Item                      | Responsibility                              |
| ------------------------- | ------------------------------------------- |
| `Program.cs`              | Host setup, services, middleware, endpoints |
| `Components/App.razor`    | HTML shell and static/application assets    |
| `Components/Routes.razor` | Router and layout selection                 |
| `Components/Pages`        | Routable components                         |
| `Components/Layout`       | Shared application layouts                  |
| `Models`                  | Domain and form models                      |
| `Services`                | Reusable application behavior               |
| `Data`                    | EF Core context and migrations              |
| `wwwroot`                 | Static CSS, JavaScript, images              |

Keep folders by responsibility and use clear namespaces. Do not place database operations directly in markup-heavy components.
