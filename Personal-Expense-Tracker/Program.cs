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

builder.Services.AddDbContext<ApplicationDbContext>(options =>
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
    await EnsureUserAsync(userManager, "user@expense.local", "User@123", "User");

    var categoryIds = await EnsureCategoriesAsync(dbContext);
    await EnsureAdminSampleExpensesAsync(dbContext, adminUser.Id, categoryIds);
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

static async Task EnsureAdminSampleExpensesAsync(ApplicationDbContext dbContext, string adminUserId, Dictionary<string, int> categoryIds)
{
    var hasData = await dbContext.Expenses.AnyAsync(x => x.UserId == adminUserId);
    if (hasData)
    {
        return;
    }

    var today = DateTime.Today;

    dbContext.Expenses.AddRange(
        new Expense
        {
            UserId = adminUserId,
            Title = "Monthly Salary",
            Amount = 75000,
            ExpenseDate = today.AddDays(-20),
            CategoryId = categoryIds["Salary"],
            Notes = "Credited salary"
        },
        new Expense
        {
            UserId = adminUserId,
            Title = "Groceries",
            Amount = 4200,
            ExpenseDate = today.AddDays(-5),
            CategoryId = categoryIds["Food"],
            Notes = "Weekly household groceries"
        },
        new Expense
        {
            UserId = adminUserId,
            Title = "Electricity Bill",
            Amount = 1800,
            ExpenseDate = today.AddDays(-3),
            CategoryId = categoryIds["Bills"],
            Notes = "Monthly utility payment"
        });

    await dbContext.SaveChangesAsync();
}
