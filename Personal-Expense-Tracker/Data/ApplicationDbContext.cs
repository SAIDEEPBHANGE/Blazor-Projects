using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Personal_Expense_Tracker.Domain;

namespace Personal_Expense_Tracker.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Expense> Expenses => Set<Expense>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Expense>()
            .HasIndex(x => new { x.UserId, x.ExpenseDate });

        builder.Entity<Expense>()
            .Property(x => x.Amount)
            .HasPrecision(18, 2);
    }
}
