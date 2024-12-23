using CleanProject.Data.Interfaces;
using CleanProject.Domain.Entities;
using CleanProject.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace CleanProject.Data.Repository;

public class ClientRepository(
    AppDbContext appDbContext,
    ILogger<Client> logger)
    : GenericRepository<Client>(appDbContext, logger), IClientRepository
{
    private const string TableName = "Clients";

    public async Task<Result<Client>> GetClientWithAccount(Guid id)
    {
        var result = appDbContext.Clients.Include(a => a.Account).FirstOrDefault(a => a.Id == id);
        if (result == null)
        {
            logger.LogWarning("Account with ID {id} not found", id);
            return Result<Client>.Failure($"No account found with the specified id : {id}",
                StatusCodes.Status404NotFound);
        }

        logger.LogInformation("Retrieved entity with ID {Id} from {tableName}", id, TableName);

        return Result<Client>.Success(result);
    }
}