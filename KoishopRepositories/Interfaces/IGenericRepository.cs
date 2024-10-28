using KoishopBusinessObjects;
using System.Linq.Expressions;

namespace KoishopRepositories.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task UpdateRangeAsync(IEnumerable<T> entities);
    Task DeleteAsync(T entity);
    Task DeleteRangeAsync(IEnumerable<T> entities);

    Task<T?> FindAsync(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken = default);
    Task<T?> FindAsync(Expression<Func<T, bool>> filterExpression, Func<IQueryable<T>, IQueryable<T>> queryOptions, CancellationToken cancellationToken = default);
    Task<T?> FindAsync(Func<IQueryable<T>, IQueryable<T>> queryOptions, CancellationToken cancellationToken = default);
    Task<List<T>> FindAllAsync(CancellationToken cancellationToken = default);
    Task<List<T>> FindAllAsync(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken = default);
    Task<List<T>> FindAllAsync(Expression<Func<T, bool>> filterExpression, Func<IQueryable<T>, IQueryable<T>> queryOptions, CancellationToken cancellationToken = default);
    Task<IPagedResult<T>> FindAllAsync(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<IPagedResult<T>> FindAllAsync(Expression<Func<T, bool>> filterExpression, int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<IPagedResult<T>> FindAllAsync(Expression<Func<T, bool>> filterExpression, int pageNo, int pageSize, Func<IQueryable<T>, IQueryable<T>> queryOptions, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken = default);

    Task<List<T>> FindAllAsync(Func<IQueryable<T>, IQueryable<T>> queryOptions, CancellationToken cancellationToken = default);
    Task<IPagedResult<T>> FindAllAsync(int pageNo, int pageSize, Func<IQueryable<T>, IQueryable<T>> queryOptions, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Func<IQueryable<T>, IQueryable<T>>? queryOptions = default, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Func<IQueryable<T>, IQueryable<T>>? queryOptions = default, CancellationToken cancellationToken = default);

    Task<Dictionary<TKey, TValue>> FindAllToDictionaryAsync<TKey, TValue>(
        Expression<Func<T, bool>> filterExpression,
        Expression<Func<T, TKey>> keySelector,
        Expression<Func<T, TValue>> valueSelector,
        CancellationToken cancellationToken = default);
}
