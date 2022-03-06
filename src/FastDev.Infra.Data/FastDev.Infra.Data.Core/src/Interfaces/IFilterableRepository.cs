namespace FastDev.Infra.Data;
public interface IFilterableRepository<T, TId> : IRepositoryBase<T, TId> where TId : struct
{
    Task<IEnumerable<T>> FilterAsync(IFilterData filterData);
}