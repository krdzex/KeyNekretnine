using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetById;
public sealed record GetAgencyByIdQuery(int AgencyId) : IQuery<AgencyResponse>;