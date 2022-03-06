namespace FastDev.Infra.Data;
public abstract class RepositoryBase<T, TId> : IRepositoryBase<T, TId> where TId : struct
{
    public abstract Task<IEnumerable<T>> GetAllAsync();
    public abstract Task<T?> GetByIdAsync(TId id);
    public abstract Task<bool> CreateAsync(T obj);
    public abstract Task<bool> UpdateAsync(T obj);
    public abstract Task<bool> DeleteAsync(TId id);
    public abstract Task<bool> HasByIdAsync(TId id);
}