using Contracts;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IO;
using Repository.RawQuery;
using System.Data;

namespace Repository.Repositories;
internal class TemporeryImageDataRepository : ITemporeryImageDataRepository
{

    private readonly DapperContext _dapperContext;
    private static readonly RecyclableMemoryStreamManager manager = new RecyclableMemoryStreamManager(128 * 1024, 13 * 1024 * 1024);
    public TemporeryImageDataRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<IEnumerable<byte[]>> Get(int advertId, bool isCover)
    {
        using (var connection = _dapperContext.CreateConnection())
        {
            var query = TemporeryImageDataQuery.GetAdvertPurposesQuery;

            var param = new DynamicParameters();
            param.Add("@advert_id", advertId, DbType.Int32);
            param.Add("@is_cover", isCover, DbType.Boolean);

            var imagesData = await connection.QueryAsync<byte[]>(query, param);
            return imagesData;
        }
    }

    public async Task DeleteAll(int advertId)
    {
        using (var connection = _dapperContext.CreateConnection())
        {
            var query = TemporeryImageDataQuery.DeleteTemporeryImageDataQuery;

            var param = new DynamicParameters();
            param.Add("@advert_id", advertId);

            await connection.ExecuteAsync(query, param);
        }
    }

    public async Task Insert(IFormFile image, int advertId, bool is_cover)
    {

        using (var memoryStream = manager.GetStream("test"))
        {
            await image.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            var buffer = new byte[memoryStream.Length];
            await memoryStream.ReadAsync(buffer, 0, buffer.Length);

            using (var connection = _dapperContext.CreateConnection())
            {
                var query = TemporeryImageDataQuery.InsertTemporeryImageDataQuery;

                var param = new DynamicParameters();
                param.Add("@image_data", buffer);
                param.Add("@advert_id", advertId, DbType.Int64);
                param.Add("@is_cover", is_cover, DbType.Boolean);

                await connection.ExecuteAsync(query, param);
            }
        }
    }
}
