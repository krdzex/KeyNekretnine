using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class ImaginaryAgentLanguageConfiguration : IEntityTypeConfiguration<ImaginaryAgentLanguage>
{
    public void Configure(EntityTypeBuilder<ImaginaryAgentLanguage> builder)
    {
        builder.HasKey(x => new { x.ImaginaryAgentId, x.LanguageId });
        builder.HasOne(x => x.ImaginaryAgent).WithMany(x => x.ImaginaryAgentLanguages).HasForeignKey(x => x.ImaginaryAgentId);
        builder.HasOne(x => x.Language).WithMany(x => x.ImaginaryAgentLanguages).HasForeignKey(x => x.LanguageId);
    }
}
