using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;
using Shared.DataTransferObjects.Auth;

namespace KeyNekretnine.Application.Core.Auth.Commands.UserRegistration;
public sealed record UserRegistrationCommand(UserForRegistrationDto RegistrationUser) : ICommand<Unit>;