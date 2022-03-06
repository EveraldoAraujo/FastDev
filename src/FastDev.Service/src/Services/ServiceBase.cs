using FastDev.Infra.Data;

namespace FastDev.Service;
public class ServiceBase<T, TId> : BaseService<T, IRepositoryBase<T, TId>, TId>, IServiceBase<T, TId> where TId : struct
{
    public ServiceBase(IRepositoryBase<T, TId> repository, IUoW uoW) : base(repository, uoW)
    {
    }

}