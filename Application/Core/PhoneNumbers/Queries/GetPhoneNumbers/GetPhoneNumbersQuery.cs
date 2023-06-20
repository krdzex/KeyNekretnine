using Application.Abstraction.Messaging;
using Shared.DataTransferObjects.PhoneNumber;

namespace Application.Core.PhoneNumbers.Queries.GetPhoneNumbers;
public sealed record GetPhoneNumbersQuery() : IQuery<List<PhoneNumberDto>>;