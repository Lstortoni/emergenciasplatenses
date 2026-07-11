using EmergenciasPlatenses.Api.DTOs;
using EmergenciasPlatenses.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmergenciasPlatenses.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController(IComercioService comercioService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyCollection<CategoriaDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<CategoriaDto>>> ObtenerCategorias()
    {
        return Ok(await comercioService.ObtenerCategoriasAsync());
    }
}
