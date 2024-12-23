using CleanProject.Data.Interfaces;
using CleanProject.Domain.DTO;
using CleanProject.Domain.Entities;
using CleanProject.Shared.Model;

namespace CleanProject.Data.Repository;

public class AtmRepository(AppDbContext appDbContext, ILogger<Atm> logger)
    : GenericRepository<Atm>(appDbContext, logger), IAtmRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;
    private readonly ILogger<Atm> _logger = logger;

    public async Task<Result<Atm>> Withdraw(AtmWithdrawDto dto)
    {
        _logger.LogInformation("Trying to withdraw to atm with id: {id}", dto.AtmId);
        var atm = await _appDbContext.Atms.FindAsync(dto.AtmId);
        if (atm == null)
        {
            _logger.LogInformation("Atm with id : {id} does not exist", dto.AtmId);
            return Result<Atm>.Failure($"Atm with id : {dto.AtmId} does not exist", StatusCodes.Status404NotFound);
        }

        if (!atm.CanWithdraw(dto.Amount))
        {
            _logger.LogWarning("Insufficient Balance in atm with id {id}", dto.AtmId);
            return Result<Atm>.Failure("Insufficient Balance", StatusCodes.Status400BadRequest);
        }

        atm.Withdraw(dto.Amount);

        await _appDbContext.SaveChangesAsync();
        _logger.LogInformation(" withdrew from atm with id: {id}", dto.AtmId);
        return Result<Atm>.Success(atm);
    }

    public async Task<Result<Atm>> Deposit(AtmDepositDto dto)
    {
        _logger.LogInformation("Trying to deposit to atm with id: {id}", dto.AtmId);
        var atm = await _appDbContext.Atms.FindAsync(dto.AtmId);
        if (atm == null)
        {
            _logger.LogWarning("Atm with id : {id} does not exist", dto.AtmId);
            return Result<Atm>.Failure($"Atm with id : {dto.AtmId} does not exist", StatusCodes.Status404NotFound);
        }

        atm.Deposit(dto.Amount);

        await _appDbContext.SaveChangesAsync();
        _logger.LogInformation("Deposited to atm with id: {id} and amount {amount}", dto.AtmId, dto.Amount);
        return Result<Atm>.Success(atm);
    }

    //  public async Task<Result<AccountDto>> Withdraw(AtmWithdrawDto dto)
    // {
    //     var creditCardResult = await _creditCardRepository.GetByIdIncludeAccount(dto.CardId);
    //
    //     if (creditCardResult is not { IsSuccess: true, Value.Account: not null })
    //         return Result<AccountDto>.Failure(creditCardResult.Error, creditCardResult.StatusCode);
    //     var atmResult = await _atmRepository.GetByIdAsync(dto.AtmId);
    //     if (!atmResult.IsSuccess || !atmResult.Value.CanWithdraw(dto.Amount))
    //         return Result<AccountDto>.Failure("Sorry We cannot withdrow", creditCardResult.StatusCode);
    //         var clientResult = await _clientRepository.GetClientWithAccount(creditCardResult.Value.Account.ClientId);
    //     if (clientResult is not { IsSuccess: true, Value.Account: not null })
    //         return Result<AccountDto>.Failure(creditCardResult.Error, creditCardResult.StatusCode);
    //     creditCardResult.Value?.Account.Withdraw(dto.Amount);
    //     await _accountRepository.UpdateAsync(creditCardResult.Value?.Account!);
    //     _logger.LogInformation("Account With Id: {id} has withdraw {amount} successfully",
    //         creditCardResult.Value?.AccountId,
    //         dto.Amount);
    //     var accountDto = new AccountDto
    //     (
    //         creditCardResult.Value!.AccountId,
    //         clientResult.Value.Username,
    //         clientResult.Value?.Email,
    //         creditCardResult.Value!.Account!.Balance
    //     );
    //     return Result<AccountDto>.Success(accountDto);
    // }
    //
    // public async Task<Result<AccountDto>> Deposit(AtmDepositDto dto)
    // {
    //     var creditCardResult = await _creditCardRepository.GetByIdIncludeAccount(dto.CardId);
    //
    //     if (creditCardResult is not { IsSuccess: true, Value.Account: not null })
    //         return Result<AccountDto>.Failure(creditCardResult.Error, creditCardResult.StatusCode);
    //     var clientResult = await _clientRepository.GetClientWithAccount(creditCardResult.Value.Account.ClientId);
    //     if (clientResult is not { IsSuccess: true, Value.Account: not null })
    //         return Result<AccountDto>.Failure(creditCardResult.Error, creditCardResult.StatusCode);
    //     creditCardResult.Value?.Account.Deposit(dto.Amount);
    //     await _accountRepository.UpdateAsync(creditCardResult.Value?.Account!);
    //     _logger.LogInformation("Account With Id: {id} has deposit {amount} successfully",
    //         creditCardResult.Value?.AccountId,
    //         dto.Amount);
    //     var accountDto = new AccountDto
    //     (
    //         creditCardResult.Value!.AccountId,
    //         clientResult.Value.Username,
    //         clientResult.Value?.Email,
    //         creditCardResult.Value!.Account!.Balance
    //     );
    //     return Result<AccountDto>.Success(accountDto);
    // }
}