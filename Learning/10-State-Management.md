# State Management

## State Types

| State             | Lifetime                     | Typical use                         |
| ----------------- | ---------------------------- | ----------------------------------- |
| Component state   | Component instance           | Form or page data                   |
| Cascading state   | Component subtree            | Authentication/theme                |
| Scoped service    | Server circuit/request scope | Shared workflow state               |
| Singleton service | Application lifetime         | Shared cache or configuration       |
| Browser storage   | Browser lifetime             | Preferences and non-sensitive cache |
| Database          | Persistent                   | Source of truth                     |

```csharp
builder.Services.AddScoped<ExpenseFilterState>();
```

```razor
@inject ExpenseFilterState FilterState
```

- State changes may trigger rerendering.
- Notify other components with a state-changed event or a state service.
- Never put passwords or access tokens in local storage unless a threat model explicitly approves the design.
- Persistent data should be considered untrusted until validated on the server.
