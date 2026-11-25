# Routing And Navigation

## Routing

- `@page "/students"` defines a route.
- Routes can contain parameters: `@page "/students/{Id:int}"`.
- `Routes.razor` configures the router and layout.
- `NotFoundPage` defines a component for unmatched routes.

```razor
@page "/students/{Id:int}"
@code {
    [Parameter] public int Id { get; set; }
}
```

## Navigation

- Use `NavigationManager.NavigateTo("/students")` for imperative navigation.
- Use `<NavLink>` for navigation links and active CSS classes.
- Use `<a href="/students">` for normal links.
- `NavigateTo(..., forceLoad: true)` performs a full browser navigation.

```razor
<NavLink href="/weather" Match="NavLinkMatch.All">
    Weather
</NavLink>
```

## Protected Routes

- Use `<AuthorizeView>` to show UI based on the current user.
- Use `[Authorize]` on a page/component for access control.
- `[Authorize(Roles = "Admin")]` enforces a role.
- `AuthorizeRouteView` replaces unauthorized route content.

```razor
@attribute [Authorize(Roles = "Admin")]
```

Authorization is always also enforced in services, APIs, repositories, or database operations.
