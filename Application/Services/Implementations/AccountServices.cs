using CleanProject.Data.Interfaces;
using CleanProject.Domain.DTO;
using CleanProject.Domain.Entities;
using CleanProject.Services.Interfaces;
using CleanProject.Shared.Model;

namespace Application.Services.Implementations;

public class AccountService(
    ICreditCardRepository creditCardRepository,
    IClientRepository clientRepository,
    IAccountRepository accountRepository) : IAccountService

{
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly IClientRepository _clientRepository = clientRepository;

    private readonly ICreditCardRepository _creditCardRepository = creditCardRepository;


    public async Task<Result<Account>> GetAccountByIdAsync(Guid accountId)
    {
        return await _accountRepository.GetAccountWithCards(accountId);
    }

    public async Task<Result<Account>> CreateAccountAsync(CreateAccountDto account)
    {
        var clientResult = await _clientRepository.GetByIdAsync(account.ClientId);
        if (!clientResult.IsSuccess) return Result<Account>.Failure(clientResult.Error, clientResult.StatusCode);
        var newAccount = new Account
        {
            Balance = account.Balance,
            ClientId = account.ClientId
        };
        var accountResult = await _accountRepository.CreateAsync(newAccount);
        if (!accountResult.IsSuccess) return accountResult;
        clientResult.Value.AccountId = accountResult.Value.Id;
        var updatedAccount = await _clientRepository.UpdateAsync(clientResult.Value!);
        var card = await _creditCardRepository.CreateAsync(CreditCard.Generate(accountResult.Value!.Id));
        newAccount.CreditCards.Add(card.Value!);

        return accountResult;
    }

    public async Task<Result<Account>> DeleteAccountAsync(Guid accountId)
    {
        return await _accountRepository.DeleteAsync(accountId);
    }


    public async Task<Result<IReadOnlyList<Account>>> AllAccountAsync()
    {
        return await _accountRepository.GetAllAsync();
    }

    public async Task<Result<Account>> UpdateAccountAsync(UpdateAccountDto account)
    {
        var result = await _accountRepository.GetByIdAsync(account.Id);
        if (!result.IsSuccess) return result;
        result.Value!.Balance = account.Balance;
        return await _accountRepository.UpdateAsync(result.Value!);
    }


    public async Task<Result<Account>> DepositToAccount(Guid accountId, double amount)
    {
        return await _accountRepository.Withdraw(accountId, amount);
    }

    public async Task<Result<Account>> WithdrawFromAccount(Guid accountId, double amount)
    {
        return await _accountRepository.Withdraw(accountId, amount);
    }
}