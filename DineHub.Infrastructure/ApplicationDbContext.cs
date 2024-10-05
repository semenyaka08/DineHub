using DineHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DineHub.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    public DbSet<Restaurant> Restaurants { get; set; }

    public DbSet<Dish> Dishes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>().OwnsOne(z => z.Address);
    }
}