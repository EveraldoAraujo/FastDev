using FastDev.Infra.Data;

namespace FastDev.Service;
public class FilterableService<T, TId, TDbContext> : BaseService<T, IFilterableRepository<T, TId>, TId, TDbContext>, IFilterableService<T, TId, TDbContext> 
where T : class 
where TId : struct 
where TDbContext:class
{
    public FilterableService(IFilterableRepository<T, TId> repository, IUoW<TDbContext> uoW) : base(repository, uoW)
    { }
    public async Task<IEnumerable<T>> FilterAsync(IFilterData filterData)
    => await _repository.FilterAsync(filterData);
}