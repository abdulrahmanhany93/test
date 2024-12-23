using CleanProject.Domain.DTO;
using CleanProject.Domain.Entities;
using CleanProject.Shared.Model;

namespace CleanProject.Services.Interfaces;

public interface IClientServices
{
    Task<Result<IReadOnlyList<Client>>> AllClientAsync();
    Task<Result<Client>> GetClientByIdAsync(Guid clientId);
    Task<Result<Client>> CreateClientAsync(ClientDto client);
    Task<Result<Client>> UpdateClientAsync(Client client);

    Task<Result<Client>> DeleteClientAsync(Guid clientId);
    Task<Result<Account>> Withdraw(ClientWithdrawDto clientWithdrawDto);
    Task<Result<Account>> Deposit(ClientDepositDto clientDepositDto);
}