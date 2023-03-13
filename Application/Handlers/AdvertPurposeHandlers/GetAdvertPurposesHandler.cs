﻿using Application.Queries.AdvertPurposesQueries;
using Contracts;
using MediatR;
using Shared.DataTransferObjects.AdvertPurpose;

namespace Application.Handlers.AdvertPurposeHandlers;

internal sealed class GetAdvertPurposesHandler : IRequestHandler<GetAdvertPurposesQuery, IEnumerable<AdvertPurposeDto>>
{
    private readonly IRepositoryManager _repository;

    public GetAdvertPurposesHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<AdvertPurposeDto>> Handle(GetAdvertPurposesQuery request, CancellationToken cancellationToken)
    {
        var advertPurposes = await _repository.AdvertPurpose.GetAdvertPurposes(cancellationToken);

        return advertPurposes;
    }
}

