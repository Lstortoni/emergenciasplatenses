namespace EmergenciasPlatenses.Api.Models;

public class Comercio
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    public string? Telefono { get; set; }

    public string? WhatsApp { get; set; }

    public string? Horario { get; set; }

    public bool EstaDeTurno { get; set; }

    public bool Activo { get; set; } = true;

    public int CategoriaId { get; set; }

    public Categoria Categoria { get; set; } = null!;

    public Direccion Direccion { get; set; } = new();
}
