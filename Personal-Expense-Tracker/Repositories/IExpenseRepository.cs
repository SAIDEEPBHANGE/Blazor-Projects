using Personal_Expense_Tracker.Domain;

namespace Personal_Expense_Tracker.Repositories;

public interface IExpenseRepository
{
    Task<List<Expense>> GetByUserAsync(string userId, CancellationToken cancellationToken = default);
    Task<Expense?> GetByIdAsync(int id, string userId, CancellationToken cancellationToken = default);
    Task AddAsync(Expense expense, CancellationToken cancellationToken = default);
    Task UpdateAsync(Expense expense, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, string userId, CancellationToken cancellationToken = default);
    Task<bool> HasAnyForUserAsync(string userId, CancellationToken cancellationToken = default);
}
