using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;
public class AgencyLanguageConfiguration : IEntityTypeConfiguration<AgencyLanguage>
{
    public void Configure(EntityTypeBuilder<AgencyLanguage> builder)
    {
        builder.HasKey(x => new { x.AgencyId, x.LanguageId });
        builder.HasOne(x => x.Agency).WithMany(x => x.AgencyLanguages).HasForeignKey(x => x.AgencyId);
        builder.HasOne(x => x.Language).WithMany(x => x.AgencyLanguages).HasForeignKey(x => x.LanguageId);
    }
}
