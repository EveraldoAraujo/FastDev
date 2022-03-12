using System.Linq.Expressions;

namespace FastDev.Service;

public interface ISearchableService<T, TId, TDbContext> : IServiceBase<T, TId, TDbContext> 
where TId : struct
where TDbContext: class
{
    Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate);
}