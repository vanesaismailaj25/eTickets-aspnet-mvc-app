﻿using System.Linq.Expressions;

namespace eTickets.Models.BaseRepository;

public interface IBaseEntityRepository<T> where T : class, IBaseEntity, new()
{
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
    public Task<T> GetEntityByIdAsync(int entityId);
    public Task AddAsync(T entity);
    public Task UpdateAsync(int entityId, T entity);
    public Task DeleteAsync(int entityId);
}
