using MediatR;
using PetWeb.NET._7.Application.Pets.Queries;
using PetWeb.NET._7.Application.Pets.Responses;
using PetWeb.NET._7.Domain.Pets;
using PetWeb.NET._7.Domain.Shared;

namespace PetWeb.NET._7.Application.Pets.Handlers;

public class GetPetHandler : IRequestHandler<GetPetQuery, Page<GetPetResponse>>
{
    private readonly IPetRepository _petRepository;

    public GetPetHandler(IPetRepository PetRepository)
    {
        _petRepository = PetRepository;
    }

    public async Task<Page<GetPetResponse>> Handle(GetPetQuery request, CancellationToken cancellationToken)
    {
        PetFilter filter = new()
        {
            PetId = request.PetId,
            Name = request.Name,
            Type = request.Type,
        };

        Page<Pet> response = await _petRepository.GetPetAsync(filter, request.PageRequest!);

        return new Page<GetPetResponse>
        {
            CurrentPage = response.CurrentPage,
            Size = response.Size,
            TotalResults = response.TotalResults,
            Results = response.Results.Select(pet => new GetPetResponse
            {
                PetId = pet.PetId,
                Name = pet.Name
            })
        };
    }
}
