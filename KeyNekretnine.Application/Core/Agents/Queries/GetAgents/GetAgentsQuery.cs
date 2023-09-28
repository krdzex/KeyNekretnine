﻿using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared.Pagination;

namespace KeyNekretnine.Application.Core.Agents.Queries.GetAgents;
public sealed record GetAgentsQuery(string OrderBy, int PageNumber, int PageSize) : IQuery<Pagination<PaginationAgentResponse>>;