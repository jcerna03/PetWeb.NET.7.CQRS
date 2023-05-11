namespace PetWeb.NET._7.Domain.Shared;

public class PageRequest
{
    public int Page { get; set; } = 1;

    public int Size { get; set; } = 20;

    public virtual int Offset => (Page - 1) * Size;
}