using Microsoft.JSInterop;

namespace Personal_Expense_Tracker.Services;

public sealed class LocalStorageExpenseFilterStateService(IJSRuntime jsRuntime) : IExpenseFilterStateService
{
    private const string StorageKey = "expenseTracker.filters";

    public async Task<ExpenseFilterState> LoadAsync()
    {
        try
        {
            var state = await jsRuntime.InvokeAsync<ExpenseFilterState?>("expenseTrackerState.get", StorageKey);
            return state ?? new ExpenseFilterState();
        }
        catch
        {
            return new ExpenseFilterState();
        }
    }

    public async Task SaveAsync(ExpenseFilterState state)
    {
        await jsRuntime.InvokeVoidAsync("expenseTrackerState.set", StorageKey, state);
    }
}
