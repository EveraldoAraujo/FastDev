using System.Linq.Expressions;
using FastDev.Infra.Data;

namespace FastDev.Service;
public class Service<T, TId> : BaseService<T, IRepository<T, TId>, TId>, IService<T, TId> where T : class where TId : struct
{
    public Service(IRepository<T, TId> repository, IUoW uoW) : base(repository, uoW)
    { }
    public async Task<IEnumerable<T>> FilterAsync(IFilterData filterData)
    => await _repository.FilterAsync(filterData);

    public async Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate)
    => await _repository.SearchAsync(predicate);
}