using FastDev.Infra.Data;

namespace FastDev.Service;
public class FilterableService<T, TId> : BaseService<T, IFilterableRepository<T, TId>, TId>, IFilterableService<T, TId> where T : class where TId : struct
{
    public FilterableService(IFilterableRepository<T, TId> repository, IUoW uoW) : base(repository, uoW)
    { }
    public async Task<IEnumerable<T>> FilterAsync(IFilterData filterData)
    => await _repository.FilterAsync(filterData);
}