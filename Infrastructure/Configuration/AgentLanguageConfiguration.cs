using KeyNekretnine.Domain.AgentLanguages;
using KeyNekretnine.Domain.Agents;
using KeyNekretnine.Domain.Languages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyNekretnine.Infrastructure.Configuration;

public class AgentLanguageConfiguration : IEntityTypeConfiguration<AgentLanguage>
{
    public void Configure(EntityTypeBuilder<AgentLanguage> builder)
    {
        builder.ToTable("agent_languages");

        builder.HasKey(x => new { x.AgentId, x.LanguageId });

        builder.HasOne<Agent>()
            .WithMany(agent => agent.AgentLanguages)
            .HasForeignKey(agentLanguage => agentLanguage.AgentId);

        builder.HasOne<Language>()
            .WithMany()
            .HasForeignKey(agentLanguage => agentLanguage.LanguageId);
    }
}
