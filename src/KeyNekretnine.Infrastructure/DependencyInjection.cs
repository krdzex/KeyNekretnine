using Dapper;
using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Email;
using KeyNekretnine.Application.Abstraction.Image;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.AdvertUpdates;
using KeyNekretnine.Domain.Agencies;
using KeyNekretnine.Domain.Agents;
using KeyNekretnine.Domain.TemporeryImageDatas;
using KeyNekretnine.Domain.Users;
using KeyNekretnine.Infrastructure.Authentication;
using KeyNekretnine.Infrastructure.BackgroundJobs.ImageDeleter;
using KeyNekretnine.Infrastructure.BackgroundJobs.ImageUploader;
using KeyNekretnine.Infrastructure.BackgroundJobs.Outbox;
using KeyNekretnine.Infrastructure.Clock;
using KeyNekretnine.Infrastructure.Data;
using KeyNekretnine.Infrastructure.EmailProvider;
using KeyNekretnine.Infrastructure.ImageProvider;
using KeyNekretnine.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IO;
using Npgsql;
using Quartz;
using SendGrid.Extensions.DependencyInjection;
using System.Text;

namespace KeyNekretnine.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IEmailService, EmailService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IGoogleService, GoogleService>();

        AddPersistence(services, configuration);
        AddAuthentication(services);
        AddBackgroundJobs(services, configuration);

        return services;
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString =
            GetConnectionString() ??
            throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IAgencyRepository, AgencyRepository>();
        services.AddScoped<IAgentRepository, AgentRepository>();
        services.AddScoped<IImageToDeleteRepository, ImageToDeleteRepository>();
        services.AddScoped<IAdvertRepository, AdvertRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAdvertUpdateRepository, AdvertUpdateRepository>();
        services.AddScoped<ITemporeryImageDataRepository, TemporeryImageDataRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ =>
            new SqlConnectionFactory(connectionString));

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
    }

    private static void AddAuthentication(IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(o =>
        {
            o.Password.RequireDigit = true;
            o.Password.RequireLowercase = true;
            o.Password.RequireUppercase = true;
            o.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddTokenProvider("KeyNekretnineAPI", typeof(DataProtectorTokenProvider<User>))
            .AddDefaultTokenProviders();

        services.Configure<DataProtectionTokenProviderOptions>(o =>
        o.TokenLifespan = TimeSpan.FromHours(24));

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

        services.AddSingleton<RecyclableMemoryStreamManager>(provider =>
            new RecyclableMemoryStreamManager(new RecyclableMemoryStreamManager.Options()
            {
                BlockSize = 4 * 1024,
                LargeBufferMultiple = 512 * 1024,
                MaximumBufferSize = 5 * 1024 * 1024,
                GenerateCallStacks = false,
                AggressiveBufferReturn = true,
                MaximumLargePoolFreeBytes = 2 * (512 * 1024),
                MaximumSmallPoolFreeBytes = 100 * 1024,
            })
        );

        services.AddScoped<IJwtService, JwtService>();

        services.AddHttpContextAccessor();

        services.AddScoped<IUserContext, UserContext>();

        services.AddSendGrid(options =>
        {
            options.ApiKey = Environment.GetEnvironmentVariable("SEND_GRID_API_KEY");
        });
    }

    private static void AddBackgroundJobs(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));
        services.Configure<ImageDeleteOptions>(configuration.GetSection("ImageDeleter"));
        services.Configure<ImageUploadOptions>(configuration.GetSection("ImageUploader"));

        services.AddQuartz();

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        services.ConfigureOptions<ProcessOutboxMessagesJobSetup>();
        services.ConfigureOptions<ProcessImageDeleteJobSetup>();
        services.ConfigureOptions<ProcessImageUploadJobSetup>();
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
}