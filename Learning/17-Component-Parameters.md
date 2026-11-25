# Component Parameters

```razor
<StudentCard Student="selectedStudent" Editable="true"
             OnSaved="RefreshAsync" />
```

```csharp
[Parameter, EditorRequired]
public Student Student { get; set; } = default!;
[Parameter]
public bool Editable { get; set; }
[Parameter]
public EventCallback OnSaved { get; set; }
```

- Parameters pass data from a parent component to a child.
- Mark required parameters with `[EditorRequired]`.
- Do not mutate parameters inside a child; raise an `EventCallback` instead.
- A parameter is typically reset when the parent rerenders or changes parameters.
- Use immutable or copy-only data for predictable state.

## Cascading Parameters

```csharp
[CascadingParameter] public Task<AuthenticationState>? AuthState { get; set; }
```

Cascading values, such as authentication state or theme, flow down multiple component levels without repeated parameter declarations.
