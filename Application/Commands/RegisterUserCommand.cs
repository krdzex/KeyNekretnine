using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects.Auth;

namespace Application.Commands;
public sealed record RegisterUserCommand(UserForRegistrationDto RegistrationUser) : IRequest<IdentityResult>;
