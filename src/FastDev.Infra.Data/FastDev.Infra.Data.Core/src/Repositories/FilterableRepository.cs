namespace FastDev.Infra.Data;
public abstract class FilterableRepository<T, TId> : RepositoryBase<T, TId>, IFilterableRepository<T, TId> where TId : struct
{
    public abstract Task<IEnumerable<T>> FilterAsync(IFilterData filterData);
}