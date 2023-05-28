using AspNetCoreRateLimit;
using Contracts;
using Entities.Models;
using KeyNekretnine.BackgroundWorkers;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using Repository;
using Service;
using Service.Contracts;
using StackExchange.Redis;
using System.Text;

namespace CompanyEmployees.Extensions;
public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
            options.AddPolicy("Dev", builder =>
            {
                builder
                .WithOrigins("https://keynekretnine-dev.vercel.app", "https://keynekretnine-git-http-only-voi99.vercel.app", "http://localhost:3000", "https://localhost:4200")
                .WithExposedHeaders("set-cookie", "xcvuhgi-awtzpdsa", "mjoifp-fo8ahsj")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            })
    );
    //services.AddCors(options =>
    //{
    //    options.AddPolicy("CorsPolicy", builder =>
    //    builder.AllowAnyMethod()
    //    .AllowAnyHeader()
    //    .SetIsOriginAllowed(origin => true)
    //    .AllowCredentials());

    //    //options.AddPolicy("ProdPolicy", builder =>
    //    //{
    //    //    builder.WithOrigins("https://keynekretnine-dev.vercel.app");
    //    //    builder.AllowAnyHeader();
    //    //    builder.WithMethods("PUT", "POST", "DELETE", "GET");
    //    //});
    //});

    public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IServiceManager, ServiceManager>();

    public static void ConfigureDapperContext(this IServiceCollection services) =>
        services.AddSingleton<DapperContext>();

    public static void ConfigureChannel(this IServiceCollection services) =>
        services.AddSingleton<IProcessingChannel, ProcessingChannel>();

    public static void ConfigureBackgroundWorker(this IServiceCollection services) =>
        services.AddHostedService<ChannelBackgroundWorker>();

    public static void ConfigurePgsqlContext(this IServiceCollection services)
    {
        services.AddDbContext<RepositoryContext>(opts =>
        {
            opts.UseNpgsql(GetConnectionString()).UseSnakeCaseNamingConvention();
        });
    }

    public static void SetupSwagger(this IServiceCollection service)
    {
        service.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo { Title = "Agencija108", Version = "v1" });
            s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Place to add JWT with bearer",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                        },
                        new List<string>()
                    }
                });
        });
    }

    private static string GetConnectionString()
    {

        var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        var databaseUri = new Uri(databaseUrl);
        var userInfo = databaseUri.UserInfo.Split(':');

        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = databaseUri.Host,
            Port = databaseUri.Port,
            Username = userInfo[0],
            Password = userInfo[1],
            Database = databaseUri.LocalPath.TrimStart('/')
        };

        return builder.ToString();
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentity<User, IdentityRole>(o =>
        {
            o.Password.RequireDigit = true;
            o.Password.RequireLowercase = true;
            o.Password.RequireUppercase = true;
            o.User.RequireUniqueEmail = true;
            o.SignIn.RequireConfirmedEmail = true;
        })
        .AddEntityFrameworkStores<RepositoryContext>()
        .AddTokenProvider("KeyNekretnineAPI", typeof(DataProtectorTokenProvider<User>))
        .AddDefaultTokenProviders();
    }

    public static void ConfigureJWT(this IServiceCollection services)
    {
        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddCookie(o =>
        {
            o.Cookie.Name = "xcvuhgi-awtzpdsa";
        }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY"))),
                    ValidateAudience = false
                };
                o.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["xcvuhgi-awtzpdsa"];
                        return Task.CompletedTask;
                    }
                };
            });
    }

    public static void ConfigureRedis(this IServiceCollection services)
    {
        services.AddStackExchangeRedisCache(options =>
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
    }

    public static void ConfigureRateLimitingOptions(this IServiceCollection services)
    {
        var rateLimitRules = new List<RateLimitRule>
        {
            new RateLimitRule
            {
                Endpoint = "*",
                Limit = 100,
                Period = "1m"
            }
        };

        services.Configure<IpRateLimitOptions>(opt =>
        {
            opt.GeneralRules = rateLimitRules;
        });

        services.AddSingleton<IRateLimitCounterStore,
        MemoryCacheRateLimitCounterStore>();
        services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
    }
}