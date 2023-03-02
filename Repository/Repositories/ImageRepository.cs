using Contracts;
using Dapper;
using Repository.RawQuery;
using System.Data;

namespace Repository.Repositories;

internal class ImageRepository : IImageRepository
{
    private readonly DapperContext _dapperContext;
    public ImageRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task InsertImages(IEnumerable<string> urls, int advertId, CancellationToken cancellationToken)
    {
        var query = ImageQuery.InsertImageQuery;

        foreach (var url in urls)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                var param = new DynamicParameters();
                param.Add("@url", url, DbType.String);
                param.Add("@advertId", advertId, DbType.Int32);

                var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

                await connection.ExecuteAsync(cmd);
            }
        }
    }
}