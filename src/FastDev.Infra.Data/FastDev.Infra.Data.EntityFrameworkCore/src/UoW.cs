using Microsoft.EntityFrameworkCore;

namespace FastDev.Infra.Data.EntityFrameworkCore;

public class UoW<TDbContext> : IUoW<TDbContext> where TDbContext: DbContext
{
    private readonly TDbContext _context;

    public UoW(TDbContext context) => (_context) = (context);

    public async Task CommitTransaction()
    {
        await _context.SaveChangesAsync();
    }
}
