using FastDev.Sample.Models;
using Microsoft.EntityFrameworkCore;

namespace FastDev.DbContexts;

public class AppDbContext : DbContext
{
    public DbSet<Product>? Products { get; set; }
    public DbSet<Category>? Categories { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().Navigation(n=>n.Products).AutoInclude();
    }
}

