
namespace FastDev.Service
{
    public interface IServiceBase<T, TId> : IBaseService<T, TId> where TId : struct
    {
    }
}