namespace WebbDesignBlazorLabb3.Server.DataAccess;

public interface IRepository<T>
{
    Task AddAsync(T entity);
    Task DeleteAsync(long isbn);
    Task DeleteAsync(string email, long isbn);
    Task<T> GetAsync(long isbn);
    Task<IEnumerable<T>> GetAllAsync();
    Task UpdateAsync(T entity);
	Task<T> GetAsync(string email);
}
