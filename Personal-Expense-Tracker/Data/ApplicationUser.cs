using Microsoft.AspNetCore.Identity;
using Personal_Expense_Tracker.Domain;

namespace Personal_Expense_Tracker.Data;

public sealed class ApplicationUser : IdentityUser
{
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
