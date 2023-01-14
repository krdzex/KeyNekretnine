using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;

namespace Application.Handlers;
internal sealed class RegisterUserHandler : IRequestHandler<RegisterUserCommand, IdentityResult>
{
    private readonly IServiceManager _service;

    public RegisterUserHandler(IServiceManager service)
    {
        _service = service;
    }

    public async Task<IdentityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _service.AuthenticationService.RegisterUser(request.RegistrationUser);

        return result;
    }
}
