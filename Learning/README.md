# Blazor Framework Learning Guide

This folder contains interview-ready notes for the complete 30-point Blazor curriculum.

## Learning Path

| #   | Topic                             | Notes                                                                                                                  |
| --- | --------------------------------- | ---------------------------------------------------------------------------------------------------------------------- |
| 1   | Introduction to Blazor            | [01-Blazor-Fundamentals.md](./01-Blazor-Fundamentals.md)                                                               |
| 2   | Blazor Architecture               | [01-Blazor-Fundamentals.md](./01-Blazor-Fundamentals.md), [13-JavaScript-Interop.md](./13-JavaScript-Interop.md)       |
| 3   | Blazor Server vs WebAssembly      | [01-Blazor-Fundamentals.md](./01-Blazor-Fundamentals.md), [27-Server-vs-WebAssembly.md](./27-Server-vs-WebAssembly.md) |
| 4   | Blazor Project Structure          | [26-Project-Structure.md](./26-Project-Structure.md)                                                                   |
| 5   | Razor Syntax                      | [02-Razor-Components.md](./02-Razor-Components.md)                                                                     |
| 6   | Blazor Components                 | [02-Razor-Components.md](./02-Razor-Components.md)                                                                     |
| 7   | Component Lifecycle Methods       | [16-Component-Lifecycle.md](./16-Component-Lifecycle.md)                                                               |
| 8   | Component Parameters              | [17-Component-Parameters.md](./17-Component-Parameters.md)                                                             |
| 9   | Data Binding                      | [03-Data-Binding-and-Events.md](./03-Data-Binding-and-Events.md)                                                       |
| 10  | Event Handling                    | [03-Data-Binding-and-Events.md](./03-Data-Binding-and-Events.md)                                                       |
| 11  | Forms and Input Controls          | [05-Forms-and-Validation.md](./05-Forms-and-Validation.md)                                                             |
| 12  | Form Validation                   | [05-Forms-and-Validation.md](./05-Forms-and-Validation.md)                                                             |
| 13  | Routing and Navigation            | [04-Routing-and-Navigation.md](./04-Routing-and-Navigation.md)                                                         |
| 14  | Layout Components                 | [14-Layouts-and-Shared-UI.md](./14-Layouts-and-Shared-UI.md)                                                           |
| 15  | Dependency Injection              | [06-Dependency-Injection.md](./06-Dependency-Injection.md)                                                             |
| 16  | Service Creation and Registration | [29-Service-Creation-and-Registration.md](./29-Service-Creation-and-Registration.md)                                   |
| 17  | State Management                  | [10-State-Management.md](./10-State-Management.md)                                                                     |
| 18  | HTTP Client                       | [07-HTTP-and-APIs.md](./07-HTTP-and-APIs.md)                                                                           |
| 19  | Web API Integration               | [07-HTTP-and-APIs.md](./07-HTTP-and-APIs.md)                                                                           |
| 20  | CRUD Operations                   | [18-CRUD-Operations.md](./18-CRUD-Operations.md)                                                                       |
| 21  | Entity Framework Core             | [08-EF-Core-and-Data.md](./08-EF-Core-and-Data.md)                                                                     |
| 22  | SQL Server Integration            | [19-SQL-Server-Integration.md](./19-SQL-Server-Integration.md)                                                         |
| 23  | Authentication and Authorization  | [09-Authentication-and-Security.md](./09-Authentication-and-Security.md)                                               |
| 24  | JWT Authentication                | [20-JWT-Authentication.md](./20-JWT-Authentication.md)                                                                 |
| 25  | Role-Based Authorization          | [21-Role-Based-Authorization.md](./21-Role-Based-Authorization.md)                                                     |
| 26  | JavaScript Interoperability       | [13-JavaScript-Interop.md](./13-JavaScript-Interop.md)                                                                 |
| 27  | Error Handling                    | [23-Error-Handling.md](./23-Error-Handling.md)                                                                         |
| 28  | Logging                           | [22-Logging.md](./22-Logging.md)                                                                                       |
| 29  | Unit Testing                      | [24-Unit-Testing.md](./24-Unit-Testing.md)                                                                             |
| 30  | Blazor Application Deployment     | [25-Deployment.md](./25-Deployment.md)                                                                                 |

## Core Rendering Rules

- Use `@page` to route a component.
- Components rerender after supported events and component parameter updates.
- Use `@rendermode` explicitly in an interactive Blazor application.
- Call `InvokeAsync(StateHasChanged)` when a background thread updates UI state.
- Keep server and database operations server-side; do not trust browser values.
- Components should display UI and delegate reusable work to services.

## Recommended Study Sequence

1. Fundamentals and project structure
2. Razor components, lifecycle, parameters, binding, and events
3. Routing, layouts, forms, and validation
4. Dependency injection and state
5. HTTP APIs and CRUD
6. EF Core and SQL Server
7. Authentication, JWT, and authorization
8. JavaScript, logging, testing, and deployment

## Repository Examples

| Project                                                   | Main concepts                                              |
| --------------------------------------------------------- | ---------------------------------------------------------- |
| [CounterApp](../CounterApp)                               | Components, binding, events, lifecycle, responsive UI      |
| [ToDoApp](../ToDoApp)                                     | Collections, models, callback parameters, state            |
| [Student-Management-System](../Student-Management-System) | EditForm, validation, services, CRUD UI                    |
| [Weather-Dashboard](../Weather-Dashboard)                 | HttpClient, async operations, loading and errors           |
| [Personal-Expense-Tracker](../Personal-Expense-Tracker)   | EF Core, SQL Server, repositories, Identity, authorization |
