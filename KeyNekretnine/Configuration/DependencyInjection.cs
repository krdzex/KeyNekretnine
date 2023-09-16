using Application.Behaviors;
using AspNetCoreRateLimit;
using Contracts;
using Entities.Models;
using FluentValidation;
using KeyNekretnine.Attributes;
using KeyNekretnine.BackgroundWorkers;
using LoggerService;
using MediatR;
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

namespace KeyNekretnine.Configuration;
public static class DependencyInjection
{
    public static IServiceCollection AddCaching(this IServiceCollection services)
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

        services.AddMemoryCache();

        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(Application.AssemblyReference).Assembly);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(
            typeof(Application.AssemblyReference).Assembly,
            includeInternalTypes: true);

        return services;
    }

    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers(config =>
        {
            config.RespectBrowserAcceptHeader = true;
            config.ReturnHttpNotAcceptable = true;
        }).AddXmlDataContractSerializerFormatters()
          .AddApplicationPart(typeof(KeyNekretnine.Presentation.AssemblyReference).Assembly);

        services.AddSwaggerGen(s =>
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

        services.AddSwaggerGen();
        services.AddEndpointsApiExplorer();

        return services;
    }

    public static IServiceCollection AddAuthorizationAndAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddCookie(o =>
        {
            o.Cookie.Name = "X-Access-Token";
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
                ValidateAudience = false,
                ClockSkew = TimeSpan.FromSeconds(0)
            };
            o.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    context.Token = context.Request.Cookies["X-Access-Token"];
                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services)
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

        services.AddDbContext<RepositoryContext>(opts =>
        {
            opts.UseNpgsql(builder.ToString()).UseSnakeCaseNamingConvention();
        });

        services.AddIdentity<User, IdentityRole>(o =>
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

        services.AddSingleton<DapperContext>();

        return services;
    }

    public static IServiceCollection AddRateLimiting(this IServiceCollection services)
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

        services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

        return services;
    }

    public static IServiceCollection AddBackgroundTasks(this IServiceCollection services)
    {
        services.AddHostedService<ChannelBackgroundWorker>();

        return services;
    }
    public static IServiceCollection AddManagers(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager, ServiceManager>();
        services.AddScoped<IRepositoryManager, RepositoryManager>();

        return services;
    }
    public static IServiceCollection AddCustomAttributes(this IServiceCollection services)
    {
        services.AddScoped<BanUserChack>();
        services.AddScoped<OwnerAdvertChack>();

        return services;
    }

    public static IServiceCollection AddHttpConfiguration(this IServiceCollection services)
    {
        services.AddCors(options =>
            options.AddPolicy("Dev", builder =>
            {
                builder
                .WithOrigins(
                    "https://keynekretnine-dev.vercel.app",
                    "https://testing-ui.keynekretnine.me",
                    "http://localhost:3000",
                    "http://localhost:4200",
                    "https://key-nekretnine-admin.vercel.app"
                    )
                .WithExposedHeaders("set-cookie")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            }));

        services.AddHttpClient();
        services.AddHttpContextAccessor();

        return services;
    }

    public static IServiceCollection AddServicesRegistration(this IServiceCollection services)
    {
        services.AddSingleton<IProcessingChannel, ProcessingChannel>();
        services.AddSingleton<ILoggerManager, LoggerManager>();

        return services;
    }

    public static IServiceCollection AddMapings(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program));

        return services;
    }
}