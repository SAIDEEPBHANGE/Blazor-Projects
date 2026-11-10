using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Personal_Expense_Tracker.Domain;

namespace Personal_Expense_Tracker.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<ExpenseCategory> ExpenseCategories => Set<ExpenseCategory>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Expense>()
            .HasIndex(x => new { x.UserId, x.ExpenseDate });

        builder.Entity<Expense>()
            .Property(x => x.Amount)
            .HasPrecision(18, 2);

        builder.Entity<Expense>()
            .HasOne(x => x.User)
            .WithMany(x => x.Expenses)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Expense>()
            .HasOne(x => x.Category)
            .WithMany(x => x.Expenses)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ExpenseCategory>()
            .HasIndex(x => x.Name)
            .IsUnique();
    }
}
