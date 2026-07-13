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
        bool? atiende24Horas,
        decimal? latitud,
        decimal? longitud)
    {
        var comercios = await comercioRepository.ObtenerComerciosAsync(
            categoriaId,
            ciudadId,
            atiende24Horas);

        var resultado = comercios
            .Select(comercio => CrearResumen(comercio, latitud, longitud));

        resultado = latitud.HasValue
            ? resultado
                .OrderBy(comercio => comercio.DistanciaKm ?? double.MaxValue)
                .ThenBy(comercio => comercio.Nombre)
            : resultado.OrderBy(comercio => comercio.Nombre);

        return resultado.ToArray();
    }

    public async Task<ComercioDetalleDto?> ObtenerComercioAsync(int id)
    {
        var comercio = await comercioRepository.ObtenerPorIdAsync(id);
        return comercio is null ? null : CrearDetalle(comercio);
    }

    private static ComercioResumenDto CrearResumen(
        Comercio comercio,
        decimal? latitudUsuario,
        decimal? longitudUsuario)
    {
        var direccion = comercio.Direccion;

        return new ComercioResumenDto
        {
            Id = comercio.Id,
            Nombre = comercio.Nombre,
            Direccion = FormatearDireccion(direccion),
            Telefono = comercio.Telefono,
            Atiende24Horas = comercio.Atiende24Horas,
            CategoriaId = comercio.CategoriaId,
            Categoria = comercio.Categoria.Nombre,
            CiudadId = direccion.CiudadId,
            Ciudad = direccion.Ciudad.Nombre,
            Latitud = direccion.Latitud,
            Longitud = direccion.Longitud,
            DistanciaKm = CalcularDistanciaKm(
                latitudUsuario,
                longitudUsuario,
                direccion.Latitud,
                direccion.Longitud)
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
            Atiende24Horas = comercio.Atiende24Horas,
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

    private static double? CalcularDistanciaKm(
        decimal? latitudOrigen,
        decimal? longitudOrigen,
        decimal? latitudDestino,
        decimal? longitudDestino)
    {
        if (!latitudOrigen.HasValue ||
            !longitudOrigen.HasValue ||
            !latitudDestino.HasValue ||
            !longitudDestino.HasValue)
        {
            return null;
        }

        const double radioTierraKm = 6371.0088;

        var latitudOrigenRadianes = GradosARadianes((double)latitudOrigen.Value);
        var latitudDestinoRadianes = GradosARadianes((double)latitudDestino.Value);
        var diferenciaLatitud = GradosARadianes(
            (double)(latitudDestino.Value - latitudOrigen.Value));
        var diferenciaLongitud = GradosARadianes(
            (double)(longitudDestino.Value - longitudOrigen.Value));

        var haversine =
            Math.Pow(Math.Sin(diferenciaLatitud / 2), 2) +
            Math.Cos(latitudOrigenRadianes) *
            Math.Cos(latitudDestinoRadianes) *
            Math.Pow(Math.Sin(diferenciaLongitud / 2), 2);

        var anguloCentral = 2 * Math.Atan2(
            Math.Sqrt(haversine),
            Math.Sqrt(1 - haversine));

        return Math.Round(radioTierraKm * anguloCentral, 2);
    }

    private static double GradosARadianes(double grados) =>
        grados * Math.PI / 180;
}
