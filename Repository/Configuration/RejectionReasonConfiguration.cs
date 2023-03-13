using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;
public class RejectionReasonConfiguration : IEntityTypeConfiguration<RejectionReason>
{
    public void Configure(EntityTypeBuilder<RejectionReason> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ReasonSr).IsRequired().HasMaxLength(100);
        builder.Property(x => x.ReasonEn).IsRequired().HasMaxLength(100);
        builder.HasData(
           new RejectionReason
           {
               Id = 1,
               ReasonSr = "Oglas slican ovome vec postoji.",
               ReasonEn = "The advertisement similar to this already exists."
           },
           new RejectionReason
           {
               Id = 2,
               ReasonSr = "Slike za oglas su neprimjerene ili nisu tacne.",
               ReasonEn = "The advert images are inappropriate or accurate."
           },
           new RejectionReason
           {
               Id = 3,
               ReasonSr = "Podaci o oglasu su neprimjereni ili nisu tacni.",
               ReasonEn = "The advert informations are inappropriate or accurate."
           });
    }
}