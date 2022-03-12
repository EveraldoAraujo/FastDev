using FastDev.Sample.Models;
using Microsoft.EntityFrameworkCore;

namespace FastDev.Sample.DbContexts;

public class AppDbContext : DbContext
{
    public DbSet<Product>? Products { get; set; }
    
    public DbSet<FastDev.Sample.Models.Category>? Categories { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().Navigation(n=>n.Category).AutoInclude();
        modelBuilder.Entity<Category>().Navigation(n=>n.Products).AutoInclude(false);

    }
}

