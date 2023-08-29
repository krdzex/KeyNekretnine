using MediatR;

namespace KeyNekretnine.Application.Core.Adverts.Commands.CreateAdvert;
public sealed record AdvertCreatedEvent(int AdvertId) : INotification;