using Application.Abstraction.Messaging;
using MediatR;
using Shared.DataTransferObjects.User;

namespace Application.Core.Users.Commands.UpdateUser;
public sealed record UpdateUserCommand(UpdateUserDto UpdateUser, string Email) : ICommand<Unit>;