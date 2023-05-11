using System.Data;

namespace PetWeb.NET._7.Infrastructure.Factories;

public interface IConnectionFactory
{
    IDbConnection GetConnection();
}