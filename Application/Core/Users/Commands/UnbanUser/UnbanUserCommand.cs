using Application.Abstraction.Messaging;
using MediatR;

namespace Application.Core.Users.Commands.UnbanUser;
public sealed record UnbanUserCommand(string Email) : ICommand<Unit>;

