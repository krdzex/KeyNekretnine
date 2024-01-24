using System.Threading.RateLimiting;

namespace KeyNekretnine.Configuration;
public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
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

        services.AddCors(options =>
            options.AddPolicy("Dev", builder =>
            {
                builder
                .WithOrigins(
                    "https://testing-ui.keynekretnine.me",
                    "https://keynekretnine-dev.vercel.app",
                    "http://localhost:3000",
                    "https://localhost:4200",
                    "https://key-nekretnine-admin.vercel.app"
                    )
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            }));
        return services;
    }
}