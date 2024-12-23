namespace CleanProject.Domain.DTO;

public sealed record AtmDepositDto
{
    public AtmDepositDto(Guid atmId, double amount)

    {
        Amount = amount;
        AtmId = atmId;
    }

    public double Amount { get; set; }
    public Guid AtmId { get; set; }
}