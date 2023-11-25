using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.UserAdvertFavorites;
using KeyNekretnine.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyNekretnine.Infrastructure.Configuration;
public class UserAdvertFavoriteConfiguration : IEntityTypeConfiguration<UserAdvertFavorite>
{
    public void Configure(EntityTypeBuilder<UserAdvertFavorite> builder)
    {
        builder.ToTable("user_advert_favorites");

        builder.HasKey(x => new { x.UserId, x.AdvertId });

        builder.Property(x => x.CreatedFavoriteDate).IsRequired();

        builder.HasOne<Advert>()
            .WithMany()
            .HasForeignKey(favorite => favorite.AdvertId);

        builder.HasOne<User>()
            .WithMany(user => user.UserAdvertFavorites)
            .HasForeignKey(favorite => favorite.UserId);

    }
}