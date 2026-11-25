# SQL Server Integration

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=...;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
```

## Production Practices

- Store production credentials in environment variables, Key Vault, user secrets, or managed identity.
- Never commit passwords or connection strings containing secrets.
- Use migrations for repeatable schema changes.
- Enable parameterized queries through EF Core.
- Use pooling and connection limits according to environment capacity.
- Validate TLS and certificate settings appropriately.

## Repository Example

[Personal-Expense-Tracker/Program.cs](../Personal-Expense-Tracker/Program.cs) registers SQL Server through `UseSqlServer`, applies migrations at startup, and tests database connectivity.
