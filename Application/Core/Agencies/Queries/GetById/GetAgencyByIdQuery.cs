using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyById;
public sealed record GetAgencyByIdQuery(int AgencyId) : IQuery<AgencyResponse>;