using MediatR;
using PetWeb.NET._7.Application.Pets.Responses;

namespace PetWeb.NET._7.Application.Pets.Commands;

public class InsertPetCommand : IRequest<InsertPetResponse>
{
    public string? Name { get; set; }
    public int Type { get; set; }
}