using System.Linq.Expressions;

namespace FastDev.Infra.Data;
public interface ISearchableRepository<T, TId> : IRepositoryBase<T, TId> where TId : struct
{
    Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate);
}