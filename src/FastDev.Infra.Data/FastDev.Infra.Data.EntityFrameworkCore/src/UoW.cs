using Microsoft.EntityFrameworkCore;

namespace FastDev.Infra.Data.EntityFrameworkCore;

public class UoW : IUoW
{
    private readonly DbContext _context;

    public UoW(DbContext context) => (_context) = (context);

    public async Task CommitTransaction()
    {
        await _context.SaveChangesAsync();
    }
}
