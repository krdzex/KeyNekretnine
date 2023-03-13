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
    public DbSet<AdvertStatus> AdvertStatuses { get; set; }
    public DbSet<AdvertPurpose> AdvertPurposes { get; set; }
    public DbSet<AdvertType> AdvertTypes { get; set; }
    public DbSet<Advert> Adverts { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Neighborhood> Neighborhoods { get; set; }
    public DbSet<TemporeryImageData> TemporeryImagesData { get; set; }
    public DbSet<UserAdvertFavorite> UserAdvertFavorites { get; set; }
    public DbSet<RejectReason> RejectReasons { get; set; }
    public DbSet<UserAdvertReport> UserAdvertReports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryContext).Assembly);
    }
}