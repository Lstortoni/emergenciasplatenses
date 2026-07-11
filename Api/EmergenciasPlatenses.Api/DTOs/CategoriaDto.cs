namespace EmergenciasPlatenses.Api.DTOs;

public class CategoriaDto
{
    public int Id { get; init; }

    public string Nombre { get; init; } = string.Empty;

    public string? Descripcion { get; init; }

    public string? Icono { get; init; }
}
