using EmergenciasPlatenses.Api.DTOs;

namespace EmergenciasPlatenses.Api.Services;

public interface IComercioService
{
    Task<IReadOnlyCollection<CategoriaDto>> ObtenerCategoriasAsync();

    Task<IReadOnlyCollection<ComercioResumenDto>> ObtenerComerciosAsync(
        int? categoriaId,
        int? ciudadId,
        bool? atiende24Horas,
        decimal? latitud,
        decimal? longitud);

    Task<ComercioDetalleDto?> ObtenerComercioAsync(int id);
}
