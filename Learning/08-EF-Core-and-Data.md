# EF Core And Data

- EF Core maps CLR entities to relational tables.
- `DbContext` manages querying and change tracking.
- `DbSet<T>` represents an entity collection.
- Migrations generate and apply database schema changes.

```csharp
public sealed class Expense
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public decimal Amount { get; set; }
    public DateTime ExpenseDate { get; set; }
}
```

```csharp
var expenses = await dbContext.Expenses
    .AsNoTracking()
    .Where(x => x.UserId == userId)
    .OrderByDescending(x => x.ExpenseDate)
    .ToListAsync();
```

## Change Tracking

- Tracked entities detect changes for `SaveChanges`.
- `AsNoTracking` is appropriate for read-only queries.
- Use one scoped `DbContext` per unit of work.
- Do not keep a context alive for the lifetime of a server circuit unless the design is deliberate.

## Repository Pattern

- Repositories group persistence operations behind interfaces.
- Components call services or repositories; they do not embed LINQ database operations.
- The pattern should reduce duplication but should not expose `IQueryable` as a public abstraction.

## Migrations

```powershell
dotnet ef migrations add InitialCreate --project Personal-Expense-Tracker
dotnet ef database update --project Personal-Expense-Tracker
```
