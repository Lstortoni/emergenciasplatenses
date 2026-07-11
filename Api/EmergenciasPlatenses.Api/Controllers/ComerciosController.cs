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
        [FromQuery] bool? deTurno)
    {
        return Ok(await comercioService.ObtenerComerciosAsync(categoriaId, ciudadId, deTurno));
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
