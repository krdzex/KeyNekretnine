using Dapper;
using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Image;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using System.Data;

namespace KeyNekretnine.Infrastructure.BackgroundJobs.ImageDeleter;
[DisallowConcurrentExecution]
internal sealed class ProcessImageDeleteJob : IJob
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ImageDeleteOptions _imageDeleteOptions;
    private readonly ILogger<ProcessImageDeleteJob> _logger;
    private readonly IImageService _imageService;

    public ProcessImageDeleteJob(
        ISqlConnectionFactory sqlConnectionFactory,
        IDateTimeProvider dateTimeProvider,
        IOptions<ImageDeleteOptions> imageDeleteOptions,
        ILogger<ProcessImageDeleteJob> logger,
        IImageService imageService)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _dateTimeProvider = dateTimeProvider;
        _logger = logger;
        _imageDeleteOptions = imageDeleteOptions.Value;
        _imageService = imageService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Beginning to process delete images");

        using var connection = _sqlConnectionFactory.CreateConnection();

        var imagesToDelete = await GetImagesToDeleteAsync(connection);

        foreach (var imageToDelete in imagesToDelete)
        {
            var imagePublicId = _imageService.GetPublicIdFromUrl(imageToDelete.ImageUrl);

            var result = await _imageService.DeleteImageFromCloudinary(imagePublicId);

            await UpdateImageToDeleteAsync(connection, imageToDelete, result);
        }

        _logger.LogInformation("Completed processing delete images");
    }

    private async Task<IReadOnlyList<ImageToDeleteResponse>> GetImagesToDeleteAsync(
        IDbConnection connection)
    {
        var sql = $"""                
            SELECT 
                id,
                image_url AS imageUrl
            FROM images_to_delete 
            WHERE deleted_on_time IS NULL
            ORDER BY added_on_time
            LIMIT {_imageDeleteOptions.BatchSize}
            FOR UPDATE
            """;

        var imagesToDelete = await connection.QueryAsync<ImageToDeleteResponse>(sql);

        return imagesToDelete.ToList();
    }

    private async Task UpdateImageToDeleteAsync(
        IDbConnection connection,
        ImageToDeleteResponse outboxMessage,
        string errorMassage)
    {
        const string sql = @"
            UPDATE images_to_delete
            SET deleted_on_time = @DeletedOnTime,
                error = @Error
            WHERE id = @Id";

        await connection.ExecuteAsync(
            sql,
            new
            {
                outboxMessage.Id,
                DeletedOnTime = _dateTimeProvider.Now,
                Error = errorMassage
            });
    }

    internal sealed record ImageToDeleteResponse(Guid Id, string ImageUrl);
}