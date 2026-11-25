# Blazor Server vs WebAssembly

## Interactive Server

- Components execute on ASP.NET Core.
- The browser receives UI diffs through a real-time SignalR connection.
- State is held on the server in a circuit.
- Server restarts and lost connections can invalidate state.
- Suitable for secure business apps when server latency is acceptable.

## WebAssembly

- Components and .NET runtime execute in the browser.
- Static hosting can serve downloaded assets.
- API requests still require secure server endpoints.
- Browser storage and memory survive only according to browser behavior.
- First load includes runtime and application downloads.

## Decision Factors

- Security-sensitive work should remain server-side.
- Offline and client-heavy behavior favors WebAssembly.
- Low startup payload, direct server data access, and low device usage favor server rendering.
- Global circuit scale and network reliability favor a hybrid or hosting assessment.
