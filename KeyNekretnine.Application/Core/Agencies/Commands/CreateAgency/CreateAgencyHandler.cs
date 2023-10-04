using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Exceptions;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Agencies;
using KeyNekretnine.Domain.Shared;
using KeyNekretnine.Domain.Users;
using Microsoft.AspNetCore.Identity;
using System.Transactions;

namespace KeyNekretnine.Application.Core.Agencies.Commands.CreateAgency;
internal sealed class CreateAgencyHandler : ICommandHandler<CreateAgencyCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IAgencyRepository _agencyRepository;
    public CreateAgencyHandler(
        UserManager<User> userManager,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider,
        IAgencyRepository agencyRepository)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _agencyRepository = agencyRepository;
    }

    public async Task<Result> Handle(CreateAgencyCommand request, CancellationToken cancellationToken)
    {
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var user = User.Create(
            new FirstName(request.FirstName),
            new LastName(request.LastName),
            request.Email,
            request.UserName,
            _dateTimeProvider.Now,
            true);

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors
            .Select(authenticationFailure => new AuthenticationError(
                authenticationFailure.Code,
                authenticationFailure.Description))
            .ToList();

            throw new AuthenticationException(errors);
        }

        var agency = Agency.Create(
            new Name(request.AgencyName),
            user.Id,
            _dateTimeProvider.Now);

        _agencyRepository.Add(agency);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        transaction.Complete();

        return Result.Success();
    }
}