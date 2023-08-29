using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.DataTransferObjects.PhoneNumber;

namespace KeyNekretnine.Application.Core.PhoneNumbers.Queries.GetPhoneNumbers;
public sealed record GetPhoneNumbersQuery() : IQuery<List<PhoneNumberDto>>;