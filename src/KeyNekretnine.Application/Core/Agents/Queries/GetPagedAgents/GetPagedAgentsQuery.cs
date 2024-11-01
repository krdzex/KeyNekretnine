﻿using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared.Pagination;

namespace KeyNekretnine.Application.Core.Agents.Queries.GetPagedAgents;
public sealed record GetPagedAgentsQuery(string OrderBy, int PageNumber, int PageSize) : IQuery<Pagination<PagedAgentResponse>>;