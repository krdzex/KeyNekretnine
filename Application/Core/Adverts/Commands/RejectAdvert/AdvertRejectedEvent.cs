using MediatR;

namespace Application.Core.Adverts.Commands.RejectAdvert
{
    public sealed record AdvertRejectedEvent(string Email, int AdvertId) : INotification;

}
