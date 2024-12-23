using System.ComponentModel;

namespace CleanProject.Domain.Entities;

public sealed class Atm : BaseEntity
{
    [DefaultValue(0.0)] public double Amount { get; set; }


    public bool HasBalance => Amount > 0;

    public List<AtmTransaction> Transactions { get; set; } = [];

    public bool CanWithdraw(double withdrawAmount)
    {
        return withdrawAmount <= Amount;
    }

    public void Withdraw(double withdrawAmount)
    {
        Amount -= withdrawAmount;
    }

    public void Deposit(double depositAmount)
    {
        Amount += depositAmount;
    }
}