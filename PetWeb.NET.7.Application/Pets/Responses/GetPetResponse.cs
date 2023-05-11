namespace PetWeb.NET._7.Application.Pets.Responses;

public class GetPetResponse
{
    public int PetId { get; set; }
    public string? Name { get; set; }
    public int Type { get; set; }
}