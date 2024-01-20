using CleanArchitecture.Application.Vehiculos.BuscarVehiculos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.Vehiculos;

[ApiController]
[Route("api/vehiculos")]
public class VehiculosController : ControllerBase
{
    private readonly ISender _sender;

    public VehiculosController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> SearchVehiculo(
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken
    )
    {
        var  query = new BuscarVehiculoQuery(startDate, endDate);
        var resultados = await _sender.Send(query, cancellationToken);
        return Ok(resultados.Value);
    }
}