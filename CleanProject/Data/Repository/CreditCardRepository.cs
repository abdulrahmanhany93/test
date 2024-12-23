using CleanProject.Data.Interfaces;
using CleanProject.Domain.Entities;
using CleanProject.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace CleanProject.Data.Repository;

public class CreditCardRepository(AppDbContext appDbContext, ILogger<CreditCard> logger)
    : GenericRepository<CreditCard>(appDbContext, logger), ICreditCardRepository
{
    private const string TableName = "CreditCards";
    private readonly AppDbContext _appDbContext = appDbContext;
    private readonly ILogger<CreditCard> _logger = logger;

    public async Task<Result<CreditCard>> GetByIdIncludeAccount(Guid cardId)
    {
        var creditCard = await _appDbContext.CreditCards.Include(c => c.Account)
            .FirstOrDefaultAsync(e => e.Id == cardId);
        if (creditCard != null)
        {
            _logger.LogInformation("Retrieved entity with ID {Id} from {tableName}", cardId, TableName);
            return Result<CreditCard>.Success(creditCard);
        }

        _logger.LogWarning("Credit card with id {cardId} not found", cardId);
        return Result<CreditCard>.Failure($"Entity with ID {cardId} not found in {TableName}",
            StatusCodes.Status404NotFound);
    }
}