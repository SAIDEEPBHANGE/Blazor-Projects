using System.ComponentModel.DataAnnotations;

namespace Personal_Expense_Tracker.Domain;

public sealed class Expense
{
    public int Id { get; set; }

    [Required]
    [MaxLength(450)]
    public string UserId { get; set; } = string.Empty;

    [Required]
    [MaxLength(120)]
    public string Title { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "999999999")]
    public decimal Amount { get; set; }

    public DateTime ExpenseDate { get; set; } = DateTime.UtcNow.Date;

    [Required]
    public ExpenseCategory Category { get; set; }

    [MaxLength(500)]
    public string? Notes { get; set; }
}
