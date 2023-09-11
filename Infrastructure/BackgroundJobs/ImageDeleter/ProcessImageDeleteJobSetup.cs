using Microsoft.Extensions.Options;
using Quartz;

namespace KeyNekretnine.Infrastructure.BackgroundJobs.ImageDeleter;
public class ProcessImageDeleteJobSetup : IConfigureOptions<QuartzOptions>
{
    private readonly ImageDeleteOptions _imageDeleteOptions;

    public ProcessImageDeleteJobSetup(IOptions<ImageDeleteOptions> imageDeleteOptions)
    {
        _imageDeleteOptions = imageDeleteOptions.Value;
    }

    public void Configure(QuartzOptions options)
    {
        const string jobName = nameof(ProcessImageDeleteJob);

        options
            .AddJob<ProcessImageDeleteJob>(configure => configure.WithIdentity(jobName))
            .AddTrigger(configure =>
                configure
                    .ForJob(jobName)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInSeconds(_imageDeleteOptions.IntervalInSeconds).RepeatForever()));
    }
}