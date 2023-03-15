using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;
public class UserAdvertReportConfiguration : IEntityTypeConfiguration<UserAdvertReport>
{
    public void Configure(EntityTypeBuilder<UserAdvertReport> builder)
    {
        builder.HasKey(x => new { x.UserId, x.AdvertId, x.RejectReasonId });
        builder.HasOne(x => x.Advert).WithMany(x => x.UserAdvertReports).HasForeignKey(x => x.AdvertId);
        builder.HasOne(x => x.User).WithMany(x => x.UserAdvertReports).HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.RejectReason).WithMany(x => x.UserAdvertReports).HasForeignKey(x => x.RejectReasonId);
        builder.Property(x => x.CreatedReportDate).IsRequired();
    }
}