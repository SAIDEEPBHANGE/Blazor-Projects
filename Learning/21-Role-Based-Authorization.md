# Role-Based Authorization

Roles are common claims that divide permissions into categories.

```csharp
[Authorize(Roles = "Admin")]
public async Task DeleteAsync(int id)
{
    // Delete only for an authorized administrator.
}
```

```razor
@attribute [Authorize(Roles = "Admin")]
```

```razor
<AuthorizeView Roles="Admin">
    <Authorized>
        <button>Administrator Action</button>
    </Authorized>
</AuthorizeView>
```

## Claims and Policies

- Roles answer "does this identity have the Admin role?"
- Claims answer "does this identity contain an assertion?"
- Policies combine requirements and can express complex rules.

```csharp
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanManageExpenses", policy =>
        policy.RequireClaim("permission", "expenses.manage"));
});
```

Use server-side API and service checks even when UI hides the action.
