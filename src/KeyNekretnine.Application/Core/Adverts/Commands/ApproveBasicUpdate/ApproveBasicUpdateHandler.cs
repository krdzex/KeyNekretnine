﻿using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Adverts.Queries.GetBasicUpdate;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.AdvertUpdates;
using Newtonsoft.Json;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ApproveBasicUpdate;
internal sealed class ApproveBasicUpdateHandler : ICommandHandler<ApproveBasicUpdateCommand>
{
    private readonly IAdvertUpdateRepository _advertUpdateRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ApproveBasicUpdateHandler(
        IAdvertUpdateRepository advertUpdateRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _advertUpdateRepository = advertUpdateRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result> Handle(ApproveBasicUpdateCommand request, CancellationToken cancellationToken)
    {
        var update = await _advertUpdateRepository.GetByIdWithAdvert(request.UpdateId, UpdateTypes.BasicInformations, cancellationToken);

        if (update is null)
        {
        }

        var newValues = JsonConvert.DeserializeObject<BasicAdvertInformations>(update.Content)!;

        var approveResult = update.ApproveBasicUpdate(
            _dateTimeProvider.Now,
            newValues.Price,
            newValues.FloorSpace,
            newValues.NoOfBedrooms,
            newValues.NoOfBathrooms,
            newValues.Type,
            newValues.Purpose,
            newValues.YearOfBuildingCreated,
            newValues.BuildingFloor,
            newValues.HasGarage,
            newValues.IsFurnished,
            newValues.HasWifi,
            newValues.HasElevator,
            newValues.IsUrgent,
            newValues.HasTerrace,
            newValues.IsUnderConstruction);

        if (approveResult.IsFailure)
        {

        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}