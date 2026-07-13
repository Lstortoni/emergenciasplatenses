using EmergenciasPlatenses.Api.Models;

namespace EmergenciasPlatenses.Api.Repositories;

public interface IComercioRepository
{
    Task<IReadOnlyCollection<Categoria>> ObtenerCategoriasAsync();

    Task<IReadOnlyCollection<Comercio>> ObtenerComerciosAsync(
        int? categoriaId,
        int? ciudadId,
        bool? atiende24Horas);

    Task<Comercio?> ObtenerPorIdAsync(int id);
}
