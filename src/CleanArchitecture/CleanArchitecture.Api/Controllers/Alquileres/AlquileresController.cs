using CleanArchitecture.Application.Alquileres.ObterAlquiler;
using CleanArchitecture.Application.Alquileres.ReservarAlquiler;
using CleanArchitecture.Domain.Alquileres;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.Alquileres;

[ApiController]
[Route("api/alquileres")]
public class AlquileresController : ControllerBase
{
    private readonly ISender _sender;

    public AlquileresController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("id")]
    public async Task<IActionResult> ObterAlquiler(
        DateOnly startDate,
        Guid alquilerId,
        CancellationToken cancellationToken
    )
    {
        var  query = new ObterAlquilerQuery(startDate, alquilerId);
        var resultado = await _sender.Send(query, cancellationToken);
        return resultado.IsSuccess ? Ok(resultado.Value): NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> ReservaAlquiler(
        AlquilerReservaRequest request,
        Guid vehiculoId,
        CancellationToken cancellationToken
    )
    {
        var  command = new ReservarAlquilerCommand(
            request.vehiculoId,
            request.usuarioId,
            request.startDate,
            request.endDate
        );
        var resultado = await _sender.Send(command, cancellationToken);
        return resultado.IsFailure ? BadRequest(resultado.Error): 
            CreatedAtAction(nameof(ObterAlquiler), 
            new { id = resultado.Value }, resultado.Value);
    }
}
