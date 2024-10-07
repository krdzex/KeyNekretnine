using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.AdvertUpdates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyNekretnine.Infrastructure.Configuration;
internal sealed class UpdateAdvertConfiguration : IEntityTypeConfiguration<AdvertUpdate>
{
    public void Configure(EntityTypeBuilder<AdvertUpdate> builder)
    {
        builder.ToTable("advert_updates");

        builder.HasKey(updateAdvert => updateAdvert.Id);

        builder.Property(updateAdvert => updateAdvert.NewContent).IsRequired(false).HasColumnType("json");

        builder.Property(updateAdvert => updateAdvert.OldContent).IsRequired(false).HasColumnType("json");

        builder.HasOne<Advert>(u => u.Advert)
            .WithMany()
            .HasForeignKey(updateAdvert => updateAdvert.AdvertId);
    }
}
