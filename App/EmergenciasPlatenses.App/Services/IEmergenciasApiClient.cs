using EmergenciasPlatenses.App.Models;

namespace EmergenciasPlatenses.App.Services;

public interface IEmergenciasApiClient
{
    Task<IReadOnlyCollection<Categoria>> ObtenerCategoriasAsync(
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<ComercioResumen>> ObtenerComerciosAsync(
        int? categoriaId = null,
        int? ciudadId = null,
        bool? atiende24Horas = null,
        decimal? latitud = null,
        decimal? longitud = null,
        CancellationToken cancellationToken = default);

    Task<ComercioDetalle?> ObtenerComercioAsync(
        int id,
        CancellationToken cancellationToken = default);
}
