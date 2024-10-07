using KoishopBusinessObjects;
using KoishopRepositories.DatabaseContext;
using KoishopRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
