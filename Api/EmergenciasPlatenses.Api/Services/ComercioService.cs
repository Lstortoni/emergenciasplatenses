using EmergenciasPlatenses.Api.DTOs;
using EmergenciasPlatenses.Api.Models;
using EmergenciasPlatenses.Api.Repositories;

namespace EmergenciasPlatenses.Api.Services;

public class ComercioService(IComercioRepository comercioRepository) : IComercioService
{
    public async Task<IReadOnlyCollection<CategoriaDto>> ObtenerCategoriasAsync()
    {
        var categorias = await comercioRepository.ObtenerCategoriasAsync();

        return categorias
            .Select(categoria => new CategoriaDto
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion,
                Icono = categoria.Icono
            })
            .ToArray();
    }

    public async Task<IReadOnlyCollection<ComercioResumenDto>> ObtenerComerciosAsync(
        int? categoriaId,
        int? ciudadId,
        bool? deTurno)
    {
        var comercios = await comercioRepository.ObtenerComerciosAsync(
            categoriaId,
            ciudadId,
            deTurno);

        return comercios.Select(CrearResumen).ToArray();
    }

    public async Task<ComercioDetalleDto?> ObtenerComercioAsync(int id)
    {
        var comercio = await comercioRepository.ObtenerPorIdAsync(id);
        return comercio is null ? null : CrearDetalle(comercio);
    }

    private static ComercioResumenDto CrearResumen(Comercio comercio)
    {
        return new ComercioResumenDto
        {
            Id = comercio.Id,
            Nombre = comercio.Nombre,
            Direccion = FormatearDireccion(comercio.Direccion),
            Telefono = comercio.Telefono,
            EstaDeTurno = comercio.EstaDeTurno,
            CategoriaId = comercio.CategoriaId,
            Categoria = comercio.Categoria.Nombre,
            CiudadId = comercio.Direccion.CiudadId,
            Ciudad = comercio.Direccion.Ciudad.Nombre
        };
    }

    private static ComercioDetalleDto CrearDetalle(Comercio comercio)
    {
        var direccion = comercio.Direccion;
        var ciudad = direccion.Ciudad;

        return new ComercioDetalleDto
        {
            Id = comercio.Id,
            Nombre = comercio.Nombre,
            Descripcion = comercio.Descripcion,
            Telefono = comercio.Telefono,
            WhatsApp = comercio.WhatsApp,
            Horario = comercio.Horario,
            EstaDeTurno = comercio.EstaDeTurno,
            CategoriaId = comercio.CategoriaId,
            Categoria = comercio.Categoria.Nombre,
            CiudadId = direccion.CiudadId,
            Direccion = new DireccionDto
            {
                Calle = direccion.Calle,
                Numero = direccion.Numero,
                Piso = direccion.Piso,
                Departamento = direccion.Departamento,
                CodigoPostal = direccion.CodigoPostal,
                Referencia = direccion.Referencia,
                Ciudad = ciudad.Nombre,
                Provincia = ciudad.Provincia,
                Pais = ciudad.Pais,
                Latitud = direccion.Latitud,
                Longitud = direccion.Longitud,
                DireccionCompleta = FormatearDireccion(direccion)
            }
        };
    }

    private static string FormatearDireccion(Direccion direccion) =>
        $"{direccion.Calle} Nº {direccion.Numero}, {direccion.Ciudad.Nombre}";
}
