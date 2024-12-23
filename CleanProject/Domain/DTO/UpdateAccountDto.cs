namespace CleanProject.Domain.DTO;

public sealed record UpdateAccountDto
{
    public Guid Id { get; set; }

    public double Balance { get; set; }
}