using MediatR;
using PetWeb.NET._7.Application.Pets.Responses;
using PetWeb.NET._7.Domain.Shared;

namespace PetWeb.NET._7.Application.Pets.Queries;

public class GetPetQuery : IRequest<Page<GetPetResponse>>
{
    public int? PetId { get; set; }
    public string? Name { get; set; }
    public int? Type { get; set; }
    public PageRequest? PageRequest { get; set; }
}