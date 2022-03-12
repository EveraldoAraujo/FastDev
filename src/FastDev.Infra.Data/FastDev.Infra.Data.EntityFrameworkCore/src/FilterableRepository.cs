using Microsoft.EntityFrameworkCore;

namespace FastDev.Infra.Data.EntityFrameworkCore;
public abstract class FilterableRepository<T, TId, TDbContext> : RepositoryBase<T, TId, TDbContext>, IFilterableRepository<T, TId> where T : class, IDataModel<TId> where TId : struct where TDbContext : DbContext
{
    public FilterableRepository(TDbContext context) : base(context)
    {
    }

    public abstract Task<IEnumerable<T>> FilterAsync(IFilterData filterData);

}
