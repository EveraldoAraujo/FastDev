using FastDev.Infra.Data;

namespace FastDev.Service;
public class ServiceBase<T, TId, TDbContext> : BaseService<T, IRepositoryBase<T, TId>, TId, TDbContext>, IServiceBase<T, TId, TDbContext> 
where TId : struct
where TDbContext: class
{
    public ServiceBase(IRepositoryBase<T, TId> repository, IUoW<TDbContext> uoW) : base(repository, uoW)
    {
    }

}