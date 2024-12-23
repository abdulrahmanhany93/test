namespace CleanProject.Domain.DTO;

public sealed record ClientDepositDto
{
    public ClientDepositDto(double amount, Guid atmId, Guid cardId)

    {
        Amount = amount;
        AtmId = atmId;
        CardId = cardId;
    }

    public double Amount { get; set; }
    public Guid AtmId { get; set; }
    public Guid CardId { get; set; }
}