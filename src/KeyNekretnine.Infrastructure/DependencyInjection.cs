using Dapper;
using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Email;
using KeyNekretnine.Application.Abstraction.Image;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.Agencies;
using KeyNekretnine.Domain.Agents;
using KeyNekretnine.Domain.Users;
using KeyNekretnine.Infrastructure.Authentication;
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
using Npgsql;
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
            o.SignIn.RequireConfirmedEmail = true;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddTokenProvider("KeyNekretnineAPI", typeof(DataProtectorTokenProvider<User>))
            .AddDefaultTokenProviders();


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

        services.AddScoped<IJwtService, JwtService>();

    }

    private static void AddBackgroundJobs(IServiceCollection services, IConfiguration configuration)
    {
        //services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));
        //services.Configure<ImageDeleteOptions>(configuration.GetSection("ImageDeleter"));

        //services.AddQuartz(options => { options.UseMicrosoftDependencyInjectionJobFactory(); });

        //services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        //services.ConfigureOptions<ProcessOutboxMessagesJobSetup>();
        //services.ConfigureOptions<ProcessImageDeleteJobSetup>();

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