using CleanProject.Domain.Entities;
using CleanProject.Shared.Model;

namespace CleanProject.Services.Interfaces;

public interface IAtmServices
{
    Task<Result<Atm>> CreateAtm(double amount);
    Task<Result<Atm>> GetAtm();
    Task<Result<Atm>> UpdateAtm(Guid id, double amount);
    Task<Result<Atm>> WithdrawAtm(Guid id, double amount);
    Task<Result<Atm>> DepositAtm(Guid id, double amount);
}