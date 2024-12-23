using CleanProject.Domain.Entities;
using CleanProject.Shared.Model;

namespace CleanProject.Data.Interfaces;

public interface ICreditCardRepository : IGenericRepository<CreditCard>
{
    Task<Result<CreditCard>> GetByIdIncludeAccount(Guid cardId);
}