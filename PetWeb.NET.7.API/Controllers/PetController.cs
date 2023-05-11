using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetWeb.NET._7.API.Requests;
using PetWeb.NET._7.Application.Pets.Commands;
using PetWeb.NET._7.Application.Pets.Queries;
using PetWeb.NET._7.Application.Pets.Responses;
using PetWeb.NET._7.Domain.Shared;

namespace PetWeb.NET._7.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PetController : ControllerBase
{
    private readonly IMediator _mediator;

    public PetController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetPets([FromQuery] GetPetRequest request)
    {
        Page<GetPetResponse> response = await _mediator.Send(
            new GetPetQuery
            {
                PetId = request.PetId,
                Name = request.Name,
                Type = request.Type,
                PageRequest = new PageRequest
                {
                    Page = request.Page ?? 1,
                    Size = request.Size ?? 20,
                }
            });

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> InsertPet([FromBody] InsertPetRequest request)
    {
        return Ok(await _mediator.Send(new InsertPetCommand { Name = request.Name, Type = request.Type }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePet(int id, [FromBody] UpdatePetRequest request)
    {
        UpdatePetResponse response = await _mediator.Send(
            new UpdatePetCommand
            {
                PetId = id,
                Name = request.Name,
                Type = request.Type
            });

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePet(int id)
    {
        return Ok(await _mediator.Send(new DeletePetCommand { PetId = id }));
    }
}