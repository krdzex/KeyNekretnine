using Application.Abstraction.Messaging;
using Contracts;
using Entities.DomainErrors;
using MediatR;
using Service.Contracts;
using Shared.Error;
using System.Web;
namespace Application.Core.Email.Commands.SendConfirmEmail;
internal sealed class SendConfirmEmailHandler : ICommandHandler<SendConfirmEmailCommand, Unit>
{
    private readonly IServiceManager _serviceManager;
    private readonly IRepositoryManager _repositoryManager;
    public SendConfirmEmailHandler(IServiceManager serviceManager, IRepositoryManager repositoryManager)
    {
        _serviceManager = serviceManager;
        _repositoryManager = repositoryManager;
    }
    public async Task<Result<Unit>> Handle(SendConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _repositoryManager.User.GetUserByEmail(request.Email);

        var token = await _repositoryManager.User.GenerateEmailConfirmationToken(user);

        var encodedToken = HttpUtility.UrlEncode(token);

        var result = await _serviceManager.EmailService.SendEmailConfrim(request.Email, encodedToken, cancellationToken);

        if (!result)
        {
            return Result.Failure<Unit>(DomainErrors.EmailService.EmailSendFailed);
        }

        return Unit.Value;
    }
}

