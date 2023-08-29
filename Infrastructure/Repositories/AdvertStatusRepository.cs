//using Contracts;
//using Dapper;
//using Repository.RawQuery;
//using Shared.DataTransferObjects.AdvertStatus;

//namespace KeyNekretnine.Infrastructure.Repositories;

//internal sealed class AdvertStatusRepository : IAdvertStatusRepository
//{
//    private readonly DapperContext _dapperContext;
//    public AdvertStatusRepository(DapperContext dapperContext)
//    {
//        _dapperContext = dapperContext;
//    }

//    public async Task<IEnumerable<AdvertStatusDto>> GetAdvertsStatuses(CancellationToken cancellationToken)
//    {
//        var query = AdvertStatusQuery.AllAdvertStatuses;

//        using (var connection = _dapperContext.CreateConnection())
//        {
//            var cmd = new CommandDefinition(query, cancellationToken: cancellationToken);

//            var advertStatuses = await connection.QueryAsync<AdvertStatusDto>(cmd);

//            return advertStatuses;
//        }
//    }
//};
