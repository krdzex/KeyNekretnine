using Contracts;
using Dapper;
using Repository.RawQuery;
using Shared.DataTransferObjects.Agency;
using Shared.DataTransferObjects.PhoneNumber;
using System.Data;

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


    public async Task<string> MakeNumber(CreateNumberDto numberDto, CancellationToken cancellationToken)
    {
        var query = PhoneNumberQuery.GetCountryPhone;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("countryId", numberDto.CountryId, DbType.Int32);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var result = await connection.QueryFirstOrDefaultAsync<string>(cmd);

            if (result is null)
            {
                return null;
            }

            var realNumber = $"+{result} - {numberDto.Number}";

            return realNumber;
        }
    }
}