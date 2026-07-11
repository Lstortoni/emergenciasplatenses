namespace EmergenciasPlatenses.Api.DTOs;

public class DireccionDto
{
    public string Calle { get; init; } = string.Empty;

    public string Numero { get; init; } = string.Empty;

    public string? Piso { get; init; }

    public string? Departamento { get; init; }

    public string? CodigoPostal { get; init; }

    public string? Referencia { get; init; }

    public string Ciudad { get; init; } = string.Empty;

    public string Provincia { get; init; } = string.Empty;

    public string Pais { get; init; } = string.Empty;

    public decimal? Latitud { get; init; }

    public decimal? Longitud { get; init; }

    public string DireccionCompleta { get; init; } = string.Empty;
}
