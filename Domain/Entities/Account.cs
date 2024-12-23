using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CleanProject.Domain.Entities;

public sealed class Account : BaseEntity
{
    [JsonIgnore] public Client Client { get; init; }

    [ForeignKey("Client")] public Guid ClientId { get; set; }

    [DefaultValue(0.0)] public double Balance { get; set; }

    public List<AccountTransaction> Transactions { get; init; } = new();

    public List<CreditCard> CreditCards { get; set; } = new(); // Properly initialize the list

    public bool HasBalance => Balance > 0;

    public void Deposit(double amount)
    {
        Balance += amount;
    }

    public void Withdraw(double amount)
    {
        Balance -= amount;
    }

    public bool CanWithdraw(double amount)
    {
        return Balance >= amount;
    }
}