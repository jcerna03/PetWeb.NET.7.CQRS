using MediatR;
using PetWeb.NET._7.Application.Pets.Commands;
using PetWeb.NET._7.Application.Pets.Responses;
using PetWeb.NET._7.Domain.Pets;

namespace PetWeb.NET._7.Application.Pets.Handlers;

public class InsertPetHandler : IRequestHandler<InsertPetCommand, InsertPetResponse>
{
    private readonly IPetRepository _petRepository;

    public InsertPetHandler(IPetRepository PetRepository)
    {
        _petRepository = PetRepository;
    }

    public async Task<InsertPetResponse> Handle(InsertPetCommand request, CancellationToken cancellationToken)
    {
        return new InsertPetResponse
        {
            PetId = await _petRepository.InsertPetAsync(request.Name!, request.Type)
        };
    }
}