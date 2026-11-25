# Service Creation and Registration

## Recommended Pattern

```csharp
public interface IStudentService
{
    Task<IReadOnlyList<Student>> GetAllAsync(CancellationToken cancellationToken);
}
public sealed class StudentService : IStudentService
{
    private readonly HttpClient httpClient;
    public StudentService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task<IReadOnlyList<Student>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await httpClient.GetFromJsonAsync<IReadOnlyList<Student>>(
            "api/students", cancellationToken) ?? [];
    }
}
```

```csharp
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddHttpClient<IStudentService, StudentService>();
```

- Interfaces enable substitution and testing.
- Services own reusable application operations.
- Constructors receive dependencies instead of resolving services manually.
- Keep classes focused: HTTP, persistence, and UI orchestration are separate concerns.
