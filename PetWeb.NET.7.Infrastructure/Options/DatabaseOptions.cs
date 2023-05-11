namespace PetWeb.NET._7.Infrastructure.Options;

public class DatabaseOptions
{
    public const string Name = "Database";
    public string? ConnectionString { get; set; }
    public int CommandTimeOut { get; set; }
}