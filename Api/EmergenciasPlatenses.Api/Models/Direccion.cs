namespace EmergenciasPlatenses.Api.Models;

public class Direccion
{
    public int Id { get; set; }

    public int ComercioId { get; set; }

    public string Calle { get; set; } = string.Empty;

    public string Numero { get; set; } = string.Empty;

    public string? Piso { get; set; }

    public string? Departamento { get; set; }

    public string? CodigoPostal { get; set; }

    public string? Referencia { get; set; }

    public int CiudadId { get; set; }

    public Ciudad Ciudad { get; set; } = null!;

    public decimal? Latitud { get; set; }

    public decimal? Longitud { get; set; }
}
