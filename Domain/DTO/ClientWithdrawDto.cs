namespace CleanProject.Domain.DTO;

public sealed record ClientWithdrawDto
{
    public ClientWithdrawDto(double amount, Guid atmId, Guid cardId)

    {
        Amount = amount;
        AtmId = atmId;
        CardId = cardId;
    }

    public double Amount { get; set; }
    public Guid AtmId { get; set; }
    public Guid CardId { get; set; }
}