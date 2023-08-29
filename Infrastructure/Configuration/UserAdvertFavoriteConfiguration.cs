using KeyNekretnine.Domain.UserAdvertFavorites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyNekretnine.Infrastructure.Configuration;
public class UserAdvertFavoriteConfiguration : IEntityTypeConfiguration<UserAdvertFavorite>
{
    public void Configure(EntityTypeBuilder<UserAdvertFavorite> builder)
    {
        builder.HasKey(x => new { x.UserId, x.AdvertId });
        builder.HasOne(x => x.Advert).WithMany(x => x.UserAdvertFavorites).HasForeignKey(x => x.AdvertId);
        builder.HasOne(x => x.User).WithMany(x => x.UserAdvertFavorites).HasForeignKey(x => x.UserId);
        builder.Property(x => x.CreatedFavoriteDate).IsRequired();
    }
}