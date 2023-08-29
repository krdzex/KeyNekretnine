namespace KeyNekretnine.Application.Abstraction.Clock;
public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}