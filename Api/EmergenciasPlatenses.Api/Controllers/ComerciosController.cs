using EmergenciasPlatenses.Api.DTOs;
using EmergenciasPlatenses.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmergenciasPlatenses.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComerciosController(IComercioService comercioService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyCollection<ComercioResumenDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<ComercioResumenDto>>> ObtenerComercios(
        [FromQuery] int? categoriaId,
        [FromQuery] int? ciudadId,
        [FromQuery] bool? atiende24Horas,
        [FromQuery] decimal? latitud,
        [FromQuery] decimal? longitud)
    {
        if (latitud.HasValue != longitud.HasValue)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Ubicación incompleta",
                Detail = "Latitud y longitud deben enviarse juntas.",
                Status = StatusCodes.Status400BadRequest
            });
        }

        if (latitud is < -90 or > 90)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Latitud inválida",
                Detail = "La latitud debe estar entre -90 y 90.",
                Status = StatusCodes.Status400BadRequest
            });
        }

        if (longitud is < -180 or > 180)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Longitud inválida",
                Detail = "La longitud debe estar entre -180 y 180.",
                Status = StatusCodes.Status400BadRequest
            });
        }

        return Ok(await comercioService.ObtenerComerciosAsync(
            categoriaId,
            ciudadId,
            atiende24Horas,
            latitud,
            longitud));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<ComercioDetalleDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ComercioDetalleDto>> ObtenerComercio(int id)
    {
        var comercio = await comercioService.ObtenerComercioAsync(id);
        return comercio is null ? NotFound() : Ok(comercio);
    }
}
