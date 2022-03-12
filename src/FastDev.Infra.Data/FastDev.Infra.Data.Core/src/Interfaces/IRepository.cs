using System.Linq.Expressions;

namespace FastDev.Infra.Data;
public interface IRepository<T, TId> : IRepositoryBase<T, TId>, ISearchableRepository<T, TId>, IFilterableRepository<T, TId> where TId : struct
{

}