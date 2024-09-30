using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Image;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.TemporeryImageDatas;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Data;

namespace KeyNekretnine.Infrastructure.BackgroundJobs.ImageUploader;
[DisallowConcurrentExecution]
internal sealed class ProcessImageUploadJob : IJob
{
    private readonly ILogger<ProcessImageUploadJob> _logger;
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly IImageService _imageService;


    public ProcessImageUploadJob(
        IImageService imageService,
        ILogger<ProcessImageUploadJob> logger,
        ISqlConnectionFactory sqlConnectionFactory)
    {
        _imageService = imageService;
        _logger = logger;
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Beginning to upload images");

        using var connection = _sqlConnectionFactory.CreateConnection();
        using var transaction = connection.BeginTransaction();

        var advertId = await GetAdvertToUploadImages(connection, transaction);

        if (advertId is not null)
        {
            var imagesToUpload = await GetImagesForAdvert(connection, advertId.Value, transaction);

            foreach (var image in imagesToUpload)
            {
                var imageUploadUrl = await _imageService.UploadImageOnCloudinaryUsingDb(image.ImageData, advertId.Value.ToString());

                if (image.IsCover)
                {
                    await AddCoverImgForAdvert(connection, advertId.Value, imageUploadUrl, transaction);
                }
                else
                {
                    await AddImgForAdvert(connection, advertId.Value, imageUploadUrl, transaction);
                }
            }

            await DeleteTemporaryImageForAdvert(connection, advertId.Value, transaction);

            await UpdateAdvertStatus(connection, advertId.Value, transaction);
        }

        transaction.Commit();

        _logger.LogInformation("Completed uploading images");
    }

    private async Task<Guid?> GetAdvertToUploadImages(
        IDbConnection connection,
        IDbTransaction transaction)
    {
        var sql = $"""                
            SELECT advert_id
            FROM temporery_images_data
            WHERE advert_id IS NOT NULL
            ORDER BY created_date
            """;

        var advertId = await connection.QueryFirstOrDefaultAsync<Guid>(sql, transaction: transaction);

        return advertId;
    }

    private async Task<IReadOnlyList<TemporeryImageData>> GetImagesForAdvert(
        IDbConnection connection,
        Guid advertId,
        IDbTransaction transaction)
    {
        var sql = $"""                
            SELECT 
                id,
                advert_id AS advertId,
                image_data AS imageData,
                is_cover AS isCover,
                created_date AS createdDate
            FROM temporery_images_data 
            WHERE advert_id = @advertId
            ORDER BY createdDate
            """;

        var imagesToUpload = await connection.QueryAsync<TemporeryImageData>(sql, new { advertId }, transaction: transaction);

        return imagesToUpload.ToList();
    }

    private async Task AddCoverImgForAdvert(
        IDbConnection connection,
        Guid advertId,
        string imgUrl,
        IDbTransaction transaction)
    {
        var sql = $"""                
            UPDATE adverts
            SET cover_image_url = @imgUrl 
            WHERE id = @advertId
            """;

        await connection.ExecuteAsync(sql, new { imgUrl, advertId }, transaction: transaction);
    }

    private async Task AddImgForAdvert(
        IDbConnection connection,
        Guid advertId,
        string imgUrl,
        IDbTransaction transaction)
    {
        var sql = $"""                
            INSERT INTO images (url,advert_id)
            VALUES (@imgUrl,@advertId)
            """;

        await connection.ExecuteAsync(sql, new { imgUrl, advertId }, transaction: transaction);
    }

    private async Task DeleteTemporaryImageForAdvert(
    IDbConnection connection,
    Guid advertId,
    IDbTransaction transaction)
    {
        var sql = $"""                
            DELETE FROM temporery_images_data
            WHERE advert_id = @advertId
            """;

        await connection.ExecuteAsync(sql, new { advertId }, transaction: transaction);
    }

    private async Task UpdateAdvertStatus(
        IDbConnection connection,
        Guid advertId,
        IDbTransaction transaction)
    {
        var pandingStatus = (int)AdvertStatus.Pending;

        var sql = $"""                
            UPDATE adverts
            SET status = @pandingStatus 
            WHERE id = @advertId
            """
        ;

        await connection.ExecuteAsync(sql, new { pandingStatus, advertId }, transaction: transaction);
    }
}