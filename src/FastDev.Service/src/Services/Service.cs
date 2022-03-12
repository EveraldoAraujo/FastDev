using System.Linq.Expressions;
using FastDev.Infra.Data;

namespace FastDev.Service;
public class Service<T, TId, TDbContext> : BaseService<T, IRepository<T, TId>, TId, TDbContext>, IService<T, TId, TDbContext> 
where T : class 
where TId : struct
where TDbContext: class
{
    public Service(IRepository<T, TId> repository, IUoW<TDbContext> uoW) : base(repository, uoW)
    { }
    public async Task<IEnumerable<T>> FilterAsync(IFilterData filterData)
    => await _repository.FilterAsync(filterData);

    public async Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate)
    => await _repository.SearchAsync(predicate);
}