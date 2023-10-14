//using Contracts;
//using Dapper;
//using Entities.Exceptions;
//using Repository.RawQuery;
//using System.Data;

//namespace KeyNekretnine.Infrastructure.Repositories;

//internal class ImageRepository : IImageRepository
//{
//    private readonly DapperContext _dapperContext;
//    public ImageRepository(DapperContext dapperContext)
//    {
//        _dapperContext = dapperContext;
//    }

//    public async Task InsertImages(IEnumerable<string> urls, int advertId, CancellationToken cancellationToken)
//    {
//        var query = ImageQuery.InsertImageQuery;

//        foreach (var url in urls)
//        {
//            using (var connection = _dapperContext.CreateConnection())
//            {
//                var param = new DynamicParameters();
//                param.Add("@url", url, DbType.String);
//                param.Add("@advertId", advertId, DbType.Int32);

//                var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

//                await connection.ExecuteAsync(cmd);
//            }
//        }
//    }

//    public async Task<List<string>> DeleteImagesAndGetPublicIds(IEnumerable<string> urls, int advertId, CancellationToken cancellationToken)
//    {
//        var publicIdQuery = ImageQuery.GetPublicIdQuery;
//        var deleteQuery = ImageQuery.DeleteImageQuery;

//        var publicIds = new List<string>();
//        foreach (var url in urls)
//        {
//            using (var connection = _dapperContext.CreateConnection())
//            {
//                var publicIdParam = new DynamicParameters();
//                publicIdParam.Add("@url", url, DbType.String);
//                publicIdParam.Add("@advertId", advertId, DbType.Int32);

//                var publicIdCmd = new CommandDefinition(publicIdQuery, publicIdParam, cancellationToken: cancellationToken);

//                var publicId = await connection.QueryFirstOrDefaultAsync<string>(publicIdCmd);

//                if (string.IsNullOrEmpty(publicId))
//                {
//                    throw new ImageNotFoundException();
//                }

//                var deleteParam = new DynamicParameters();

//                deleteParam.Add("@url", url, DbType.String);

//                var deleteCmd = new CommandDefinition(deleteQuery, deleteParam, cancellationToken: cancellationToken);

//                await connection.ExecuteAsync(deleteCmd);

//                publicIds.Add(publicId);
//            }
//        }
//        return publicIds;
//    }

//    public async Task<int> GetNumberOfImages(int advertId, CancellationToken cancellationToken)
//    {
//        var query = ImageQuery.GetNumberOfImagesQuery;
//        using (var connection = _dapperContext.CreateConnection())
//        {
//            var param = new DynamicParameters();
//            param.Add("@advertId", advertId, DbType.Int32);

//            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

//            var noOfImages = await connection.QueryFirstOrDefaultAsync<int>(cmd);

//            return noOfImages;
//        }
//    }
//}