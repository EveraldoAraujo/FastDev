
namespace FastDev.Service
{
    public interface IServiceBase<T, TId, TDbContext> : IBaseService<T, TId, TDbContext>
    where TId : struct
    where TDbContext : class
    {
    }
}