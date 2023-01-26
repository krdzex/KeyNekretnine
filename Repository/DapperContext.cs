using Npgsql;
using System.Data;

namespace Repository
{
    public class DapperContext
    {

        public IDbConnection CreateConnection()
            => new NpgsqlConnection(GetConnectionString());


        private static string GetConnectionString()
        {

            //var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            //var databaseUri = new Uri(databaseUrl);
            //var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                //Host = databaseUri.Host,
                //Port = databaseUri.Port,
                //Username = userInfo[0],
                //Password = userInfo[1],
                //Database = databaseUri.LocalPath.TrimStart('/')
                // Local testing
                Host = "localhost",
                Port = 15432,
                Username = "postgres",
                Password = "b3dfe7ef987752928499ef1e4e9e3f10a0e3f74c8eee1028",
                Database = "agencija108"
            };

            return builder.ToString();
        }
    }
}
