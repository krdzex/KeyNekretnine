using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;

namespace KeyNekretnine.Application.Core.Users.Commands.UnbanUser;
public sealed record UnbanUserCommand(string Email) : ICommand<Unit>;