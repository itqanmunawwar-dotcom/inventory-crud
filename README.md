# Inventory CRUD

A small ASP.NET Core MVC application for managing products. It demonstrates the full CRUD flow with Entity Framework Core and SQLite, plus server-side validation and a simple search.

## Requirements

- .NET 8 SDK or Visual Studio 2022 with the **ASP.NET and web development** workload
- ASP.NET Core Runtime 8 (installed by the workload or the .NET 8 SDK)

No database server is required. The application creates `inventory.db` on first run and adds three sample products so the app is immediately usable.

The connection string in `appsettings.json` is only a relative file path:

```json
"DefaultConnection": "Data Source=inventory.db"
```

The database file itself is intentionally excluded from Git. On a fresh clone, Entity Framework creates the file automatically in the application directory, then the seed routine adds the sample products. This keeps the repository small and prevents local test data from being shared.

## Run locally

### Visual Studio

1. Install Visual Studio 2022 with the **ASP.NET and web development** workload. This installs the ASP.NET Core runtime required to run the web project.
2. Open `InventoryCrud.sln` (the solution file), not just the containing folder.
3. In the toolbar, select the `Inventory.Web` launch profile and press `Ctrl+F5` or `F5`.
4. If Visual Studio shows stale web-server metadata, close it and delete the generated `.vs` folder beside the solution, then reopen `InventoryCrud.sln`.

The project intentionally uses one project-based HTTP profile named `Inventory.Web`. IIS Express is not required.

If the debugger reports that the target process exited before the CoreCLR started, open a Developer PowerShell and run `dotnet --list-runtimes`. You should see `Microsoft.AspNetCore.App 8.0.x`. Seeing only `Microsoft.NETCore.App 8.0.x` is not sufficient for an ASP.NET Core application.

### Command line

```bash
git clone <repository-url>
cd inventory-crud
dotnet restore
dotnet run
```

Open the local URL printed by the command, usually `http://localhost:5175`. The first request creates the SQLite database. To reset the sample data, stop the app and delete `inventory.db`, then run it again.

## What is included

- List products and search by name or description
- View product details
- Create, edit, and delete products
- Data annotations for required fields, string lengths, price, and stock validation
- SQLite persistence through Entity Framework Core
- Seed data for a predictable first-run experience
- Anti-forgery protection on write operations

## Project structure

- `Models/Product.cs` — product data and validation rules
- `Data/ApplicationDbContext.cs` — EF Core database context
- `Data/SeedData.cs` — safe first-run sample data
- `Controllers/ProductsController.cs` — CRUD actions
- `Views/Products/` — MVC pages and shared form partial

## Manual acceptance check

1. Start the app and confirm the three seeded products appear.
2. Search for `dock` and confirm only the USB-C Dock appears.
3. Add a product with a blank name or negative price and confirm validation messages appear.
4. Add a valid product, edit its stock quantity, open its details, and delete it.
5. Stop and restart the app; confirm created data remains.

## Design notes

The application intentionally stays small: one aggregate, one controller, and one SQLite database. That keeps the code easy to review while still showing the important production habits for a basic CRUD feature: async database calls, `AsNoTracking` for reads, input validation, anti-forgery tokens, and a repeatable local setup.
