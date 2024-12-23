using CleanProject.Domain.Entities;

namespace CleanProject.Domain.DTO;

public sealed class AtmTransactionDto : BaseEntity
{
    public AtmTransactionDto(Guid id, double amount, DateTime date, string cardNumber, string cardHolderName)
    {
        Id = id;
        Amount = amount;
        Date = date;
        CardNumber = cardNumber;
        CardHolderName = cardHolderName;
    }


    public double Amount { get; init; }
    public DateTime Date { get; init; }
    public string CardNumber { get; init; }
    public string CardHolderName { get; init; }
}