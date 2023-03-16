using MediatR;
using Shared.DataTransferObjects.User;

namespace Application.Commands.UserCommands;
public sealed record UpdateUserCommand(UpdateUserDto UpdateUser) : IRequest;
