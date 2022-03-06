using System.Linq.Expressions;

namespace FastDev.Infra.Data;
public abstract class SearchableRepository<T, TId> : RepositoryBase<T, TId>, ISearchableRepository<T, TId> where TId : struct
{
    public abstract Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> searchExpression);
}