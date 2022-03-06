using FastDev.Infra.Data;
namespace FastDev.Service;

public interface IFilterableService<T, TId> : IServiceBase<T, TId> where TId : struct
{
    Task<IEnumerable<T>> FilterAsync(IFilterData filterData);
}