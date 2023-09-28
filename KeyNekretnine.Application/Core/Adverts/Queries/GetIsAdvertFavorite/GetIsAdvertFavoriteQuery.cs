﻿using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetIsAdvertFavorite;
public sealed record GetIsAdvertFavoriteQuery(string ReferenceId, string UserId) : IQuery<bool>;