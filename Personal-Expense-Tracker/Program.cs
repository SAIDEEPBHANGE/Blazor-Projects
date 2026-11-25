using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Personal_Expense_Tracker.Components;
using Personal_Expense_Tracker.Data;
using Personal_Expense_Tracker.Domain;
using Personal_Expense_Tracker.Repositories;
using Personal_Expense_Tracker.Services;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddMudServices();
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
    contextLifetime: ServiceLifetime.Scoped,
    optionsLifetime: ServiceLifetime.Singleton);
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies(options =>
    {
        options.ApplicationCookie.Configure(cookie =>
        {
            cookie.LoginPath = "/login";
            cookie.AccessDeniedPath = "/login";
        });
    });
builder.Services.AddAuthorization();
builder.Services
    .AddIdentityCore<ApplicationUser>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>();
builder.Services.AddScoped<IExpenseFilterStateService, LocalStorageExpenseFilterStateService>();
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, NoOpEmailSender>();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
    var canConnect = await dbContext.Database.CanConnectAsync();
    if (!canConnect)
    {
        throw new InvalidOperationException("Database connection failed. Verify SQL Server instance and connection string.");
    }
    await SeedDefaultsAsync(scope.ServiceProvider);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.MapGroup("/auth").MapIdentityApi<ApplicationUser>();
app.MapGet("/logout", async (HttpContext httpContext) =>
{
    await httpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
    return Results.Redirect("/login");
});
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.Run();
static async Task SeedDefaultsAsync(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
    await EnsureRoleAsync(roleManager, "Admin");
    await EnsureRoleAsync(roleManager, "User");
    var adminUser = await EnsureUserAsync(userManager, "admin@expense.local", "Admin@123", "Admin");
    var standardUser = await EnsureUserAsync(userManager, "user@expense.local", "User@123", "User");
    var categoryIds = await EnsureCategoriesAsync(dbContext);
    await EnsureSampleExpensesAsync(dbContext, adminUser.Id, categoryIds, isAdmin: true);
    await EnsureSampleExpensesAsync(dbContext, standardUser.Id, categoryIds, isAdmin: false);
}
static async Task EnsureRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
{
    if (await roleManager.RoleExistsAsync(roleName))
    {
        return;
    }
    await roleManager.CreateAsync(new IdentityRole(roleName));
}
static async Task<ApplicationUser> EnsureUserAsync(UserManager<ApplicationUser> userManager, string email, string password, string roleName)
{
    var user = await userManager.FindByEmailAsync(email);
    if (user is null)
    {
        user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };
        var createResult = await userManager.CreateAsync(user, password);
        if (!createResult.Succeeded)
        {
            throw new InvalidOperationException($"Unable to create default user {email}: {string.Join(", ", createResult.Errors.Select(x => x.Description))}");
        }
    }
    if (!await userManager.IsInRoleAsync(user, roleName))
    {
        await userManager.AddToRoleAsync(user, roleName);
    }
    return user;
}
static async Task<Dictionary<string, int>> EnsureCategoriesAsync(ApplicationDbContext dbContext)
{
    var requiredNames = new[] { "Food", "Travel", "Shopping", "Bills", "Salary" };
    var existing = await dbContext.ExpenseCategories.ToListAsync();
    foreach (var name in requiredNames)
    {
        var category = existing.FirstOrDefault(x => x.Name == name);
        if (category is null)
        {
            dbContext.ExpenseCategories.Add(new ExpenseCategory
            {
                Name = name,
                IsActive = true
            });
        }
        else if (!category.IsActive)
        {
            category.IsActive = true;
        }
    }
    await dbContext.SaveChangesAsync();
    return await dbContext.ExpenseCategories
        .AsNoTracking()
        .ToDictionaryAsync(x => x.Name, x => x.Id);
}
static async Task EnsureSampleExpensesAsync(ApplicationDbContext dbContext, string userId, Dictionary<string, int> categoryIds, bool isAdmin)
{
    var now = DateTime.Today;
    var hasCurrentMonthData = await dbContext.Expenses.AnyAsync(x =>
        x.UserId == userId &&
        x.ExpenseDate.Month == now.Month &&
        x.ExpenseDate.Year == now.Year);
    if (hasCurrentMonthData)
    {
        return;
    }
    if (isAdmin)
    {
        dbContext.Expenses.AddRange(
            new Expense
            {
                UserId = userId,
                Title = "Monthly Salary",
                Amount = 75000,
                ExpenseDate = now.AddDays(-10),
                CategoryId = categoryIds["Salary"],
                Notes = "Credited salary"
            },
            new Expense
            {
                UserId = userId,
                Title = "Groceries",
                Amount = 4200,
                ExpenseDate = now.AddDays(-5),
                CategoryId = categoryIds["Food"],
                Notes = "Weekly household groceries"
            },
            new Expense
            {
                UserId = userId,
                Title = "Electricity Bill",
                Amount = 1800,
                ExpenseDate = now.AddDays(-3),
                CategoryId = categoryIds["Bills"],
                Notes = "Monthly utility payment"
            });
    }
    else
    {
        dbContext.Expenses.AddRange(
            new Expense
            {
                UserId = userId,
                Title = "Monthly Salary",
                Amount = 42000,
                ExpenseDate = now.AddDays(-8),
                CategoryId = categoryIds["Salary"],
                Notes = "Credited salary"
            },
            new Expense
            {
                UserId = userId,
                Title = "Office Commute",
                Amount = 2600,
                ExpenseDate = now.AddDays(-4),
                CategoryId = categoryIds["Travel"],
                Notes = "Metro pass and cabs"
            },
            new Expense
            {
                UserId = userId,
                Title = "Internet Bill",
                Amount = 1100,
                ExpenseDate = now.AddDays(-2),
                CategoryId = categoryIds["Bills"],
                Notes = "Monthly broadband"
            });
    }
    await dbContext.SaveChangesAsync();
}
