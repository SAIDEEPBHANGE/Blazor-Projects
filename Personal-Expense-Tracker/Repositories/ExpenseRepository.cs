using Microsoft.EntityFrameworkCore;
using Personal_Expense_Tracker.Data;
using Personal_Expense_Tracker.Domain;

namespace Personal_Expense_Tracker.Repositories;

public sealed class ExpenseRepository(ApplicationDbContext dbContext) : IExpenseRepository
{
    public async Task<List<Expense>> GetByUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Expenses
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.ExpenseDate)
            .ThenByDescending(x => x.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<Expense?> GetByIdAsync(int id, string userId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Expenses
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId, cancellationToken);
    }

    public async Task AddAsync(Expense expense, CancellationToken cancellationToken = default)
    {
        dbContext.Expenses.Add(expense);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Expense expense, CancellationToken cancellationToken = default)
    {
        dbContext.Expenses.Update(expense);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, string userId, CancellationToken cancellationToken = default)
    {
        var expense = await GetByIdAsync(id, userId, cancellationToken);

        if (expense is null)
        {
            return;
        }

        dbContext.Expenses.Remove(expense);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
