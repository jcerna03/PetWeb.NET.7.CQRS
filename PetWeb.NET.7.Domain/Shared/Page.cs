namespace PetWeb.NET._7.Domain.Shared;

public class Page<T>
{
    public int CurrentPage { get; set; }
    public int TotalResults { get; set; }
    public int Size { get; set; }
    public IEnumerable<T> Results { get; set; } = new List<T>();
}