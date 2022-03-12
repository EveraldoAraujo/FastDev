using FastDev.Notifications.Interfaces;

namespace FastDev.Service
{
    public interface IBaseService<T, TId, TDbContext> 
    where TId : struct
    where TDbContext: class
    {
        IEnumerable<INotification> Notifications { get; set; }
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(TId id);
        Task<Boolean> CreateAsync(T obj, bool commitTransaction = true);
        Task<Boolean> UpdateAsync(T obj, bool commitTransaction = true);
        Task<Boolean> DeleteAsync(TId id, bool commitTransaction = true);
        Task<bool> HasByIdAsync(TId id);
    }
}