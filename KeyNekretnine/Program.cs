using Application.Behaviors;
using CompanyEmployees.Extensions;
using Contracts;
using FluentValidation;
using KeyNekretnine.Attributes;
using KeyNekretnine.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),
//"/nlog.config"));

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters()
  .AddApplicationPart(typeof(KeyNekretnine.Presentation.AssemblyReference).Assembly);

builder.Services.Configure<FormOptions>(x =>
{
    x.ValueLengthLimit = int.MaxValue;
    x.MultipartBodyLengthLimit = int.MaxValue;
    x.MultipartHeadersLengthLimit = int.MaxValue;
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = int.MaxValue;
});

builder.Services.ConfigureCors();
builder.Services.AddMediatR(typeof(Application.AssemblyReference).Assembly);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(Application.AssemblyReference).Assembly);
builder.Services.AddStackExchangeRedisCache(options =>
{
    var redisUrl = Environment.GetEnvironmentVariable("REDIS_URL");
    var tokens = redisUrl.Split(':', '@');

    var configurationOptions = ConfigurationOptions.Parse(string.Format("{0}:{1},password={2}", tokens[3], tokens[4], tokens[2]));
    configurationOptions.ConnectTimeout = 5000;
    configurationOptions.ConnectRetry = 5;
    configurationOptions.SyncTimeout = 5000;
    configurationOptions.AbortOnConnectFail = false;
    configurationOptions.Ssl = true;

    options.ConfigurationOptions = configurationOptions;
});

builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureChannel();
builder.Services.ConfigureBackgroundWorker();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT();
builder.Services.SetupSwagger();
builder.Services.AddScoped<BanUserChack>();
builder.Services.AddAuthentication();
builder.Services.ConfigurePgsqlContext();
builder.Services.AddControllers();
builder.Services.ConfigureDapperContext();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DisplayRequestDuration();
});

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseResponseCaching();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
