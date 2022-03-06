using Microsoft.EntityFrameworkCore;

namespace FastDev.Infra.Data.EntityFrameworkCore;
public abstract class Repository<T, TId> : SearchableRepository<T, TId>, IFilterableRepository<T, TId> where T : class, IDataModel<TId> where TId : struct
{
    public Repository(DbContext context) : base(context)
    {
    }

    public abstract Task<IEnumerable<T>> FilterAsync(IFilterData filterData);
}
