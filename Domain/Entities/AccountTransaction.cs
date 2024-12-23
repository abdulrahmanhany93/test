using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CleanProject.Domain.Enum;

namespace CleanProject.Domain.Entities;

public sealed class AccountTransaction : BaseEntity
{
    [JsonIgnore] public Account? Account { get; init; }
    public TransactionType TransactionType { get; init; }
    [ForeignKey("Account")] public Guid AccountId { get; init; }

    public DateTime Date { get; init; }
    public double Amount { get; init; }
}