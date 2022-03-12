using System.Linq.Expressions;
using FastDev.Infra.Data;

namespace FastDev.Service;

public class SearchableService<T, TId, TDbContext> : BaseService<T, ISearchableRepository<T, TId>, TId, TDbContext>, ISearchableService<T, TId, TDbContext> 
where TId : struct
where TDbContext: class
{
    public SearchableService(ISearchableRepository<T, TId> repository, IUoW<TDbContext> uoW) : base(repository, uoW)
    { }

    public async Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate)
    => await _repository.SearchAsync(predicate);

}