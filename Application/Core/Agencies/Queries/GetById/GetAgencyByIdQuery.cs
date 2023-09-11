using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetById;
public sealed record GetAgencyByIdQuery(Guid AgencyId) : IQuery<AgencyResponse>;