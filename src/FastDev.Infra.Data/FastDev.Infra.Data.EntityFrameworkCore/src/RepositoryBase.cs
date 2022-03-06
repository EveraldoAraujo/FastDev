using Microsoft.EntityFrameworkCore;

namespace FastDev.Infra.Data.EntityFrameworkCore;
public class RepositoryBase<T, TId> : FastDev.Infra.Data.RepositoryBase<T, TId>, IRepositoryBase<T, TId> where T : class, IDataModel<TId> where TId : struct
{
    private readonly DbContext _context;
    protected readonly DbSet<T> _dataSet;
    public RepositoryBase(DbContext context) => (_context, _dataSet) = (context, context.Set<T>());

    public override async Task<IEnumerable<T>> GetAllAsync()
    => await _dataSet.Where(e => e.Deleted == false).ToListAsync();

    public override async Task<T?> GetByIdAsync(TId id)
    {
        _dataSet.AsNoTracking().Where(e => e.Id.Equals(id));
        var entity = await _dataSet.FindAsync(id);

        if (entity is null || entity.Deleted == true)
            return null;

        return entity;
    }

    public override async Task<bool> HasByIdAsync(TId id)
    {
        var entity = await _dataSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id) && e.Deleted == false);

        if (entity is not null && entity.Deleted is false)
            return true;

        return false;
    }

    public override async Task<bool> CreateAsync(T obj)
    => (await _dataSet.AddAsync(obj)).State == EntityState.Added;

    public override async Task<bool> UpdateAsync(T obj)
    => (await Task.Run(() => _dataSet.Update(obj))).State == EntityState.Modified;

    public override async Task<bool> DeleteAsync(TId id)
    {
        T? obj = await GetByIdAsync(id);
        if (obj is null)
            return false;

        obj.SetDeleted();

        return _dataSet.Update(obj).State == EntityState.Modified;
    }
}