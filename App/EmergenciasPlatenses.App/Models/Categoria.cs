namespace EmergenciasPlatenses.App.Models;

public class Categoria
{
    public int Id { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public string? Descripcion { get; init; }
    public string? Icono { get; init; }
}
