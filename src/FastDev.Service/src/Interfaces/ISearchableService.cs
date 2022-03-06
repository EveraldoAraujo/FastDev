using System.Linq.Expressions;

namespace FastDev.Service;

public interface ISearchableService<T, TId> : IServiceBase<T, TId> where TId : struct
{
    Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate);
}