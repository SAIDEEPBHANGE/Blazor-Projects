# JavaScript Interoperability

JS interop invokes JavaScript from .NET or .NET from JavaScript.

```razor
@inject IJSRuntime JS
<button @onclick="CopyAsync">Copy</button>
@code {
    private async Task CopyAsync()
    {
        await JS.InvokeVoidAsync("navigator.clipboard.writeText", "Blazor");
    }
}
```

```javascript
window.copyText = async (text) => navigator.clipboard.writeText(text);
```

## JavaScript to .NET

```javascript
DotNet.invokeMethodAsync("MyAssembly", "ReceiveFromJavaScript", message);
```

```csharp
[JSInvokable]
public static Task ReceiveFromJavaScript(string message)
{
    return Task.CompletedTask;
}
```

## Rules

- Load JavaScript modules with `IJSRuntime.InvokeAsync<IJSObjectReference>("import", path)`.
- Call interop only after component rendering in `OnAfterRenderAsync`.
- Dispose JavaScript object references.
- Use interop for browser APIs, third-party JavaScript, and DOM operations that Blazor cannot cover cleanly.
- Do not expose secrets to JavaScript.
