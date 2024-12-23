using CleanProject.Data.Interfaces;
using CleanProject.Domain.DTO;
using CleanProject.Domain.Entities;
using CleanProject.Services.Interfaces;
using CleanProject.Shared.Model;
using Microsoft.Extensions.Logging;

namespace Application.Services.Implementations;

public class ClientServices(
    ILogger<ClientServices> logger,
    ICreditCardRepository creditCardRepository,
    IClientRepository clientRepository,
    IAtmRepository atmRepository,
    IAccountRepository accountRepository) : IClientServices

{
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly IAtmRepository _atmRepository = atmRepository;
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly ICreditCardRepository _creditCardRepository = creditCardRepository;
    private readonly ILogger<ClientServices> _logger = logger;

    public async Task<Result<Client>> GetClientByIdAsync(Guid clientId)
    {
        return await _clientRepository.GetByIdAsync(clientId);
    }

    public async Task<Result<Client>> CreateClientAsync(ClientDto client)
    {
        var newClient = new Client
        {
            Username = client.Username,
            Email = client.Email
        };

        return await _clientRepository.CreateAsync(newClient);
    }

    public async Task<Result<Client>> DeleteClientAsync(Guid clientId)
    {
        return await _clientRepository.DeleteAsync(clientId);
    }

    public async Task<Result<Account>> Withdraw(ClientWithdrawDto clientWithdrawDto)
    {
        try
        {
            var creditCardResult = await _creditCardRepository.GetByIdIncludeAccount(clientWithdrawDto.CardId);

            var accountResult =
                await _accountRepository.Withdraw(creditCardResult.Value.AccountId, clientWithdrawDto.Amount);
            if (accountResult.IsSuccess)
            {
                await _atmRepository.Withdraw(new AtmWithdrawDto(clientWithdrawDto.AtmId, clientWithdrawDto.Amount));
                return Result<Account>.Success(accountResult.Value);
            }

            return accountResult;
        }
        catch (Exception ex)
        {
            return Result<Account>.Failure(ex.Message, ex.HResult);
        }
    }


    public async Task<Result<IReadOnlyList<Client>>> AllClientAsync()
    {
        return await _clientRepository.GetAllAsync();
    }

    public async Task<Result<Client>> UpdateClientAsync(Client client)
    {
        return await _clientRepository.UpdateAsync(client);
    }

    public async Task<Result<Account>> Deposit(ClientDepositDto clientDepositDto)
    {
        // Step 1: Validate ATM
        var atmResult = await _atmRepository.GetByIdAsync(clientDepositDto.AtmId);
        if (!atmResult.IsSuccess) return Result<Account>.Failure(atmResult.Error, atmResult.StatusCode);

        // Step 2: Validate Credit Card and Account
        var creditCardResult = await _creditCardRepository.GetByIdIncludeAccount(clientDepositDto.CardId);
        if (!creditCardResult.IsSuccess)
            return Result<Account>.Failure(creditCardResult.Error, creditCardResult.StatusCode);

        // Step 3: Deposit to Account
        var accountResult = await _accountRepository.Deposit(creditCardResult.Value.AccountId, clientDepositDto.Amount);
        if (!accountResult.IsSuccess) return Result<Account>.Failure(accountResult.Error, accountResult.StatusCode);

        // Step 4: Update ATM
        var atmDepositResult =
            await _atmRepository.Deposit(new AtmDepositDto(clientDepositDto.AtmId, clientDepositDto.Amount));
        return !atmDepositResult.IsSuccess
            ? Result<Account>.Failure(atmDepositResult.Error, atmDepositResult.StatusCode)
            :
            // Return success with the updated account
            Result<Account>.Success(accountResult.Value);
    }
}