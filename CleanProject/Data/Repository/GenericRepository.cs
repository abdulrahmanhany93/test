using CleanProject.Data.Interfaces;
using CleanProject.Domain.Entities;
using CleanProject.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace CleanProject.Data.Repository;

public class GenericRepository<T>(AppDbContext appDbContext, ILogger<T> logger)
    : IGenericRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _appDbContext = appDbContext;

    private readonly string? _entityName = _GetEntityName(appDbContext);
    private readonly ILogger<T> _logger = logger;
    private readonly string? _tableName = _GetTableName(appDbContext);


    public async Task<Result<IReadOnlyList<T>>> GetAllAsync()
    {
        try
        {
            var data = await _appDbContext.Set<T>().AsNoTracking().ToListAsync();
            _logger.LogInformation("Retrieved {dataLength} records from {tableName}", data.Count, _tableName);
            return Result<IReadOnlyList<T>>.Success(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving records from {tableName}", _tableName);
            return Result<IReadOnlyList<T>>.Failure("An unexpected error occurred while retrieving records.",
                ex.HResult);
        }
    }

    public async Task<Result<T>> GetByIdAsync(Guid id)
    {
        try
        {
            var entity = await _appDbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

            if (entity == null)
            {
                _logger.LogWarning("{Entity} with ID {Id} not found in {tableName}", _entityName, id, _tableName);
                return Result<T>.Failure($"{_entityName} with ID {id} not found in {_tableName}",
                    StatusCodes.Status404NotFound);
            }

            _logger.LogInformation("Retrieved {Entity} with ID {Id} from {tableName}", _entityName, id, _tableName);
            return Result<T>.Success(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving {Entity} with ID {Id} from {tableName}", _entityName,
                id,
                _tableName);
            return Result<T>.Failure("An unexpected error occurred while retrieving the record.", ex.HResult);
        }
    }

    public async Task<Result<T>> CreateAsync(T entity)
    {
        try
        {
            await _appDbContext.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation("Created {Entity} with ID {Id} in {tableName}", _entityName, entity.Id, _tableName);
            return Result<T>.Success(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create  {entity} in {tableName}", _entityName, _tableName);
            return Result<T>.Failure($"An error occurred while creating the {_entityName}.", ex.HResult);
        }
    }

    public async Task<Result<T>> UpdateAsync(T entity)
    {
        try
        {
            var existingEntity = await _appDbContext.Set<T>().FirstOrDefaultAsync(t => t.Id == entity.Id);
            if (existingEntity == null)
            {
                _logger.LogWarning("Entity with ID {Id} not found in {tableName}", entity.Id, _tableName);
                return Result<T>.Failure($"{_entityName} with ID {entity.Id} not found in {_tableName}",
                    StatusCodes.Status404NotFound);
            }

            _appDbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation("Updated entity with ID {Id} in {tableName}", entity.Id, _tableName);
            return Result<T>.Success(entity);
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating entity with ID {Id} in {tableName}", entity.Id,
                _tableName);
            return Result<T>.Failure("An error occurred while updating the entity.", ex.HResult);
        }
    }

    public async Task<Result<T>> DeleteAsync(Guid id)
    {
        try
        {
            var entity = await _appDbContext.Set<T>().FirstOrDefaultAsync(t => t.Id == id);
            if (entity == null)
            {
                _logger.LogWarning("Entity with ID {Id} not found in {tableName}", id, _tableName);
                return Result<T>.Failure($"Entity with ID {id} not found in {_tableName}",
                    StatusCodes.Status404NotFound);
            }

            _appDbContext.Remove(entity);
            await _appDbContext.SaveChangesAsync();

            _logger.LogWarning("Deleted entity with ID {Id} from {tableName}", id, _tableName);
            return Result<T>.Success(entity);
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting entity with ID {Id} from {tableName}", id, _tableName);
            return Result<T>.Failure("An error occurred while deleting the entity.", ex.HResult);
        }
    }

    private static string? _GetTableName(AppDbContext appDbContext)
    {
        return appDbContext.Model.FindEntityType(typeof(T))?.GetTableName();
    }

    private static string? _GetEntityName(AppDbContext appDbContext)
    {
        var tableName = _GetTableName(appDbContext);
        return tableName.Substring(0, tableName.Length - 1);
    }
}