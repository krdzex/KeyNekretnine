﻿using KeyNekretnine.Application.Core.Agents.Queries.GetById;
using KeyNekretnine.Application.Core.Shared;

namespace KeyNekretnine.Application.Core.Agents.Queries.Get;
public sealed class PaginationAgentResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string ImageUrl { get; set; }
    public int NumAdverts { get; set; }
    public ShortAgencyResponse Agency { get; set; }
    public SocialNetworkResponse SocialNetwork { get; set; } = new();
}
