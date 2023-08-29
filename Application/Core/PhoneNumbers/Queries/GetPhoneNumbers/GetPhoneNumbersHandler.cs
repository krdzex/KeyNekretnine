using Contracts;
using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.DataTransferObjects.PhoneNumber;
using Shared.Error;

namespace KeyNekretnine.Application.Core.PhoneNumbers.Queries.GetPhoneNumbers;
internal sealed class GetPhoneNumbersHandler : IQueryHandler<GetPhoneNumbersQuery, List<PhoneNumberDto>>
{
    private readonly IRepositoryManager _repository;

    public GetPhoneNumbersHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<PhoneNumberDto>>> Handle(GetPhoneNumbersQuery request, CancellationToken cancellationToken)
    {
        var phoneNumbers = await _repository.PhoneNumber.GetAll(cancellationToken);

        return phoneNumbers.ToList();
    }
}