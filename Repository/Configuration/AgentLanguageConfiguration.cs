using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class AgentLanguageConfiguration : IEntityTypeConfiguration<AgentLanguage>
{
    public void Configure(EntityTypeBuilder<AgentLanguage> builder)
    {
        builder.HasKey(x => new { x.AgentId, x.LanguageId });
        builder.HasOne(x => x.Agent).WithMany(x => x.AgentLanguages).HasForeignKey(x => x.AgentId);
        builder.HasOne(x => x.Language).WithMany(x => x.AgentLanguages).HasForeignKey(x => x.LanguageId);
    }
}
