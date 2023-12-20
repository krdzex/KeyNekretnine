namespace KeyNekretnine.Domain.ValueObjects;
public record TimeRange
{
    private TimeRange()
    {
    }

    public TimeOnly? Start { get; init; }

    public TimeOnly? End { get; init; }


    public static TimeRange Create(TimeOnly? start, TimeOnly? end)
    {
        if (start > end)
        {
            throw new ApplicationException("End time precedes start time");
        }

        return new TimeRange
        {
            Start = start,
            End = end
        };
    }
}