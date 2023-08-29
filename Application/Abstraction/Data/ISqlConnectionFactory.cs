using System.Data;

namespace KeyNekretnine.Application.Abstraction.Data;
public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
