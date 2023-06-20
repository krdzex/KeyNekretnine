using Contracts;
using Dapper;
using Repository.RawQuery;
using Shared.DataTransferObjects.PhoneNumber;

namespace Repository.Repositories;
internal class PhoneNumberRepository : IPhoneNumberRepository
{
    private readonly DapperContext _dapperContext;
    public PhoneNumberRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<IEnumerable<PhoneNumberDto>> GetAll(CancellationToken cancellationToken)
    {
        var query = PhoneNumberQuery.AllPhoneNumbersQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var cmd = new CommandDefinition(query, cancellationToken: cancellationToken);

            var numbers = await connection.QueryAsync<PhoneNumberDto>(cmd);

            return numbers;
        }
    }
}