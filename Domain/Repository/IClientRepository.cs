using CleanProject.Domain.Entities;
using CleanProject.Shared.Model;

namespace CleanProject.Data.Interfaces;

public interface IClientRepository : IGenericRepository<Client>
{
    public Task<Result<Client>> GetClientWithAccount(Guid id);
}