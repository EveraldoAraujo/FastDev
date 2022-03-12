namespace FastDev.Infra.Data;
public interface IRepositoryBase<T, TId> where TId : struct
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(TId id);
    Task<Boolean> CreateAsync(T obj);
    Task<Boolean> UpdateAsync(T obj);
    Task<Boolean> DeleteAsync(TId id);
    Task<bool> HasByIdAsync(TId id);
}