using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAvgPricePerSqftInRadius;

public sealed record GetAvgPricePerSqftInRadiusQuery(string ReferenceId, double? RadiusInKm) : IQuery<AvgPricePerSqftResponse>;