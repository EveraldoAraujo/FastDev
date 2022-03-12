using FastDev.Infra.Data;
namespace FastDev.Service;

public interface IFilterableService<T, TId, TDbContext> : IServiceBase<T, TId, TDbContext>
where TId : struct
where TDbContext: class
{
    Task<IEnumerable<T>> FilterAsync(IFilterData filterData);
}