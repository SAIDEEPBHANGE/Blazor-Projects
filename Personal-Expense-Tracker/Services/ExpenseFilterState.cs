namespace Personal_Expense_Tracker.Services;

public sealed class ExpenseFilterState
{
    public string? SearchTerm { get; set; }
    public int? Month { get; set; }
    public int? Year { get; set; }
    public string? Category { get; set; }
}
