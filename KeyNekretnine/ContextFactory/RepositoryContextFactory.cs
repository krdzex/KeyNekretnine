using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;

namespace KeyNekretnine.ContextFactory;
public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
{
    public RepositoryContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<RepositoryContext>()
            .UseNpgsql("Server=localhost;Database=test;Port=5432;User Id=postgres;Password=kikimiki;",
            b => b.MigrationsAssembly("KeyNekretnine")).UseSnakeCaseNamingConvention();

        return new RepositoryContext(builder.Options);
    }
}
