using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.AdvertFeatures;
using KeyNekretnine.Domain.AdvertPurposes;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.AdvertStatuses;
using KeyNekretnine.Domain.AdvertTypes;
using KeyNekretnine.Domain.Cities;
using KeyNekretnine.Domain.Images;
using KeyNekretnine.Domain.Neighborhoods;
using KeyNekretnine.Domain.RejectReasons;
using KeyNekretnine.Domain.TemporeryImageDatas;
using KeyNekretnine.Domain.UserAdvertFavorites;
using KeyNekretnine.Domain.UserAdvertReports;
using KeyNekretnine.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KeyNekretnine.Infrastructure;
public sealed class ApplicationDbContext : IdentityDbContext<User>, IUnitOfWork
{
    private readonly IPublisher _publisher;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
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
    //public DbSet<PhoneNumber> PhoneNumbers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("asp_net_users");
        modelBuilder.Entity<IdentityRole>().ToTable("asp_net_roles");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("asp_net_user_tokens");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("asp_net_user_roles");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("asp_net_role_claims");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("asp_net_user_claims");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("asp_net_user_logins");
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //try
        //{
        var result = await base.SaveChangesAsync(cancellationToken);

        //await PublishDomainEventsAsync();

        return result;
        //}
        //catch (DbUpdateConcurrencyException ex)
        //{
        //    throw new ConcurrencyException("Concurrency exception occurred.", ex);
        //}
    }

    //private async Task PublishDomainEventsAsync()
    //{
    //    var domainEvents = ChangeTracker
    //        .Entries<Entity>()
    //        .Select(entry => entry.Entity)
    //        .SelectMany(entity =>
    //        {
    //            var domainEvents = entity.GetDomainEvents();

    //            entity.ClearDomainEvents();

    //            return domainEvents;
    //        })
    //        .ToList();

    //    foreach (var domainEvent in domainEvents)
    //    {
    //        await _publisher.Publish(domainEvent);
    //    }
    //}
}
