﻿//using Contracts;
//using Dapper;
//using Repository.RawQuery;
//using Shared.DataTransferObjects.Neighborhood;

//namespace KeyNekretnine.Infrastructure.Repositories;
//internal sealed class NeighborhoodRepository : INeighborhoodRepository
//{
//    private readonly DapperContext _dapperContext;
//    public NeighborhoodRepository(DapperContext dapperContext)
//    {
//        _dapperContext = dapperContext;
//    }

//    public async Task<IEnumerable<NeighborhoodDto>> GetNeighborhoods(int id, CancellationToken token)
//    {
//        var query = NeighborhoodQuery.NeighborhoodForCity;

//        using (var connection = _dapperContext.CreateConnection())
//        {
//            var cmd = new CommandDefinition(query, new { id }, cancellationToken: token);

//            var neighborhoods = await connection.QueryAsync<NeighborhoodDto>(cmd);

//            return neighborhoods;
//        }
//    }
//}