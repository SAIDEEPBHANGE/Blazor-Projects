# Layout Components

- A layout is a reusable Razor component containing shared page structure.
- It typically includes navigation, headers, footers, and `@Body`.
- Pages are assigned layouts with `@layout MainLayout` or through the router.
- Components can create isolated CSS and JavaScript assets.

```razor
@inherits LayoutComponentBase
<div class="app-shell">
    <NavMenu />
    <main>@Body</main>
</div>
```

Layouts should contain common UI only. Data fetching and application behavior should remain in pages or injected services.
