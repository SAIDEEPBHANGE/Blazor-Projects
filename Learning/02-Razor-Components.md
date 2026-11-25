# Razor Components

## Razor Syntax

- `@expression` evaluates C# inline.
- `@code { }` contains fields and methods.
- `@if`, `@foreach`, `@switch`, and `@for` control markup.
- `@page "/route"` declares a routable component.
- `@onclick="Handler"` attaches a C# event handler.

```razor
<h1>@Title</h1>
@foreach (var item in Items)
{
    <p>@item</p>
}
@code {
    [Parameter] public string Title { get; set; } = "";
    [Parameter] public IReadOnlyList<string> Items { get; set; } = [];
}
```

## Blazor Components

- Components are reusable units of markup and behavior.
- A component can accept parameters, maintain state, handle events, and contain child components.
- Components have the same lifecycle as any .NET object in the rendering model.
- Prefer small, focused components over large pages containing every responsibility.

```razor
<TodoRow Item="task" OnDelete="DeleteTask" />
```

## Component Identity

- Blazor tracks components within the same logical location and sequence.
- Use `@key` to preserve or reset identity when list items change.

```razor
@foreach (var student in students)
{
    <StudentRow @key="student.Id" Student="student" />
}
```

## Repository Examples

- [ToDoApp](../ToDoApp) contains page and table components.
- [StudentFormModal.razor](../Student-Management-System/Components/Pages/StudentFormModal.razor) demonstrates form UI.
