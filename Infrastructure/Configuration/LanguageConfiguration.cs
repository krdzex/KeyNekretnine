using KeyNekretnine.Domain.Agencies;
using KeyNekretnine.Domain.Languages;
using KeyNekretnine.Infrastructure.Configuration.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyNekretnine.Infrastructure.Configuration;
public class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.ToTable("languages");

        builder.HasKey(language => language.Id);

        builder.Property(language => language.Name)
            .HasMaxLength(50)
            .HasConversion(language => language.Value, value => new Name(value))
            .IsRequired();

        builder.HasData(LanguageData.GetLanguages());
    }
}
