using MediatR;

namespace Application.Core.Adverts.Commands.CreateAdvert;
public sealed record AdvertCreatedEvent(int AdvertId) : INotification;