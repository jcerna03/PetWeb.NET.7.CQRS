using MediatR;
using PetWeb.NET._7.Application.Pets.Commands;
using PetWeb.NET._7.Application.Pets.Responses;
using PetWeb.NET._7.Domain.Pets;

namespace PetWeb.NET._7.Application.Pets.Handlers;

public class UpdatePetHandler : IRequestHandler<UpdatePetCommand, UpdatePetResponse>
{
    private readonly IPetRepository _petRepository;

    public UpdatePetHandler(IPetRepository PetRepository)
    {
        _petRepository = PetRepository;
    }

    public async Task<UpdatePetResponse> Handle(UpdatePetCommand request, CancellationToken cancellationToken)
    {
        return new UpdatePetResponse
        {
            Success = await _petRepository.UpdatePetAsync(request.PetId, request.Name!, request.Type)
        };
    }
}
