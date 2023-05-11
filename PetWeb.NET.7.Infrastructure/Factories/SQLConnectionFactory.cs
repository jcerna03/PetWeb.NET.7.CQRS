using Microsoft.Extensions.Options;
using PetWeb.NET._7.Infrastructure.Options;
using System.Data;
using System.Data.SqlClient;

namespace PetWeb.NET._7.Infrastructure.Factories;

public class SQLConnectionFactory : IConnectionFactory
{
    private readonly DatabaseOptions _databaseOptions;
    public SQLConnectionFactory(IOptions<DatabaseOptions> databaseOptions)
    {
        _databaseOptions = databaseOptions.Value;
    }
    public IDbConnection GetConnection()
    {
        return new SqlConnection(_databaseOptions.ConnectionString);
    }
}