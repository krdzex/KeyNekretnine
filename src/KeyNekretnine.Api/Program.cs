//builder.Services
//    .AddCaching()
//    .AddPresentation()
//    .AddCustomAttributes()
//    .AddDatabase()
//    .AddAuthorizationAndAuthentication()
//    .AddRateLimiting()
//    .AddBackgroundTasks()
//    .AddManagers()
//    .AddHttpConfiguration()
//    .AddServicesRegistration();

using KeyNekretnine.Api.Extensions;
using KeyNekretnine.Application;
using KeyNekretnine.Infrastructure;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
            options.AddPolicy("Dev", builder =>
            {
                builder
                .WithOrigins(
                    "https://keynekretnine-dev.vercel.app",
                    "https://keynekretnine-git-http-only-voi99.vercel.app",
                    "http://localhost:3000",
                    "https://localhost:4200",
                    "https://key-nekretnine-admin.vercel.app"
                    )
                .WithExposedHeaders("set-cookie")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            }));


builder.Services.AddCors(options =>
    options.AddPolicy("Dev", builder =>
    {
        builder
        .WithOrigins(
            "https://keynekretnine-dev.vercel.app",
            "https://keynekretnine-git-http-only-voi99.vercel.app",
            "http://localhost:3000",
            "https://localhost:4200",
            "https://key-nekretnine-admin.vercel.app"
            )
        .WithExposedHeaders("set-cookie")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    }));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy("fixed-by-ip", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1)
            }));
});

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseCors("Dev");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DisplayRequestDuration();
});

app.UseCustomExceptionHandler();

app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseResponseCaching();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
