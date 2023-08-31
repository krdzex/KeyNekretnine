using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.PhoneNumbers.Queries.Get;

namespace KeyNekretnine.Application.Core.PhoneNumbers.Queries.GetPhoneNumbers;
public sealed record GetPhoneNumbersQuery() : IQuery<IReadOnlyList<PhoneNumberResponse>>;