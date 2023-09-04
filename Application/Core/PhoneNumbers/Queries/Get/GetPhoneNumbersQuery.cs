using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.PhoneNumbers.Queries.Get;
public sealed record GetPhoneNumbersQuery() : IQuery<IReadOnlyList<PhoneNumberResponse>>;