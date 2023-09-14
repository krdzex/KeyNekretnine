using MediatR;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ApproveAdvert;
public sealed record AdvertApprovedEvent(string Email, int AdvertId) : INotification;