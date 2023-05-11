using MediatR;
using PetWeb.NET._7.Application.Pets.Commands;
using PetWeb.NET._7.Application.Pets.Responses;
using PetWeb.NET._7.Domain.Pets;

namespace PetWeb.NET._7.Application.Pets.Handlers;

public class DeletePetHandler : IRequestHandler<DeletePetCommand, DeletePetResponse>
{
    private readonly IPetRepository _petRepository;

    public DeletePetHandler(IPetRepository PetRepository)
    {
        _petRepository = PetRepository;
    }

    public async Task<DeletePetResponse> Handle(DeletePetCommand request, CancellationToken cancellationToken)
    {
        return new DeletePetResponse
        {
            Success = await _petRepository.DeletePetAsync(request.PetId)
        };
    }
}