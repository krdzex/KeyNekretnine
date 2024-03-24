using Microsoft.Extensions.Options;
using Quartz;

namespace KeyNekretnine.Infrastructure.BackgroundJobs.ImageUploader;
public class ProcessImageUploadJobSetup : IConfigureOptions<QuartzOptions>
{
    private readonly ImageUploadOptions _imageUploadOptions;

    public ProcessImageUploadJobSetup(IOptions<ImageUploadOptions> imageUploadOptions)
    {
        _imageUploadOptions = imageUploadOptions.Value;
    }

    public void Configure(QuartzOptions options)
    {
        const string jobName = nameof(ProcessImageUploadJob);

        options
            .AddJob<ProcessImageUploadJob>(configure => configure.WithIdentity(jobName))
            .AddTrigger(configure =>
                configure
                    .ForJob(jobName)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInSeconds(_imageUploadOptions.IntervalInSeconds).RepeatForever()));
    }
}