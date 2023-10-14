using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.RejectReasons;
using KeyNekretnine.Domain.UserAdvertReports;
using KeyNekretnine.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyNekretnine.Infrastructure.Configuration;
public class UserAdvertReportConfiguration : IEntityTypeConfiguration<UserAdvertReport>
{
    public void Configure(EntityTypeBuilder<UserAdvertReport> builder)
    {
        builder.ToTable("user_advert_reports");

        builder.HasKey(x => new { x.UserId, x.AdvertId, x.RejectReasonId });

        builder.Property(x => x.CreatedReportDate).IsRequired();

        builder.HasOne<Advert>()
            .WithMany()
            .HasForeignKey(reports => reports.AdvertId);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(reports => reports.UserId);

        builder.HasOne<RejectReason>()
            .WithMany()
            .HasForeignKey(reports => reports.RejectReasonId);
    }
}