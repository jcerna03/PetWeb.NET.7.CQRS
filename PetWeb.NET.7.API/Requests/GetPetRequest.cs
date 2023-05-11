namespace PetWeb.NET._7.API.Requests;

public class GetPetRequest : Pageable
{
    public int? PetId { get; set; }

    public string? Name { get; set; }
    public int? Type { get; set; }
}