
namespace FastDev.Service
{
    public interface IService<T, TId, TDbContext> : ISearchableService<T, TId, TDbContext>, IFilterableService<T, TId, TDbContext>
    where TId : struct
    where TDbContext: class
    {

    }
}