using KeyNekretnine.Domain.Agencies;
using KeyNekretnine.Domain.AgencyLanguages;
using KeyNekretnine.Domain.Languages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyNekretnine.Infrastructure.Configuration;
public class AgencyLanguageConfiguration : IEntityTypeConfiguration<AgencyLanguage>
{
    public void Configure(EntityTypeBuilder<AgencyLanguage> builder)
    {
        builder.ToTable("agency_languages");

        builder.HasKey(x => new { x.AgencyId, x.LanguageId });


        builder.HasOne<Agency>()
            .WithMany(agency => agency.AgencyLanguages)
            .HasForeignKey(agencyLanguage => agencyLanguage.AgencyId);


        builder.HasOne<Language>()
            .WithMany()
            .HasForeignKey(agencyLanguage => agencyLanguage.LanguageId);
    }
}