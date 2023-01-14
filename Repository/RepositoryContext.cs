using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository;
public class RepositoryContext : IdentityDbContext<User>
{
    public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
    {

    }

    public DbSet<Image> Images { get; set; }
    public DbSet<AdvertStatus> AdvertStatus { get; set; }
    public DbSet<AdvertPurpose> AdvertPurpose { get; set; }
    public DbSet<AdvertType> AdvertType { get; set; }
    public DbSet<Advert> Adverts { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Neighborhood> Neighborhoods { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryContext).Assembly);
    }
}
