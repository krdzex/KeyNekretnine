using Application.Abstraction.Messaging;
using MediatR;
using Shared.DataTransferObjects.Auth;

namespace Application.Core.Auth.Commands.UserRegistration;
public sealed record UserRegistrationCommand(UserForRegistrationDto RegistrationUser) : ICommand<Unit>;
