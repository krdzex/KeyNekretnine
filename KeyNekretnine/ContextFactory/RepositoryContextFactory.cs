using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;
using Repository;

namespace KeyNekretnine.ContextFactory;
public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
{
    public RepositoryContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<RepositoryContext>()
            .UseNpgsql(GetConnectionString(),
            b => b.MigrationsAssembly("KeyNekretnine")).UseSnakeCaseNamingConvention();

        return new RepositoryContext(builder.Options);
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
