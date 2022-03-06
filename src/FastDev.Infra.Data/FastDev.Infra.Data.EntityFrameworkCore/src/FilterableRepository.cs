using Microsoft.EntityFrameworkCore;

namespace FastDev.Infra.Data.EntityFrameworkCore;
public abstract class FilterableRepository<T, TId> : RepositoryBase<T, TId>, IFilterableRepository<T, TId> where T : class, IDataModel<TId> where TId : struct
{
    public FilterableRepository(DbContext context) : base(context)
    {
    }

    public abstract Task<IEnumerable<T>> FilterAsync(IFilterData filterData);

}
