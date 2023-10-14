namespace KeyNekretnine.Infrastructure.BackgroundJobs.Outbox;
public sealed class OutboxMessage
{
    public OutboxMessage(Guid id, DateTime occurredOnTime, string type, string content)
    {
        Id = id;
        OccurredOnTime = occurredOnTime;
        Content = content;
        Type = type;
    }

    public Guid Id { get; private set; }

    public DateTime OccurredOnTime { get; private set; }

    public string Type { get; private set; }

    public string Content { get; private set; }

    public DateTime? ProcessedOnTime { get; private set; }

    public string? Error { get; private set; }
}
