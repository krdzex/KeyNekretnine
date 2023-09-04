using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Application.Core.PhoneNumbers.Queries.Get;
internal sealed class GetPhoneNumbersHandler : IQueryHandler<GetPhoneNumbersQuery, IReadOnlyList<PhoneNumberResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetPhoneNumbersHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<PhoneNumberResponse>>> Handle(GetPhoneNumbersQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                id,
                code,
                label,
                phone
            FROM phone_numbers
            """;

        var cmd = new CommandDefinition(sql, cancellationToken: cancellationToken);

        var phoneNumbers = await connection.QueryAsync<PhoneNumberResponse>(cmd);

        return phoneNumbers.ToList();
    }
}