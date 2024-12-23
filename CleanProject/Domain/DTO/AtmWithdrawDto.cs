namespace CleanProject.Domain.DTO;

public sealed class AtmWithdrawDto
{
    public AtmWithdrawDto(Guid atmId, double amount)
    {
        Amount = amount;
        AtmId = atmId;
    }

    public double Amount { get; set; }
    public Guid AtmId { get; set; }
}