using System.ComponentModel.DataAnnotations;

namespace CleanProject.Domain.Enum;

public enum TransactionType
{
    [Display(Name = "Withdrawal")] Withdrawal,

    [Display(Name = "Deposit")] Deposit
}