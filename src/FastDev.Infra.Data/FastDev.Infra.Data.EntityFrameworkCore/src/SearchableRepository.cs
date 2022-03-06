using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace FastDev.Infra.Data.EntityFrameworkCore;
public class SearchableRepository<T, TId> : RepositoryBase<T, TId>, ISearchableRepository<T, TId> where T : class, IDataModel<TId> where TId : struct
{
    public SearchableRepository(DbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> searchExpression)
    {
        return await Task.Run(() => base._dataSet.AsQueryable().Where(searchExpression));
    }

}
