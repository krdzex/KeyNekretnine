using KeyNekretnine.Application.Abstraction.Clock;

namespace KeyNekretnine.Infrastructure.Clock;
internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
}