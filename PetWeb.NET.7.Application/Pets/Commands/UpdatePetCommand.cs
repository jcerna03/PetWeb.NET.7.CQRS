using MediatR;
using PetWeb.NET._7.Application.Pets.Responses;

namespace PetWeb.NET._7.Application.Pets.Commands;

public class UpdatePetCommand : IRequest<UpdatePetResponse>
{
    public int PetId { get; set; }
    public string? Name { get; set; }
    public int Type { get; set; }
}