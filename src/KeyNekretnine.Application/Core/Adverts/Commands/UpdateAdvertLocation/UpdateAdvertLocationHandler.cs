﻿using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.Agents;
using KeyNekretnine.Domain.ValueObjects;

namespace KeyNekretnine.Application.Core.Adverts.Commands.UpdateAdvertLocation;
internal sealed class UpdateAdvertLocationHandler : ICommandHandler<UpdateAdvertLocationCommand>
{
    private readonly IAdvertRepository _advertRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IAgentRepository _agentRepository;

    public UpdateAdvertLocationHandler(
        IAdvertRepository advertRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider,
        IAgentRepository agentRepository)
    {
        _advertRepository = advertRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _agentRepository = agentRepository;
    }

    public async Task<Result> Handle(UpdateAdvertLocationCommand request, CancellationToken cancellationToken)
    {
        var advert = await _advertRepository.GetByReferenceIdAsync(request.ReferenceId, cancellationToken);

        if (advert is null)
        {
            return Result.Failure(AdvertErrors.NotFound);
        }

        var canUserEditResult = await advert.CanCurrentUserUpdate(
            request.IsAgency,
            request.UserId,
            _agentRepository);

        if (canUserEditResult.IsFailure)
        {
            return canUserEditResult;
        }

        var location = Location.Create(request.Address, request.Latitude, request.Longitude);

        advert.UpdateLocation(
            _dateTimeProvider.Now,
            location,
            request.NeighborhoodId ?? advert.NeighborhoodId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}