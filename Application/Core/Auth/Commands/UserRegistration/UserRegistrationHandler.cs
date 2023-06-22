using Application.Abstraction.Messaging;
using MediatR;
using Service.Contracts;
using Shared.Error;

namespace Application.Core.Auth.Commands.UserRegistration;
internal sealed class UserRegistrationHandler : ICommandHandler<UserRegistrationCommand, Unit>
{
    private readonly IServiceManager _service;

    public UserRegistrationHandler(IServiceManager service)
    {
        _service = service;
    }

    public async Task<Result<Unit>> Handle(UserRegistrationCommand notification, CancellationToken cancellationToken)
    {
        var result = await _service.AuthenticationService.RegisterUser(notification.RegistrationUser);

        if (!result.Item2.Succeeded)
        {
            var errors = result.Item2.Errors.Select(error => new Error(error.Code, error.Description)).ToArray();

            return MultipleErrorsResult<Unit>.WithErrors(errors);
        }

        return Unit.Value;
    }
}