namespace Entities.Exceptions;

public sealed class AdvertAlreadyReportedException : BadRequestException
{
    public AdvertAlreadyReportedException()
        : base("You cant report advert again for same reason")
    {
    }
}
