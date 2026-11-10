using Personal_Expense_Tracker.Domain;

namespace Personal_Expense_Tracker.Repositories;

public interface IExpenseCategoryRepository
{
    Task<List<ExpenseCategory>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<ExpenseCategory?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ExpenseCategory?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task AddAsync(ExpenseCategory category, CancellationToken cancellationToken = default);
    Task UpdateAsync(ExpenseCategory category, CancellationToken cancellationToken = default);
}
