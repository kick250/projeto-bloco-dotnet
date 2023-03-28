using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;
public class HomeRepairContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }

    public HomeRepairContext(DbContextOptions<HomeRepairContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HomeRepairContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

