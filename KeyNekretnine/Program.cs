using Application.Behaviors;
using AspNetCoreRateLimit;
using CompanyEmployees.Extensions;
using Contracts;
using FluentValidation;
using KeyNekretnine.Attributes;
using KeyNekretnine.Extensions;
using KeyNekretnine.Presentation.ActionFilters;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

//LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),
//"/nlog.config"));
builder.Services.ConfigureCors();

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

builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();
builder.Services.AddMediatR(typeof(Application.AssemblyReference).Assembly);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(Application.AssemblyReference).Assembly);
builder.Services.ConfigureRedis();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureChannel();
builder.Services.ConfigureBackgroundWorker();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT();
builder.Services.SetupSwagger();
builder.Services.AddScoped<BanUserChack>();
builder.Services.AddScoped<OwnerAdvertChack>();
builder.Services.AddAuthentication();
builder.Services.ConfigurePgsqlContext();
builder.Services.AddControllers();
builder.Services.ConfigureDapperContext();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureRateLimitingOptions();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseCors("Dev");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DisplayRequestDuration();
});

app.UseIpRateLimiting();

app.UseHttpsRedirection();

app.UseResponseCaching();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
