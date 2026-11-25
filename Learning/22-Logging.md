# Logging

ASP.NET Core provides structured logging through `ILogger<T>`.

```csharp
public sealed class ExpenseService
{
    private readonly ILogger<ExpenseService> logger;
    public ExpenseService(ILogger<ExpenseService> logger)
    {
        this.logger = logger;
    }
    public async Task SaveAsync(Expense expense)
    {
        logger.LogInformation(
            "Saving expense {ExpenseId} for user {UserId}",
            expense.Id,
            expense.UserId);
    }
}
```

## Levels

| Level       | Use                                   |
| ----------- | ------------------------------------- |
| Trace       | Detailed diagnostic data              |
| Debug       | Development diagnostics               |
| Information | Normal application events             |
| Warning     | Unexpected but recoverable situations |
| Error       | Failed operation                      |
| Critical    | Application-wide failure              |

- Use named fields rather than interpolating secrets.
- Never log passwords, tokens, card numbers, or excessive personal data.
- Correlation identifiers help trace a request across components and services.
- Configure providers and log levels in `appsettings.json` or environment configuration.
