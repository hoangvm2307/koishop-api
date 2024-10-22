using KoishopBusinessObjects;
using KoishopRepositories.DatabaseContext;
using KoishopRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KoishopRepositories.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly KoishopContext _context;

    public GenericRepository(KoishopContext context)
    {
        _context = context;
    }
    public async Task<T> AddAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _context.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities)
    {
        _context.RemoveRange(entities);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().Where(e => e.isDeleted == false || e.isDeleted == null).AsNoTracking().ToListAsync();    
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>()
            .Where(e => e.isDeleted == false)
            .AsNoTracking().FirstOrDefaultAsync(q => q.Id.Equals(id));
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(IEnumerable<T> entities)
    {
        _context.UpdateRange(entities);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<T?> FindAsync(
            Expression<Func<T, bool>> filterExpression,
            CancellationToken cancellationToken = default)
    {
        return await QueryInternal(filterExpression).SingleOrDefaultAsync<T>(cancellationToken);
    }

    public virtual async Task<T?> FindAsync(
        Expression<Func<T, bool>> filterExpression,
        Func<IQueryable<T>, IQueryable<T>> queryOptions,
        CancellationToken cancellationToken = default)
    {
        return await QueryInternal(filterExpression, queryOptions).SingleOrDefaultAsync<T>(cancellationToken);
    }

    public virtual async Task<List<T>> FindAllAsync(CancellationToken cancellationToken = default)
    {
        return await QueryInternal(x => true).ToListAsync<T>(cancellationToken);
    }

    public virtual async Task<List<T>> FindAllAsync(
        Expression<Func<T, bool>> filterExpression,
        CancellationToken cancellationToken = default)
    {
        return await QueryInternal(filterExpression).ToListAsync<T>(cancellationToken);
    }

    public virtual async Task<List<T>> FindAllAsync(
        Expression<Func<T, bool>> filterExpression,
        Func<IQueryable<T>, IQueryable<T>> queryOptions,
        CancellationToken cancellationToken = default)
    {
        return await QueryInternal(filterExpression, queryOptions).ToListAsync<T>(cancellationToken);
    }

    public virtual async Task<IPagedResult<T>> FindAllAsync(
        int pageNo,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = QueryInternal(x => true);
        return await PagedList<T>.CreateAsync(
            query,
            pageNo,
            pageSize,
            cancellationToken);
    }

    public virtual async Task<IPagedResult<T>> FindAllAsync(
        Expression<Func<T, bool>> filterExpression,
        int pageNo,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = QueryInternal(filterExpression);
        return await PagedList<T>.CreateAsync(
            query,
            pageNo,
            pageSize,
            cancellationToken);
    }

    public virtual async Task<IPagedResult<T>> FindAllAsync(
        Expression<Func<T, bool>> filterExpression,
        int pageNo,
        int pageSize,
        Func<IQueryable<T>, IQueryable<T>> queryOptions,
        CancellationToken cancellationToken = default)
    {
        var query = QueryInternal(filterExpression, queryOptions);
        return await PagedList<T>.CreateAsync(
            query,
            pageNo,
            pageSize,
            cancellationToken);
    }

    public virtual async Task<int> CountAsync(
        Expression<Func<T, bool>> filterExpression,
        CancellationToken cancellationToken = default)
    {
        return await QueryInternal(filterExpression).CountAsync(cancellationToken);
    }

    public bool Any(Expression<Func<T, bool>> filterExpression)
    {
        return QueryInternal(filterExpression).Any();
    }

    public virtual async Task<bool> AnyAsync(
        Expression<Func<T, bool>> filterExpression,
        CancellationToken cancellationToken = default)
    {
        return await QueryInternal(filterExpression).AnyAsync(cancellationToken);
    }

    public virtual async Task<T?> FindAsync(
        Func<IQueryable<T>, IQueryable<T>> queryOptions,
        CancellationToken cancellationToken = default)
    {
        return await QueryInternal(queryOptions).SingleOrDefaultAsync<T>(cancellationToken);
    }

    public virtual async Task<List<T>> FindAllAsync(
        Func<IQueryable<T>, IQueryable<T>> queryOptions,
        CancellationToken cancellationToken = default)
    {
        return await QueryInternal(queryOptions).ToListAsync<T>(cancellationToken);
    }

    public virtual async Task<IPagedResult<T>> FindAllAsync(
        int pageNo,
        int pageSize,
        Func<IQueryable<T>, IQueryable<T>> queryOptions,
        CancellationToken cancellationToken = default)
    {
        var query = QueryInternal(queryOptions);
        return await PagedList<T>.CreateAsync(
            query,
            pageNo,
            pageSize,
            cancellationToken);
    }

    public virtual async Task<int> CountAsync(
        Func<IQueryable<T>, IQueryable<T>>? queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return await QueryInternal(queryOptions).CountAsync(cancellationToken);
    }

    public virtual async Task<bool> AnyAsync(
        Func<IQueryable<T>, IQueryable<T>>? queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return await QueryInternal(queryOptions).AnyAsync(cancellationToken);
    }

    protected virtual IQueryable<T> QueryInternal(Expression<Func<T, bool>>? filterExpression)
    {
        var queryable = CreateQuery();
        if (filterExpression != null)
        {
            queryable = queryable.Where(filterExpression);
        }
        return queryable;
    }

    protected virtual IQueryable<TResult> QueryInternal<TResult>(
        Expression<Func<T, bool>> filterExpression,
        Func<IQueryable<T>, IQueryable<TResult>> queryOptions)
    {
        var queryable = CreateQuery();
        queryable = queryable.Where(filterExpression);
        var result = queryOptions(queryable);
        return result;
    }

    protected virtual IQueryable<T> QueryInternal(Func<IQueryable<T>, IQueryable<T>>? queryOptions)
    {
        var queryable = CreateQuery();
        if (queryOptions != null)
        {
            queryable = queryOptions(queryable);
        }
        return queryable;
    }

    protected virtual IQueryable<T> CreateQuery()
    {
        return GetSet();
    }

    protected virtual DbSet<T> GetSet()
    {
        return _context.Set<T>();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Dictionary<TKey, TValue>> FindAllToDictionaryAsync<TKey, TValue>(
        Expression<Func<T, bool>> filterExpression,
        Expression<Func<T, TKey>> keySelector,
        Expression<Func<T, TValue>> valueSelector,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _context.Set<T>().Where(filterExpression);
        return await query.ToDictionaryAsync(keySelector.Compile(), valueSelector.Compile(), cancellationToken);
    }
}
