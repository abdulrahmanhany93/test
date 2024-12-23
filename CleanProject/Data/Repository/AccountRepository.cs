using CleanProject.Data.Interfaces;
using CleanProject.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace CleanProject.Data.Repository;

public class AccountRepository(AppDbContext appDbContext, ILogger<Account> logger)
    : GenericRepository<Account>(appDbContext, logger), IAccountRepository
{
    private const string TableName = "Accounts";
    private readonly AppDbContext _appDbContext = appDbContext;
    private readonly ILogger<Account> _logger = logger;

    public new async Task<Result<Account>> CreateAsync(Account entity)
    {
        try
        {
            var client = await _appDbContext.Clients.Include(c => c.Account)
                .FirstOrDefaultAsync(e => e.Id == entity.ClientId);
            if (client?.Account != null)
            {
                _logger.LogInformation("User with ID {id} already have account", client.Id);
                return Result<Account>.Failure($"User with ID {client.Id} already have account",
                    StatusCodes.Status400BadRequest);
            }


            await _appDbContext.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation("Created entity with ID {Id} in {tableName}", entity.Id, TableName);
            return Result<Account>.Success(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create entity {entity} in {tableName}", entity, TableName);
            return Result<Account>.Failure("An error occurred while creating the entity.", ex.HResult);
        }
    }

    public async Task<Result<Account>> GetAccountWithCards(Guid id)
    {
        var result = await _appDbContext.Accounts.Include(a => a.CreditCards).FirstOrDefaultAsync(a => a.Id == id);
        if (result == null)
        {
            _logger.LogWarning("Account with ID {id} not found", id);
            return Result<Account>.Failure($"No account found with the specified id : {id}",
                StatusCodes.Status404NotFound);
        }

        _logger.LogInformation("Retrieved entity with ID {Id} from {tableName}", id, TableName);

        return Result<Account>.Success(result);
    }

    public async Task<Result<Account>> Withdraw(Guid accountId, double amount)
    {
        _logger.LogInformation("Trying to Withdraw to account with id: {id}", accountId);
        var account = await _appDbContext.Accounts.FindAsync(accountId);
        if (account == null)
            return Result<Account>.Failure("Account with ID {id} not found", StatusCodes.Status404NotFound);
        if (!account.CanWithdraw(amount))
            return Result<Account>.Failure("Insufficient Balance for this account.", StatusCodes.Status400BadRequest);
        account.Withdraw(amount);
        await _appDbContext.SaveChangesAsync();
        _logger.LogInformation("withdraw successfully to account with id : {accountId}", accountId);
        return Result<Account>.Success(account);
    }

    public async Task<Result<Account>> Deposit(Guid accountId, double amount)
    {
        _logger.LogInformation("Trying to deposit to account with id: {id}", accountId);
        var account = await _appDbContext.Accounts.FindAsync(accountId);
        if (account == null)
            return Result<Account>.Failure("Account with ID {id} not found", StatusCodes.Status404NotFound);

        account.Deposit(amount);
        await _appDbContext.SaveChangesAsync();
        _logger.LogInformation("deposit successfully to account with id : {id}", account);
        return Result<Account>.Success(account);
    }
}