using CleanProject.Domain.Entities;

namespace CleanProject.Domain.DTO;

public sealed class AccountDto : BaseEntity
{
    public AccountDto(Guid id, string userName, string userEmail, double balance)
    {
        Id = id;
        UserName = userName;
        UserEmail = userEmail;
        Balance = balance;
    }


    public string UserName { get; init; }
    public string UserEmail { get; init; }
    public double Balance { get; set; }
    public List<AccountTransaction> Transactions { get; set; } = [];
}