using Entities.Models;
using Microsoft.AspNetCore.Identity;
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
    public DbSet<AdvertFeature> AdvertFeatures { get; set; }
    public DbSet<Agency> Agencies { get; set; }
    public DbSet<ImaginaryAgent> ImaginaryAgents { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("asp_net_users");
        modelBuilder.Entity<IdentityRole>().ToTable("asp_net_roles");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("asp_net_user_tokens");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("asp_net_user_roles");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("asp_net_role_claims");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("asp_net_user_claims");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("asp_net_user_logins");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryContext).Assembly);
    }
}