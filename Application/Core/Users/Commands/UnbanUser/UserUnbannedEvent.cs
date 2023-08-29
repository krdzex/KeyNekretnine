using MediatR;

namespace KeyNekretnine.Application.Core.Users.Commands.UnbanUser;
public sealed record UserUnbannedEvent(string Email) : INotification;