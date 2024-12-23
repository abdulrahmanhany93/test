using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CleanProject.Domain.Enum;

namespace CleanProject.Domain.Entities;

public sealed class AtmTransaction : BaseEntity
{
    [JsonIgnore] public Atm? Atm { get; init; }
    public TransactionType TransactionType { get; init; }
    [ForeignKey("Atm")] public Guid AtmId { get; init; }

    public double Amount { get; init; }
    public DateTime Date { get; init; }
    [JsonIgnore] public CreditCard? Card { get; init; }

    [ForeignKey("Card")] public Guid CardId { get; init; }
}