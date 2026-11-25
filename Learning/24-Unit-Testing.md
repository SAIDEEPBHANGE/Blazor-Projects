# Unit Testing

## Test Pyramid

- Unit tests validate isolated classes and calculations.
- bUnit tests render components and simulate events.
- Integration tests validate ASP.NET Core startup and database/API behavior.
- End-to-end tests validate complete browser workflows.

```csharp
[Fact]
public void Counter_IncrementsByStep()
{
    var counter = new CounterModel();
    counter.Increment(3);
    Assert.Equal(3, counter.Value);
}
```

## Component Tests

- Render components in bUnit.
- Find elements with CSS selectors.
- Trigger click or input events.
- Assert markup and callback invocations.
- Replace services with mocks or test doubles.

## Practices

- Test behavior, not implementation details.
- Arrange, Act, Assert in each test.
- Use deterministic data and time.
- Keep database tests isolated.
- Run the smallest relevant suite during development, then run the full build/test suite before completion.
