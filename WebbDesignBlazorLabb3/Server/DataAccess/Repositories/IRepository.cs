﻿namespace WebbDesignBlazorLabb3.Server.DataAccess;

public interface IRepository<T>
{
    Task AddAsync(T entity);
    Task DeleteAsync(object id);
    Task<T> GetAsync(long isbn);
    Task<IEnumerable<T>> GetAllAsync();
    Task UpdateAsync(T entity);
}
