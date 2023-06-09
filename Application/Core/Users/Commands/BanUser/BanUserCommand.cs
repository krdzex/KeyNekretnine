using Application.Abstraction.Messaging;
using MediatR;

namespace Application.Core.Users.Notifications.BanUser;
public sealed record BanUserCommand(string Email, int NoOfDays) : ICommand<Unit>;