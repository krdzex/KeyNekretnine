using Application.Abstraction.Messaging;
using MediatR;

namespace Application.Core.Users.Commands.MultipleUsersUnban;
public sealed record MultipleUsersUnbanCommand(IEnumerable<string> Emails) : ICommand<Unit>;

