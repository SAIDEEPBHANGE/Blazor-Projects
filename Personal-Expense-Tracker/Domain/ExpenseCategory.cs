using System.ComponentModel.DataAnnotations;

namespace Personal_Expense_Tracker.Domain;

public sealed class ExpenseCategory
{
    public int Id { get; set; }

    [Required]
    [MaxLength(80)]
    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
