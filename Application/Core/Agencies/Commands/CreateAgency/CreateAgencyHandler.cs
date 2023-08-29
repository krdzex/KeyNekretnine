using Contracts;
using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;
using Service.Contracts;
using Shared.Error;
using System.Transactions;

namespace KeyNekretnine.Application.Core.Agencies.Commands.CreateAgency;
internal sealed class CreateAgencyHandler : ICommandHandler<CreateAgencyCommand, Unit>
{
    private readonly IRepositoryManager _repository;
    private readonly IServiceManager _service;

    public CreateAgencyHandler(IRepositoryManager repository, IServiceManager service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task<Result<Unit>> Handle(CreateAgencyCommand request, CancellationToken cancellationToken)
    {
        using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var createUserResult = await _service.AuthenticationService.RegisterUser(request.CreateAgencyDto.AdminUser);

            if (!createUserResult.Item2.Succeeded)
            {
                var errors = createUserResult.Item2.Errors.Select(error => new Error(error.Code, error.Description)).ToArray();

                return MultipleErrorsResult<Unit>.WithErrors(errors);
            }

            await _repository.Agency.CreateAgency(request.CreateAgencyDto.Name, createUserResult.Item1, cancellationToken);

            transaction.Complete();
        }
        return Unit.Value;
    }
}