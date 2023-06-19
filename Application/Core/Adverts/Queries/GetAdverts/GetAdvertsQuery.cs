﻿using Application.Abstraction.Messaging;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;

namespace Application.Core.Adverts.Queries.GetAdverts;
public sealed record GetAdvertsQuery(AdvertParameters AdvertParameters) : IQuery<Pagination<MinimalInformationsAboutAdvertDto>>;