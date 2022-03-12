using Microsoft.EntityFrameworkCore;

namespace FastDev.Infra.Data.EntityFrameworkCore;
public abstract class Repository<T, TId, TDbContext> : SearchableRepository<T, TId, TDbContext>, IFilterableRepository<T, TId> where T : class, IDataModel<TId> where TId : struct where TDbContext : DbContext
{
    public Repository(TDbContext context) : base(context)
    {
    }

    public abstract Task<IEnumerable<T>> FilterAsync(IFilterData filterData);
}
