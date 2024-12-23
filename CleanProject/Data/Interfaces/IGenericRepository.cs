using CleanProject.Domain.Entities;
using CleanProject.Shared.Model;

namespace CleanProject.Data.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<Result<IReadOnlyList<T>>> GetAllAsync();
    Task<Result<T>> GetByIdAsync(Guid id);
    Task<Result<T>> CreateAsync(T entity);
    Task<Result<T>> UpdateAsync(T entity);
    Task<Result<T>> DeleteAsync(Guid id);
}