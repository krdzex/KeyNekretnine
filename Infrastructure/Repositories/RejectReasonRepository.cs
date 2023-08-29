//using Contracts;
//using Dapper;
//using Repository.RawQuery;
//using Shared.DataTransferObjects.RejectReason;

//namespace KeyNekretnine.Infrastructure.Repositories;
//internal class RejectReasonRepository : IRejectReasonRepository
//{
//    private readonly DapperContext _dapperContext;
//    public RejectReasonRepository(DapperContext dapperContext)
//    {
//        _dapperContext = dapperContext;
//    }

//    public async Task<IEnumerable<RejectReasonDto>> GetRejectReasons(CancellationToken cancellationToken)
//    {
//        var query = RejectReasonQuery.AllRejectReasonsQuery;

//        using (var connection = _dapperContext.CreateConnection())
//        {
//            var cmd = new CommandDefinition(query, cancellationToken: cancellationToken);

//            var rejectReasons = await connection.QueryAsync<RejectReasonDto>(cmd);

//            return rejectReasons;
//        }

//    }
//}
