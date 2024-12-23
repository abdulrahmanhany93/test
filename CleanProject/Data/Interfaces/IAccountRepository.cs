using CleanProject.Shared.Model;

namespace CleanProject.Data.Interfaces;

public interface IAccountRepository : IGenericRepository<Account>
{
    Task<Result<Account>> GetAccountWithCards(Guid id);
    Task<Result<Account>> Withdraw(Guid accountId, double amount);
    Task<Result<Account>> Deposit(Guid accountId, double amount);
}