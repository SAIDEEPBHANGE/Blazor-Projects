# Error Handling

## Application Error Patterns

- Catch exceptions at the appropriate boundary, not around every statement.
- Convert recoverable service failures into clear UI states.
- Use `try/catch` for operations with a meaningful fallback or retry.
- Log unexpected exceptions with correlation context.
- Never expose stack traces or database details to browser users.

```csharp
try
{
    await weatherService.SearchAsync(city);
}
catch (HttpRequestException)
{
    message = "The weather service is currently unavailable.";
}
catch (TaskCanceledException)
{
    message = "The request timed out.";
}
finally
{
    isLoading = false;
}
```

## HTTP APIs

- Use status codes for the result category.
- Use `ProblemDetails` or a consistent error DTO.
- Map validation failures to 400/422 responses.
- Map missing resources to 404 responses.
- Map authorization failures to 403 responses.
- Map unexpected server failures to 500 responses and log the actual exception.
  In Interactive Server, configure an error boundary or global exception handling according to the application hosting model.
