namespace EmergenciasPlatenses.Api.DTOs;

public class ComercioDetalleDto
{
    public int Id { get; init; }

    public string Nombre { get; init; } = string.Empty;

    public string? Descripcion { get; init; }

    public string? Telefono { get; init; }

    public string? WhatsApp { get; init; }

    public string? Horario { get; init; }

    public bool Atiende24Horas { get; init; }

    public int CategoriaId { get; init; }

    public string Categoria { get; init; } = string.Empty;

    public int CiudadId { get; init; }

    public DireccionDto Direccion { get; init; } = new();
}
