namespace EmergenciasPlatenses.App.Models;

public class ComercioDetalle
{
    public int Id { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public string? Descripcion { get; init; }
    public string? Telefono { get; init; }
    public string? WhatsApp { get; init; }
    public string? Horario { get; init; }
    public bool EstaDeTurno { get; init; }
    public int CategoriaId { get; init; }
    public string Categoria { get; init; } = string.Empty;
    public int CiudadId { get; init; }
    public Direccion Direccion { get; init; } = new();
}
