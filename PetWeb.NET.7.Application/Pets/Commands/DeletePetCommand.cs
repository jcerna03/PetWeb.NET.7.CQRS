using MediatR;
using PetWeb.NET._7.Application.Pets.Responses;

namespace PetWeb.NET._7.Application.Pets.Commands;

public class DeletePetCommand : IRequest<DeletePetResponse>
{
    public int PetId { get; set; }
}