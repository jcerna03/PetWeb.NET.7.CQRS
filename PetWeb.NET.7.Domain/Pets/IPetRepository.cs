using PetWeb.NET._7.Domain.Shared;

namespace PetWeb.NET._7.Domain.Pets;

public interface IPetRepository
{
    Task<Page<Pet>> GetPetAsync(PetFilter query, PageRequest page);

    Task<int> InsertPetAsync(string name, int type);

    Task<bool> UpdatePetAsync(int petId, string name, int type);

    Task<bool> DeletePetAsync(int petId);
}