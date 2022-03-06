
namespace FastDev.Service
{
    public interface IService<T, TId> : ISearchableService<T, TId>, IFilterableService<T, TId> where TId : struct
    {

    }
}