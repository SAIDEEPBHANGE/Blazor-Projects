# Component Lifecycle

## Main Lifecycle Methods

| Method                                     | Purpose                                         |
| ------------------------------------------ | ----------------------------------------------- |
| `OnInitialized` / `OnInitializedAsync`     | First initialization                            |
| `OnParametersSet` / `OnParametersSetAsync` | Run after parameters are supplied or changed    |
| `OnAfterRender` / `OnAfterRenderAsync`     | Run after a render; use for DOM/JS work         |
| `ShouldRender`                             | Return false to prevent unnecessary rerendering |
| `Dispose` / `DisposeAsync`                 | Release subscriptions and timers                |

```csharp
protected override async Task OnInitializedAsync()
{
    products = await ProductService.GetAsync();
}
public async ValueTask DisposeAsync()
{
    await subscription.DisposeAsync();
}
```

## Rules

- Initialize one-time state in `OnInitializedAsync`.
- Reload parameter-dependent state in `OnParametersSetAsync`.
- Do not call JavaScript or inspect DOM elements before the first render.
- Dispose timers, event subscriptions, HTTP cancellation sources, and other resources.
- `StateHasChanged` requests a render but does not create an asynchronous operation.
