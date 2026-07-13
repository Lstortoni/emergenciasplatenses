namespace EmergenciasPlatenses.Api.DTOs;

public class ComercioResumenDto
{
    public int Id { get; init; }

    public string Nombre { get; init; } = string.Empty;

    public string Direccion { get; init; } = string.Empty;

    public string? Telefono { get; init; }

    public bool Atiende24Horas { get; init; }

    public int CategoriaId { get; init; }

    public string Categoria { get; init; } = string.Empty;

    public int CiudadId { get; init; }

    public string Ciudad { get; init; } = string.Empty;

    public decimal? Latitud { get; init; }

    public decimal? Longitud { get; init; }

    public double? DistanciaKm { get; init; }
}
