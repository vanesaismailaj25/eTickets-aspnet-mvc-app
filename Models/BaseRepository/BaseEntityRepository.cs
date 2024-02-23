
using eTickets.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eTickets.Models.BaseRepository;

public class BaseEntityRepository<T> : IBaseEntityRepository<T> where T : class, IBaseEntity, new()
{
    private readonly AppDbContext _context;
    public BaseEntityRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int entityId)
    {
        var entity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == entityId);
        
        EntityEntry entityEntry = _context.Entry<T>(entity);
        entityEntry.State = EntityState.Deleted;
        
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var result = await _context.Set<T>().ToListAsync();

        return result;
    }

    public async Task<T> GetEntityByIdAsync(int entityId)
    {
        var entity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == entityId);

        return entity;
    }

    public async Task UpdateAsync(int entityId, T entity)
    {
        EntityEntry entityEntry = _context.Entry<T>(entity);
        entityEntry.State = EntityState.Modified;
        
        await _context.SaveChangesAsync();
    }
}
