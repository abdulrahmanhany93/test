using CleanProject.Domain.DTO;
using CleanProject.Domain.Entities;
using CleanProject.Shared.Model;

namespace CleanProject.Data.Interfaces;

public interface IAtmRepository : IGenericRepository<Atm>
{
    Task<Result<Atm>> Withdraw(AtmWithdrawDto dto);
    Task<Result<Atm>> Deposit(AtmDepositDto dto);
}