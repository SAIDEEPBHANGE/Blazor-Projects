using Microsoft.EntityFrameworkCore;
using Personal_Expense_Tracker.Data;
using Personal_Expense_Tracker.Domain;

namespace Personal_Expense_Tracker.Repositories;

public sealed class ExpenseCategoryRepository(ApplicationDbContext dbContext) : IExpenseCategoryRepository
{
    public async Task<List<ExpenseCategory>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = dbContext.ExpenseCategories.AsNoTracking();

        if (!includeInactive)
        {
            query = query.Where(x => x.IsActive);
        }

        return await query
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<ExpenseCategory?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await dbContext.ExpenseCategories.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<ExpenseCategory?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await dbContext.ExpenseCategories
            .FirstOrDefaultAsync(x => x.Name.ToLower() == name.Trim().ToLower(), cancellationToken);
    }

    public async Task AddAsync(ExpenseCategory category, CancellationToken cancellationToken = default)
    {
        dbContext.ExpenseCategories.Add(category);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ExpenseCategory category, CancellationToken cancellationToken = default)
    {
        dbContext.ExpenseCategories.Update(category);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
