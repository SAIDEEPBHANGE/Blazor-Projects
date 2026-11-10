namespace Personal_Expense_Tracker.Services;

public interface IExpenseFilterStateService
{
    Task<ExpenseFilterState> LoadAsync();
    Task SaveAsync(ExpenseFilterState state);
}
