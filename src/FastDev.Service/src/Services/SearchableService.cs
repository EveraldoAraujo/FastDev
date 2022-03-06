using System.Linq.Expressions;
using FastDev.Infra.Data;

namespace FastDev.Service;

public class SearchableService<T, TId> : BaseService<T, ISearchableRepository<T, TId>, TId>, ISearchableService<T, TId> where TId : struct
{
    public SearchableService(ISearchableRepository<T, TId> repository, IUoW uoW) : base(repository, uoW)
    { }

    public async Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate)
    => await _repository.SearchAsync(predicate);

}