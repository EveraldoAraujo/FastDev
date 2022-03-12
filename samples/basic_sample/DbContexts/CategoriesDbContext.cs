using FastDev.Sample.Models;
using Microsoft.EntityFrameworkCore;

namespace FastDev.Sample.DbContexts;

public class CategoriesDbContext : DbContext
{
    public DbSet<FastDev.Sample.Models.Context.Category.Category>? Categories { get; set; }

    public CategoriesDbContext(DbContextOptions<CategoriesDbContext> options) : base(options)
    {
        
    }

}

