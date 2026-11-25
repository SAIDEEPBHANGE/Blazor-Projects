# Data Binding And Events

## Data Binding

- One-way binding displays a property: `@Name`.
- `@bind` generates a value assignment and change event.
- `@bind:event="oninput"` updates on each input event.
- `@bind:after="OnChanged"` runs code after the property is updated.
- Bind to simple component state and keep validation separate.

```razor
<input @bind="query" @bind:event="oninput" />
<p>@query</p>
@code {
    private string query = "";
}
```

## Event Handling

```razor
<button @onclick="Increment" @onclick:preventDefault>Increment</button>
<button @onclick="() => IncrementBy(5)">Increment by 5</button>
```

- Event handlers can be `void`, `Task`, or `async Task`.
- Use `async Task` for I/O operations.
- Do not perform blocking `.Result` or `.Wait()` calls.
- Events trigger a render automatically after the handler completes.

## Component Callbacks

```razor
[Parameter] public EventCallback<int> CountChanged { get; set; }
private async Task NotifyAsync()
{
    await CountChanged.InvokeAsync(10);
}
```

- `EventCallback<T>` notifies a parent component of a change.
- Prefer callbacks over direct mutation of child parameters.

## Repository Example

- [Counter.razor](../CounterApp/Components/Pages/Counter.razor) uses `@bind` and `@onclick`.
