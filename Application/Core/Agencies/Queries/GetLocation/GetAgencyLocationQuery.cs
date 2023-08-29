using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Agencies.Queries.GetLocation;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyLocation;
public sealed record GetAgencyLocationQuery(int AgencyId) : IQuery<AgencyLocationResponse>;
