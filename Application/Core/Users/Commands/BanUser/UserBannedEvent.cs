using MediatR;

namespace KeyNekretnine.Application.Core.Users.Commands.BanUser;
public sealed record UserBannedEvent(string Email, int NoOfDays) : INotification;