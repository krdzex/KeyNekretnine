﻿using KeyNekretnine.Infrastructure.BackgroundJobs.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyNekretnine.Infrastructure.Configuration;
internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox_messages");

        builder.HasKey(outboxMessage => outboxMessage.Id);

        builder.Property(outboxMessage => outboxMessage.Content).HasColumnType("json");
    }
}
