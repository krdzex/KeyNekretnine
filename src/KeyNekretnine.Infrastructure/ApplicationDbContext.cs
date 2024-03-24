using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Exceptions;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.AdvertFeatures;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.AdvertUpdates;
using KeyNekretnine.Domain.Cities;
using KeyNekretnine.Domain.Images;
using KeyNekretnine.Domain.Neighborhoods;
using KeyNekretnine.Domain.RejectReasons;
using KeyNekretnine.Domain.TemporeryImageDatas;
using KeyNekretnine.Domain.UserAdvertFavorites;
using KeyNekretnine.Domain.UserAdvertReports;
using KeyNekretnine.Domain.Users;
using KeyNekretnine.Infrastructure.BackgroundJobs.Outbox;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace KeyNekretnine.Infrastructure;
public sealed class ApplicationDbContext : IdentityDbContext<User>, IUnitOfWork
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    private readonly IDateTimeProvider _dateTimeProvider;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeProvider dateTimeProvider)
        : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public DbSet<Image> Images { get; set; }
    public DbSet<Advert> Adverts { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Neighborhood> Neighborhoods { get; set; }
    public DbSet<TemporeryImageData> TemporeryImagesData { get; set; }
    public DbSet<UserAdvertFavorite> UserAdvertFavorites { get; set; }
    public DbSet<RejectReason> RejectReasons { get; set; }
    public DbSet<UserAdvertReport> UserAdvertReports { get; set; }
    public DbSet<AdvertFeature> AdvertFeatures { get; set; }
    public DbSet<AdvertUpdate> AdvertUpdates { get; set; }

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
        try
        {
            AddDomainEventsAsOutboxMessages();

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency exception occurred.", ex);
        }
    }

    private void AddDomainEventsAsOutboxMessages()
    {
        var outboxMessages = ChangeTracker
            .Entries<IEntity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents();

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .Select(domainEvent => new OutboxMessage(
                Guid.NewGuid(),
                _dateTimeProvider.Now,
                domainEvent.GetType().Name,
                JsonConvert.SerializeObject(domainEvent, JsonSerializerSettings)))
            .ToList();

        AddRange(outboxMessages);
    }
}
