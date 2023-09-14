using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;

namespace KeyNekretnine.Application.Core.Users.Commands.MultipleUsersUnban;
public sealed record MultipleUsersUnbanCommand(IEnumerable<string> Emails) : ICommand<Unit>;