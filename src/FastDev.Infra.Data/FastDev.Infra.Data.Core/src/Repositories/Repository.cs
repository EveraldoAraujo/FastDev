using System.Linq.Expressions;

namespace FastDev.Infra.Data;
public abstract class Repository<T, TId> : RepositoryBase<T, TId>, IRepository<T, TId> where TId : struct
{
    public abstract Task<IEnumerable<T>> FilterAsync(IFilterData filterData);
    public abstract Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate);
}