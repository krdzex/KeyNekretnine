using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Threading.RateLimiting;

namespace KeyNekretnine.Api.Configuration;
public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.AddPolicy("high-rating", httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 300,
                        Window = TimeSpan.FromMinutes(1)
                    }));

            options.AddPolicy("low-rating", httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 5,
                        Window = TimeSpan.FromHours(1)
                    }));

            options.AddPolicy("medium-rating", httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 5,
                        Window = TimeSpan.FromHours(1)
                    }));
        });

        var info = new OpenApiInfo()
        {
            Title = "Key Nekretnine API",
            Version = "v1",
            Description = "Official API of Key Nekretnine",
            Contact = new OpenApiContact()
            {
                Name = "Key Nekretine",
                Email = "your@email.com",
            }

        };

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", info);

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });


        services.AddCors(options =>
            options.AddPolicy("Dev", builder =>
            {
                builder
                .WithOrigins(
                    "http://localhost:3000",
                    "https://localhost:4200",
                    "https://key-nekretnine-admin.vercel.app",
                    "https://key-nekretnine.vercel.app"
                    )
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            }));
        return services;
    }
}