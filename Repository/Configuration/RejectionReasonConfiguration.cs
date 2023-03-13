using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;
public class RejectionReasonConfiguration : IEntityTypeConfiguration<RejectReason>
{
    public void Configure(EntityTypeBuilder<RejectReason> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ReasonSr).IsRequired().HasMaxLength(100);
        builder.Property(x => x.ReasonEn).IsRequired().HasMaxLength(100);
        builder.HasData(
           new RejectReason
           {
               Id = 1,
               ReasonSr = "Oglas slican ovome vec postoji.",
               ReasonEn = "The advertisement similar to this already exists."
           },
           new RejectReason
           {
               Id = 2,
               ReasonSr = "Slike za oglas su neprimjerene ili nisu tacne.",
               ReasonEn = "The advert images are inappropriate or accurate."
           },
           new RejectReason
           {
               Id = 3,
               ReasonSr = "Podaci o oglasu su neprimjereni ili nisu tacni.",
               ReasonEn = "The advert informations are inappropriate or accurate."
           });
    }
}