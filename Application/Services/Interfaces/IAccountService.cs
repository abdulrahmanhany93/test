using CleanProject.Domain.DTO;
using CleanProject.Shared.Model;

namespace CleanProject.Services.Interfaces;

public interface IAccountService
{
    Task<Result<IReadOnlyList<Account>>> AllAccountAsync();
    Task<Result<Account>> GetAccountByIdAsync(Guid accountId);
    Task<Result<Account>> CreateAccountAsync(CreateAccountDto dto);
    Task<Result<Account>> UpdateAccountAsync(UpdateAccountDto account);

    Task<Result<Account>> WithdrawFromAccount(Guid accountId, double amount);
    Task<Result<Account>> DepositToAccount(Guid accountId, double amount);
    Task<Result<Account>> DeleteAccountAsync(Guid accountId);
}