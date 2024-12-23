namespace CleanProject.Domain.DTO;

public class CreateAccountDto
{
    public Guid ClientId { get; set; }
    public double Balance { get; set; }
}